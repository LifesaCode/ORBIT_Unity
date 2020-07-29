using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Orbit.Models;
using Orbit.Annotations;
using System;
using System.Reflection;

public class CO2Gauges : MonoBehaviour
{
    public GameObject Bed1TempGauge;
    public Image Bed1TempDial;
    public GameObject Bed2TempGauge;
    public Image Bed2TempDial;
    public GameObject CO2LevelGauge;
    public Image CO2LevelDial;
	public Image CO2Tank;
	public Image CO2TankSource;

    public GameObject Diagram;

    SpriteRenderer[] diagramSprites;
    TextMeshProUGUI[] bed1GaugeLabels;
    TextMeshProUGUI[] bed2GaugeLabels;
    TextMeshProUGUI[] co2LevelLabels;
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

    CarbonDioxideRemediation co2System = new CarbonDioxideRemediation();
    
    // Start is called before the first frame update
    void Start()
    {
        co2System.SeedData();

        // get gauge label components
        diagramSprites = Diagram.GetComponentsInChildren<SpriteRenderer>();
        
        bed1GaugeLabels = Bed1TempGauge.GetComponentsInChildren<TextMeshProUGUI>();
        bed2GaugeLabels = Bed2TempGauge.GetComponentsInChildren<TextMeshProUGUI>();
        co2LevelLabels = CO2LevelGauge.GetComponentsInChildren<TextMeshProUGUI>();

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
			// Debug.Log("CO2 System Status = " + co2System.Status);
			// Debug.Log("CO2 Level = " + co2System.Co2Level);
            UpdateGauge(Bed1TempDial, bed1GaugeLabels, (float)co2System.Bed1Temperature, highTemp);
			// Debug.Log("Bed 1 Temp = " + co2System.Bed1Temperature);
            UpdateGauge(Bed2TempDial, bed2GaugeLabels, (float)co2System.Bed2Temperature, highTemp);
			// Debug.Log("Bed 2 Temp = " + co2System.Bed2Temperature);
            UpdateGauge(CO2LevelDial, co2LevelLabels, (float)co2System.Co2Level, maxCO2);
			CO2Tank.fillAmount = CO2TankSource.fillAmount;

            RotateFan();
            CheckActiveBed();

            yield return new WaitForSeconds(.5f);

        }
    }

    void RotateFan()
    {
        if(fanCounter == 0)
        {
            diagramSprites[8].transform.rotation = Quaternion.Euler(0, 0, -22.5f);
            fanCounter++;
        }
        else if(fanCounter == 1)
        {
            diagramSprites[8].transform.rotation = Quaternion.Euler(0, 0, -45f);
            fanCounter++;
        }
        else if(fanCounter == 2)
        {
            diagramSprites[8].transform.rotation = Quaternion.Euler(0, 0, -67.5f);
            fanCounter++;
        }
        else
        {
            diagramSprites[8].transform.rotation = Quaternion.Euler(0, 0, 0);
            fanCounter = 0;
        }
    }

    void CheckActiveBed()
    {
        // bed 1 is active
        if(co2System.AbsorbingBed == BedOptions.Bed1)
        {
            Bed1Activate();
        }
        if(co2System.AbsorbingBed == BedOptions.Bed2)
        {
            Bed2Activate();
        }
    }

    void Bed1Activate()
    {
        diagramSprites[1].transform.rotation = Quaternion.Euler(0, 0, -90);
        diagramSprites[2].transform.rotation = Quaternion.Euler(0, 0, -90);
        diagramSprites[3].transform.rotation = Quaternion.Euler(0, 0, -90);
        diagramSprites[4].color = clearRed;
        diagramSprites[6].color = opaqueRed;
    }

    void Bed2Activate()
    {
        diagramSprites[1].transform.rotation = Quaternion.Euler(0, 0, 0);
        diagramSprites[2].transform.rotation = Quaternion.Euler(0, 0, 0);
        diagramSprites[3].transform.rotation = Quaternion.Euler(0, 0, 0);
        diagramSprites[4].color = opaqueRed;
        diagramSprites[6].color = clearRed;
    }

    void ChangeCrewedStatus()
    {
        co2System.ChangeCrewedStatus();
    }
}
