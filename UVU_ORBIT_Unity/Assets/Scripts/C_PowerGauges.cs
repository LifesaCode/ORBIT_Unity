using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Orbit.Models;
using Orbit.Annotations;
using System;
using System.Reflection;


public class C_PowerGauges : MonoBehaviour
{
    public GameObject BatteryLevelGauge;
    public Image BatteryLevelDial;
    public GameObject BatteryTempGauge;
    public Image BatteryTempDial;
    public GameObject BatteryVoltageGauge;
    public Image BatteryVoltageDial;
    public GameObject SolarVoltageGauge;
    public Image SolarVoltageDial;
    public TextMeshProUGUI SolarRotation;
    public TextMeshProUGUI EclipseTimer;
    public GameObject BatteryChargeImages;
    public GameObject SolarArrayFill;

    Image[] batteryChargeImages;
    Image[] solarFill;
    TextMeshProUGUI[] batteryLevelLabels;
    TextMeshProUGUI[] batteryTempLabels;
    TextMeshProUGUI[] batteryVoltageLabels;
    TextMeshProUGUI[] solarVoltageLabels;

    float maxBatteryLevel = 104;
    float maxBatteryTemp = 20;
    float maxBatteryVoltage = 170;
    float maxSolarVoltage = 180;
    int chargeCount = 0;
    int fillCount = 0;

    PowerSystemData power = new PowerSystemData();

    // Start is called before the first frame update
    void Start()
    {
        power.SeedData();

        solarFill = SolarArrayFill.GetComponentsInChildren<Image>();
        batteryChargeImages = BatteryChargeImages.GetComponentsInChildren<Image>();
        batteryLevelLabels = BatteryLevelGauge.GetComponentsInChildren<TextMeshProUGUI>();
        batteryTempLabels = BatteryTempGauge.GetComponentsInChildren<TextMeshProUGUI>();
        batteryVoltageLabels = BatteryVoltageGauge.GetComponentsInChildren<TextMeshProUGUI>();
        solarVoltageLabels = SolarVoltageGauge.GetComponentsInChildren<TextMeshProUGUI>();

        PropertyInfo propInfo = typeof(PowerSystemData).GetProperty("BatteryChargeLevel");
        SetGaugeMinMax(propInfo, batteryLevelLabels);
        UpdateGauge(BatteryLevelDial, batteryLevelLabels, (float)power.BatteryChargeLevel, maxBatteryLevel);

        propInfo = typeof(PowerSystemData).GetProperty("BatteryTemperature");
        SetGaugeMinMax(propInfo, batteryTempLabels);
        UpdateGauge(BatteryTempDial, batteryTempLabels, (float)power.BatteryTemperature, maxBatteryTemp);

        propInfo = typeof(PowerSystemData).GetProperty("BatteryVoltage");
        SetGaugeMinMax(propInfo, batteryVoltageLabels);
        UpdateGauge(BatteryVoltageDial, batteryVoltageLabels, (float)power.BatteryVoltage, maxBatteryVoltage);

        propInfo = typeof(PowerSystemData).GetProperty("SolarArrayVoltage");
        SetGaugeMinMax(propInfo, solarVoltageLabels);
        UpdateGauge(SolarVoltageDial, solarVoltageLabels, (float)power.SolarArrayVoltage, maxSolarVoltage);

        SolarRotation.text = power.SolarArrayRotation.ToString();
        EclipseTimer.text = power.eclipseCount.ToString();

        StartCoroutine(UpdatePower());
    }

    void SetGaugeMinMax(PropertyInfo propInfo, TextMeshProUGUI[] labels)
    {
        IdealRangeAttribute idealRange = (IdealRangeAttribute)Attribute.GetCustomAttribute(propInfo, typeof(IdealRangeAttribute));
        labels[2].text = idealRange.IdealMaximum.ToString();
        labels[3].text = idealRange.IdealMinimum.ToString();
    }

    void UpdateGauge(Image dial, TextMeshProUGUI[] labels, float value, float max)
    {
        labels[1].text = value.ToString();
        dial.fillAmount = (value / max * 1.0f) * .75f;
    }

