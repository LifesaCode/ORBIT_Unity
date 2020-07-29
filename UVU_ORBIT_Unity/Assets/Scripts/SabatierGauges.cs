using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Orbit.Models;
using Orbit.Annotations;
using System;
using System.Reflection;

public class SabatierGauges : MonoBehaviour
{
    public WaterGeneratorData sabatier = new WaterGeneratorData();

    public GameObject ReactorTempGauge;
    public Image ReactorTempDial;
    TextMeshProUGUI[] reactorTempLabels;
    float rIdealMaxTemp;
    float rIdealMinTemp;

    public GameObject SeperatorSpeedGauge;
    public Image SeperatorSpeedDial;
    TextMeshProUGUI[] seperatorSpeedLabels;
    float sIdealMaxSpeed;
    float sIdealMinSpeed;

    public Image CO2TankLevel;
    public TextMeshProUGUI CO2TankLabel;
    public Image CH4TankLevel;
    public TextMeshProUGUI CH4TankLabel;
    public Image H2TankLevel;
    public TextMeshProUGUI H2TankLabel;


    // Start is called before the first frame update
    void Start()
    {
        sabatier.SeedData();

        // get gauge labels: [0] = Label, [1] = Value, [2] = Max, [3] = Min
        reactorTempLabels = ReactorTempGauge.GetComponentsInChildren<TextMeshProUGUI>();
        seperatorSpeedLabels = SeperatorSpeedGauge.GetComponentsInChildren<TextMeshProUGUI>();

        // get min and max values for each gauge
        // Distiller Temperature
        PropertyInfo propInfo = typeof(WaterGeneratorData).GetProperty("ReactorTemp");
        IdealRangeAttribute idRangeAtt = (IdealRangeAttribute)Attribute.GetCustomAttribute(propInfo, typeof(IdealRangeAttribute));
        rIdealMinTemp = (float)idRangeAtt.IdealMinimum;
        rIdealMaxTemp = (float)idRangeAtt.IdealMaximum;

        // 2) display values
        //Debug.Log("Distiller Temp =  " + weewee.DistillerTemp);
        reactorTempLabels[1].text = sabatier.ReactorTemp.ToString();
        reactorTempLabels[2].text = rIdealMaxTemp.ToString();
        reactorTempLabels[3].text = rIdealMinTemp.ToString();
        ReactorTempDial.fillAmount = UpdateReactorTemp();

        // Distiller Speed
        propInfo = typeof(WaterGeneratorData).GetProperty("SeperatorMotorSpeed");
        idRangeAtt = (IdealRangeAttribute)Attribute.GetCustomAttribute(propInfo, typeof(IdealRangeAttribute));
        sIdealMinSpeed = (float)idRangeAtt.IdealMinimum;
        sIdealMaxSpeed = (float)idRangeAtt.IdealMaximum;

        // 2) display values
        // Debug.Log("Distiller Speed = " + weewee.DistillerSpeed); ;

        seperatorSpeedLabels[1].text = sabatier.SeperatorMotorSpeed.ToString();
        seperatorSpeedLabels[2].text = sIdealMaxSpeed.ToString();
        seperatorSpeedLabels[3].text = sIdealMinSpeed.ToString();
        SeperatorSpeedDial.fillAmount = UpdateSeperatorSpeed();

        StartCoroutine("UpdateSabatier");
    }

    IEnumerator UpdateSabatier()
    {
        while (true)
        {
            sabatier.ProcessData();

            reactorTempLabels[1].text = sabatier.ReactorTemp.ToString();
            seperatorSpeedLabels[1].text = sabatier.SeperatorMotorSpeed.ToString();

            ReactorTempDial.fillAmount = UpdateReactorTemp();
            SeperatorSpeedDial.fillAmount = UpdateSeperatorSpeed();

            CO2TankLabel.text = sabatier.Co2StoreLevel.ToString();
            CO2TankLevel.fillAmount = (float)sabatier.Co2StoreLevel / 100;
            CH4TankLabel.text = sabatier.MethaneStoreLevel.ToString();
            CH4TankLevel.fillAmount = (float)sabatier.MethaneStoreLevel / 100;
            H2TankLabel.text = sabatier.H2StoreLevel.ToString();
            H2TankLevel.fillAmount = (float)sabatier.H2StoreLevel / 100;

            yield return new WaitForSeconds(.5f);
        }
    }

    float UpdateReactorTemp()
    {
        return (float)(sabatier.ReactorTemp / (rIdealMaxTemp * 1.1)) * .75f;
    }

    float UpdateSeperatorSpeed()
    {
        return (float)(sabatier.SeperatorMotorSpeed / (sIdealMaxSpeed * 1.1)) * .75f;
    }
}
