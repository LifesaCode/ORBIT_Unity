using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Orbit.Models;
using Orbit.Annotations;
using System;
using System.Reflection;

public class C_ThermalGauges : MonoBehaviour
{

    public InternalCoolantLoopData internalCoolant = new InternalCoolantLoopData();
    public ExternalCoolantLoopData externalCoolant = new ExternalCoolantLoopData();

    public GameObject LowTempGauge;
    public Image LowTempDial;
    public GameObject MedTempGauge;
    public Image MedTempDial;
    public GameObject HeatExchGauge;
    public Image HeatExchDial;
    public GameObject LineAPressureGauge;
    public Image LineADial;
    public GameObject LineBPressureGauge;
    public Image LineBDial;
    public TextMeshProUGUI RotationValue;
    public GameObject Diagram;
    public TextMeshProUGUI InternalMixValue;
    public Image InternalMixValve;
    public TextMeshProUGUI ExternalMixValue;
    public Image ExternalMixValve;
    public GameObject InternalStandbyToggle;
    public GameObject ExternalStandbyToggle;

    float maxLowTemp;
    float maxMedTemp;
    float maxHeatExch;
    float maxLineA;
    float maxLineB;

    private Image[] diagramSprites;
    private Image[] internalStandbyImages;
    private Image[] externalStandbyImages;
    private TextMeshProUGUI[] lowTempGaugeLabels;
    private TextMeshProUGUI[] medTempGaugeLabels;
    private TextMeshProUGUI[] heatExGaugeLabels;
    private TextMeshProUGUI[] lineAGaugeLabels;
    private TextMeshProUGUI[] lineBGaugeLabels;
    private TextMeshProUGUI[] internalStandbyLabels;
    private TextMeshProUGUI[] externalStandbyLabels;
    private SystemStatus internalState;
    private SystemStatus externalState;

    Color redColor = new Color(
                240.0f / 255.0f,
                94.0f / 255.0f,
                97.0f / 255.0f,
                1);
    Color greenColor = new Color(
                162.0f / 255.0f,
                207.0f / 255.0f,
                139.0f / 25.0f,
                1);

    // Start is called before the first frame update
    void Start()
    {
        CrewedStatusSource.ChangeCrewedStatus += ChangeCrewedStatus;

        internalCoolant.SeedData();
        externalCoolant.SeedData();

        internalState = internalCoolant.Status;
        externalState = externalCoolant.Status;

        diagramSprites = Diagram.GetComponentsInChildren<Image>();
        internalStandbyImages = InternalStandbyToggle.GetComponentsInChildren<Image>();
        externalStandbyImages = ExternalStandbyToggle.GetComponentsInChildren<Image>();
        lowTempGaugeLabels = LowTempGauge.GetComponentsInChildren<TextMeshProUGUI>();
        medTempGaugeLabels = MedTempGauge.GetComponentsInChildren<TextMeshProUGUI>();
        heatExGaugeLabels = HeatExchGauge.GetComponentsInChildren<TextMeshProUGUI>();
        lineAGaugeLabels = LineAPressureGauge.GetComponentsInChildren<TextMeshProUGUI>();
        lineBGaugeLabels = LineBPressureGauge.GetComponentsInChildren<TextMeshProUGUI>();
        internalStandbyLabels = InternalStandbyToggle.GetComponentsInChildren<TextMeshProUGUI>();
        externalStandbyLabels = ExternalStandbyToggle.GetComponentsInChildren<TextMeshProUGUI>();

        // set min and max values for each gauge
        // LowTempGauge
        PropertyInfo propInfo = typeof(InternalCoolantLoopData).GetProperty("TempLowLoop");
        maxLowTemp = 20;
        SetGaugeMinMax(propInfo, lowTempGaugeLabels);
        UpdateGauge(LowTempDial, lowTempGaugeLabels, (float)internalCoolant.TempLowLoop, maxLowTemp);

        // MedTempGauge
        propInfo = typeof(InternalCoolantLoopData).GetProperty("TempMedLoop");
        maxMedTemp = 35f;
        SetGaugeMinMax(propInfo, medTempGaugeLabels);
        UpdateGauge(MedTempDial, medTempGaugeLabels, (float)internalCoolant.TempMedLoop, maxMedTemp);

        // HeatExchangerGauge
        propInfo = typeof(ExternalCoolantLoopData).GetProperty("OutputFluidTemperature");
        maxHeatExch = 8.1f;
        SetGaugeMinMax(propInfo, heatExGaugeLabels);
        UpdateGauge(HeatExchDial, heatExGaugeLabels, (float)externalCoolant.OutputFluidTemperature, maxHeatExch);

        // LineAPressureGauge
        propInfo = typeof(ExternalCoolantLoopData).GetProperty("LineAPressure");
        maxLineA = 3309f;
        SetGaugeMinMax(propInfo, lineAGaugeLabels);
        UpdateGauge(LineADial, lineAGaugeLabels, (float)externalCoolant.LineAPressure, maxLineA);

        // LineBPressureGauge
        propInfo = typeof(ExternalCoolantLoopData).GetProperty("LineBPressure");
        maxLineB = 3309f;
        SetGaugeMinMax(propInfo, lineBGaugeLabels);
        UpdateGauge(LineBDial, lineBGaugeLabels, (float)externalCoolant.LineBPressure, maxLineB);

        StartCoroutine(UpdateThermal());
    }