    IEnumerator UpdatePower()
    {
        while (true)
        {
            power.ProcessData();

            UpdateGauge(BatteryLevelDial, batteryLevelLabels, (float)power.BatteryChargeLevel, maxBatteryLevel);
            UpdateGauge(BatteryTempDial, batteryTempLabels, (float)power.BatteryTemperature, maxBatteryTemp);
            UpdateGauge(BatteryVoltageDial, batteryVoltageLabels, (float)power.BatteryVoltage, maxBatteryVoltage);
            UpdateGauge(SolarVoltageDial, solarVoltageLabels, (float)power.SolarArrayVoltage, maxSolarVoltage);

            BatteryLevelAnimate((float)power.BatteryChargeLevel, power.inEclipse);
            SolarActiveAnimate(power.inEclipse);

            SolarRotation.text = power.SolarArrayRotation.ToString();
            EclipseTimer.text = power.eclipseCount.ToString();

            yield return new WaitForSeconds(0.5f);
        }
    }

    void BatteryLevelAnimate(float level, bool inEclipse)
    {
        if (inEclipse || power.IsManualMode)
        {
            ShowDischarging();
        }
        else
        {
            ShowCharging();
        }
    }

    void SolarActiveAnimate(bool inEclipse)
    {
        if (inEclipse || power.IsManualMode)
        {
            if (fillCount < 5)
            {
                switch (fillCount)
                {
                    case 0:
                        fillCount++;
                        ChangeOpacity(solarFill[0], 0);
                        break;
                    case 1:
                        fillCount++;
                        ChangeOpacity(solarFill[1], 0);
                        break;
                    case 2:
                        fillCount++;
                        ChangeOpacity(solarFill[2], 0);
                        break;
                    case 3:
                        fillCount++;
                        ChangeOpacity(solarFill[3], 0);
                        break;
                    default:
                        break;
                }
            }
        }
        else //if (!inEclipse)
        {
            if (fillCount > 0)
            {
                switch (fillCount)
                {
                    case 1:
                        fillCount--;
                        ChangeOpacity(solarFill[0], 1);
                        break;
                    case 2:
                        fillCount--;
                        ChangeOpacity(solarFill[1], 1);
                        break;
                    case 3:
                        fillCount--;
                        ChangeOpacity(solarFill[2], 1); ;
                        break;
                    case 4:
                        fillCount--;
                        ChangeOpacity(solarFill[3], 1);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void ChangeOpacity(Image panel, float opacity)
    {
        Color c = panel.color;
        c.a = opacity;
        panel.color = c;
    }

    void ShowCharging()
    {
        Color c;
        // charge images labeled from left to right with battery positive terminal on the left. 
        switch (chargeCount)
        {
            case 0:
                for (int i = 0; i < batteryChargeImages.Length; i++)
                {
                    c = batteryChargeImages[i].color;
                    c.a = 0;
                    batteryChargeImages[i].color = c;
                }
                chargeCount++;
                break;
            case 1:
                c = batteryChargeImages[3].color;
                c.a = 1;
                batteryChargeImages[3].color = c;
                chargeCount++;
                break;
            case 2:
                c = batteryChargeImages[2].color;
                c.a = 1;
                batteryChargeImages[2].color = c;
                chargeCount++;
                break;
            case 3:
                c = batteryChargeImages[1].color;
                c.a = 1;
                batteryChargeImages[1].color = c;
                chargeCount++;
                break;
            case 4:
                c = batteryChargeImages[0].color;
                c.a = 1;
                batteryChargeImages[0].color = c;
                chargeCount = 0;
                break;
            default:
                break;
        }
    }

    void ShowDischarging()
    {
        Color c;

        switch (chargeCount)
        {
            case 0:
                for (int i = 0; i < batteryChargeImages.Length; i++)
                {
                    c = batteryChargeImages[i].color;
                    c.a = 1;
                    batteryChargeImages[i].color = c;
                }
                chargeCount++;
                break;
            case 1:
                c = batteryChargeImages[0].color;
                c.a = 0;
                batteryChargeImages[0].color = c;
                chargeCount++;
                break;
            case 2:
                c = batteryChargeImages[1].color;
                c.a = 0;
                batteryChargeImages[1].color = c;
                chargeCount++;
                break;
            case 3:
                c = batteryChargeImages[2].color;
                c.a = 0;
                batteryChargeImages[2].color = c;
                chargeCount++;
                break;
            case 4:
                c = batteryChargeImages[3].color;
                c.a = 0;
                batteryChargeImages[3].color = c;
                chargeCount = 0;
                break;
            default:
                break;
        }
    }

    public void ToggleManualMode()
    {
        if (power.IsManualMode) 
        { 
            power.IsManualMode = false; 
        }
        else 
        { 
            power.IsManualMode = true; 
        }
    }
}
