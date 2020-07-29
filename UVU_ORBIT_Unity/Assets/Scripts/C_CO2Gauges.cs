using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Orbit.Models;
using Orbit.Annotations;
using System;
using System.Reflection;


public class C_CO2Gauges : MonoBehaviour
{
    public GameObject Bed1TempGauge;
    public Image Bed1TempDial;
    public GameObject Bed2TempGauge;
    public Image Bed2TempDial;
    public GameObject CO2LevelGauge;
    public Image CO2LevelDial;
    public Image CO2Tank;
    // taken from Sabatier co2 tank level
    public Image CO2TankSource;
    public TextMeshProUGUI ActiveBedLabel;
    public GameObject BedToggle;
    public GameObject Diagram;

    Image[] diagramImages;
    Image[] toggleImages;
    TextMeshProUGUI[] bed1GaugeLabels;
    TextMeshProUGUI[] bed2GaugeLabels;
    TextMeshProUGUI[] co2LevelLabels;
    TextMeshProUGUI[] toggleText;
    float highTemp = 232;
    float maxCO2 = 8;

    int fanCounter = 0;

    Color opaqueRed = new Color(
       240.0f / 255.0f,
       94.0f / 255.0f,
       97.0f / 255.0f,
       1);
    Color clearRed = new Color(
       240.0f / 255.0f,
       94.0f / 255.0f,
       97.0f / 255.0f,
       0);

    Color white = new Color(0f, 0f, 0f, 1);
    Color black = new Color(255f, 255f, 255f, 1);

        
    CarbonDioxideRemediation co2System = new CarbonDioxideRemediation();

    // Start is called before the first frame update
    void Start()
    {
        CrewedStatusSource.ChangeCrewedStatus += ChangeCrewedStatus;
        co2System.SeedData();

        // get gauge label components
        diagramImages = Diagram.GetComponentsInChildren<Image>();
        toggleImages = BedToggle.GetComponentsInChildren<Image>();

        bed1GaugeLabels = Bed1TempGauge.GetComponentsInChildren<TextMeshProUGUI>();
        bed2GaugeLabels = Bed2TempGauge.GetComponentsInChildren<TextMeshProUGUI>();
        co2LevelLabels = CO2LevelGauge.GetComponentsInChildren<TextMeshProUGUI>();
        toggleText = BedToggle.GetComponentsInChildren<TextMeshProUGUI>();

        // get min/max values
        PropertyInfo propInfo = typeof(CarbonDioxideRemediation).GetProperty("Bed1Temperature");
        SetGaugeMinMax(propInfo, bed1GaugeLabels);
        UpdateGauge(Bed1TempDial, bed1GaugeLabels, (float)co2System.Bed1Temperature, highTemp);

        propInfo = typeof(CarbonDioxideRemediation).GetProperty("Bed2Temperature");
        SetGaugeMinMax(propInfo, bed2GaugeLabels);
        UpdateGauge(Bed2TempDial, bed2GaugeLabels, (float)co2System.Bed2Temperature, highTemp);

        propInfo = typeof(CarbonDioxideRemediation).GetProperty("Co2Level");
        SetGaugeMinMax(propInfo, co2LevelLabels);
        UpdateGauge(CO2LevelDial, co2LevelLabels, (float)co2System.Co2Level, maxCO2);

        StartCoroutine(UpdateCO2());
    }

    void SetGaugeMinMax(PropertyInfo propInfo, TextMeshProUGUI[] labels)
    {
        IdealRangeAttribute idealRange = (IdealRangeAttribute)Attribute.GetCustomAttribute(propInfo, typeof(IdealRangeAttribute));
        labels[2].text = idealRange.IdealMaximum.ToString();
        labels[3].text = idealRange.IdealMinimum.ToString();
    }

    void UpdateGauge(Image dial, TextMeshProUGUI[] labels, float value, float maxValue)
    {
        labels[1].text = value.ToString();
        dial.fillAmount = (value / (maxValue * 1.0f)) * .75f;
    }

    IEnumerator UpdateCO2()
    {
        while (true)
        {
            co2System.ProcessData();
            UpdateGauge(Bed1TempDial, bed1GaugeLabels, (float)co2System.Bed1Temperature, highTemp);
            UpdateGauge(Bed2TempDial, bed2GaugeLabels, (float)co2System.Bed2Temperature, highTemp);
            UpdateGauge(CO2LevelDial, co2LevelLabels, (float)co2System.Co2Level, maxCO2);
            CO2Tank.fillAmount = CO2TankSource.fillAmount;
			
            if(co2System.Status == SystemStatus.Processing)
            {
                RotateFan();
                ChangeActiveBed();
            }

            yield return new WaitForSeconds(.5f);

        }
    }

    void RotateFan()
    {
        if (fanCounter == 0)
        {
            diagramImages[8].transform.rotation = Quaternion.Euler(0, 0, 22.5f);
            fanCounter++;
        }
        else if (fanCounter == 1)
        {
            diagramImages[8].transform.rotation = Quaternion.Euler(0, 0, 45f);
            fanCounter++;
        }
        else if (fanCounter == 2)
        {
            diagramImages[8].transform.rotation = Quaternion.Euler(0, 0, 67.5f);
            fanCounter++;
        }
        else
        {
            diagramImages[8].transform.rotation = Quaternion.Euler(0, 0, 0);
            fanCounter = 0;
        }
    }

    void ChangeActiveBed()
    {
        // bed 1 is active
        if (co2System.AbsorbingBed == BedOptions.Bed1)
        {
            Bed1DeActivate();
            toggleImages[0].enabled = false;
            toggleImages[1].enabled = true;
            toggleText[1].color = black;
            toggleText[2].color = white; 
        }
        if (co2System.AbsorbingBed == BedOptions.Bed2)
        {
            Bed2DeActivate();
            toggleImages[0].enabled = true;
            toggleImages[1].enabled = false;
            toggleText[1].color = white;
            toggleText[2].color = black;
        }
    }

    void Bed1DeActivate()
    {
        // rotate valve positions
        diagramImages[1].transform.rotation = Quaternion.Euler(0, 0, -90);
        diagramImages[2].transform.rotation = Quaternion.Euler(0, 0, -90);
        diagramImages[3].transform.rotation = Quaternion.Euler(0, 0, -90);

        // toggle bed1 heater to 'on'
        diagramImages[4].color = clearRed;
        diagramImages[6].color = opaqueRed;
    }

    void Bed2DeActivate()
    {
        // rotate valve positions
        diagramImages[1].transform.rotation = Quaternion.Euler(0, 0, 0);
        diagramImages[2].transform.rotation = Quaternion.Euler(0, 0, 0);
        diagramImages[3].transform.rotation = Quaternion.Euler(0, 0, 0);

        // toggle bed2 heater to 'on'
        diagramImages[4].color = opaqueRed;
        diagramImages[6].color = clearRed;
    }

    void ChangeCrewedStatus()
    {
        co2System.ChangeCrewedStatus();
    }

    public void ToggleManualMode()
    {
        co2System.IsManualMode = !co2System.IsManualMode;
    }

    public void ToggleSystemState()
    {
        if(co2System.Status == SystemStatus.Standby)
        {
            co2System.Status = SystemStatus.Processing;
        }
        else
        {
            co2System.Status = SystemStatus.Standby;
        }
    }

    public void ToggleBed()
    {
        if(co2System.AbsorbingBed == BedOptions.Bed1)
        {
            co2System.AbsorbingBed = BedOptions.Bed2;
        }
        else
        {
            co2System.AbsorbingBed = BedOptions.Bed1;
        }
        ChangeActiveBed();
    }
}