    void SetGaugeMinMax(PropertyInfo propInfo, TextMeshProUGUI[] labels)
    {
        IdealRangeAttribute idealRange = (IdealRangeAttribute)Attribute.GetCustomAttribute(propInfo, typeof(IdealRangeAttribute));
        labels[2].text = idealRange.IdealMaximum.ToString();
        labels[3].text = idealRange.IdealMinimum.ToString();
    }

    void UpdateGauge(Image dial, TextMeshProUGUI[] labels, float value, float maxTemp)
    {
        labels[1].text = value.ToString();
        dial.fillAmount = (value / maxTemp * 1.1f) * .75f;
    }

    IEnumerator UpdateThermal()
    {
        while (true)
        {
            externalCoolant.ProcessData();
            internalCoolant.ProcessData();

            if(externalState != externalCoolant.Status)
            {
                UpdateExternalToggle();
            }

            if(internalState != internalCoolant.Status)
            {
                UpdateInternalToggle();
            }

            // update gauge values and dials
            UpdateGauge(LowTempDial, lowTempGaugeLabels, (float)internalCoolant.TempLowLoop, maxLowTemp);
            UpdateGauge(MedTempDial, medTempGaugeLabels, (float)internalCoolant.TempMedLoop, maxMedTemp);
            UpdateGauge(HeatExchDial, heatExGaugeLabels, (float)externalCoolant.OutputFluidTemperature, maxHeatExch);
            UpdateGauge(LineADial, lineAGaugeLabels, (float)externalCoolant.LineAPressure, maxLineA);
            UpdateGauge(LineBDial, lineBGaugeLabels, (float)externalCoolant.LineBPressure, maxLineB);

            UpdateMixingValves();
            CheckRadiator();
            CheckPumps();

            yield return new WaitForSeconds(.5f);
        }
    }

    void UpdateMixingValves()
    {
        // update pie charts
        InternalMixValve.fillAmount = internalCoolant.LowTempMixValvePosition / 100f;
        ExternalMixValve.fillAmount = externalCoolant.MixValvePosition / 100f;

        // update values
        InternalMixValue.text = internalCoolant.LowTempMixValvePosition.ToString();
        ExternalMixValue.text = externalCoolant.MixValvePosition.ToString();
    }

    void CheckRadiator()
    {
        // update rotation value;
        RotationValue.text = externalCoolant.RadiatorRotation.ToString();

        // if radiator is not deployed, enable red overlay
        if (externalCoolant.RadiatorDeployed)
        {

            diagramSprites[7].color = Color.white;
        }
        else
        {
            redColor.a = 1;
            diagramSprites[7].color = redColor;
        }
    }

    void CheckPumps()
    {
        // pump A
        if (internalCoolant.MedTempPumpOn && internalCoolant.LowTempPumpOn)
        {
            redColor.a = 0;
            greenColor.a = 1;
        }
        else
        {
            redColor.a = 1;
            greenColor.a = 0;
        }
        diagramSprites[1].color = greenColor;
        diagramSprites[2].color = redColor;

        // pump B
        if (externalCoolant.PumpBOn)
        {
            redColor.a = 0;
            greenColor.a = 1;
        }
        else
        {
            redColor.a = 1;
            greenColor.a = 0;
        }
        diagramSprites[3].color = greenColor;
        diagramSprites[4].color = redColor;
    }

    void ChangeCrewedStatus()
    {
        if(internalCoolant.Mode == Modes.Crewed)
        {
            internalCoolant.Mode = Modes.Uncrewed;
        }
        else
        {
            internalCoolant.Mode = Modes.Crewed;
        }
    }

    public void ToggleExternalManualMode()
    {
        externalCoolant.IsManualMode = !externalCoolant.IsManualMode;
    }

    public void ToggleInternalManualMode()
    {
        internalCoolant.IsManualMode = !internalCoolant.IsManualMode;
    }

    public void ToggleInternalStandby()
    {
        internalCoolant.ToggleOnOff();
        internalState = internalCoolant.Status;

        UpdateInternalToggle();
    }

    public void ToggleExternalStandby()
    {
        externalCoolant.ToggleOnOff();
        externalState = externalCoolant.Status;

        UpdateExternalToggle();
    }

    void UpdateInternalToggle()
    {
        if(internalState == SystemStatus.Standby)
        {
            internalStandbyImages[1].enabled = false;
            internalStandbyImages[2].enabled = true;
            internalStandbyLabels[0].color = new Color(255, 255, 255, 1);
            internalStandbyLabels[1].color = new Color(0, 0, 0, 1);
        }
        else
        {
            internalStandbyImages[1].enabled = true;
            internalStandbyImages[2].enabled = false;
            internalStandbyLabels[0].color = new Color(0, 0, 0, 1);
            internalStandbyLabels[1].color = new Color(255, 255, 255, 1);
        }
    }

    void UpdateExternalToggle()
    {
        if (internalState == SystemStatus.Standby)
        {
            externalStandbyImages[1].enabled = false;
            externalStandbyImages[2].enabled = true;
            externalStandbyLabels[0].color = new Color(255, 255, 255, 1);
            externalStandbyLabels[1].color = new Color(0, 0, 0, 1);
        }
        else
        {
            externalStandbyImages[1].enabled = true;
            externalStandbyImages[2].enabled = false;
            externalStandbyLabels[0].color = new Color(0, 0, 0, 1);
            externalStandbyLabels[1].color = new Color(255, 255, 255, 1);
        }
    }
}
