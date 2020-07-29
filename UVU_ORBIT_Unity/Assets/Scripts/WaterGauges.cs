using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Orbit.Models;
using Orbit.Annotations;
using System;
using System.Reflection;

public class WaterGauges : MonoBehaviour
{
    UrineSystemData weewee = new UrineSystemData();
    WasteWaterStorageTankData dirtyTank = new WasteWaterStorageTankData();
    WaterProcessorData water = new WaterProcessorData();

    public GameObject DistillerTempGauge;
    public Image DistillerTempDial;
    TextMeshProUGUI[] distillerTempLabels;
    float dIdealMaxTemp;
    float dIdealMinTemp;

    public GameObject DistillerSpeedGauge;
    public Image DistillerSpeedDial;
    TextMeshProUGUI[] distillerSpeedLabels;
    float dIdealMaxSpeed;
    float dIdealMinSpeed;

    public GameObject CatalyticHeaterGauge;
    public Image CatalyticTempDial;
    TextMeshProUGUI[] catalyticTempLabels;
    float cIdealTempMax;
    float cIdealTempMin;

    public Image BlackTank;
    public TextMeshProUGUI BlackTankLabel;
    public Image BrineTank;
    public TextMeshProUGUI BrineTankLabel;
    public Image GrayTank;
    public TextMeshProUGUI GrayTankLabel;
    public Image CleanTank;
    public TextMeshProUGUI CleanTankLabel;

    // Start is called before the first frame update
    void Start()
    {
        // initialize systems with start data
        weewee.SeedData();
        dirtyTank.SeedData();
        water.SeedData();

        // get gauge labels: [0] = Label, [1] = Value, [2] = Max, [3] = Min
        distillerTempLabels = DistillerTempGauge.GetComponentsInChildren<TextMeshProUGUI>();
        distillerSpeedLabels = DistillerSpeedGauge.GetComponentsInChildren<TextMeshProUGUI>();
        catalyticTempLabels = CatalyticHeaterGauge.GetComponentsInChildren<TextMeshProUGUI>();

        // get min and max values for each gauge
        // Distiller Temperature
        PropertyInfo propInfo = typeof(UrineSystemData).GetProperty("DistillerTemp");
        IdealRangeAttribute idRangeAtt = (IdealRangeAttribute)Attribute.GetCustomAttribute(propInfo, typeof(IdealRangeAttribute));
        dIdealMinTemp = (float)idRangeAtt.IdealMinimum;
        dIdealMaxTemp = (float)idRangeAtt.IdealMaximum;

        // 2) display values
        //Debug.Log("Distiller Temp =  " + weewee.DistillerTemp);
        distillerTempLabels[1].text = weewee.DistillerTemp.ToString();
        distillerTempLabels[2].text = dIdealMaxTemp.ToString();
        distillerTempLabels[3].text = dIdealMinTemp.ToString();
        DistillerTempDial.fillAmount = UpdateDistillerTemp();

        // Distiller Speed
        propInfo = typeof(UrineSystemData).GetProperty("DistillerSpeed");
        idRangeAtt = (IdealRangeAttribute)Attribute.GetCustomAttribute(propInfo, typeof(IdealRangeAttribute));
        dIdealMinSpeed = (float)idRangeAtt.IdealMinimum;
        dIdealMaxSpeed = (float)idRangeAtt.IdealMaximum;

        // 2) display values
        distillerSpeedLabels[1].text = weewee.DistillerSpeed.ToString();
        distillerSpeedLabels[2].text = dIdealMaxSpeed.ToString();
        distillerSpeedLabels[3].text = dIdealMinSpeed.ToString();
        DistillerSpeedDial.fillAmount = UpdateDistillerSpeed();

        // Catalytic Heater Temperature
        // 1) get values
        propInfo = typeof(WaterProcessorData).GetProperty("PostHeaterTemp");
        idRangeAtt = (IdealRangeAttribute)Attribute.GetCustomAttribute(propInfo, typeof(IdealRangeAttribute));
        cIdealTempMin = (float)idRangeAtt.IdealMinimum;
        cIdealTempMax = (float)idRangeAtt.IdealMaximum;

        // 2) display values
        catalyticTempLabels[1].text = water.PostHeaterTemp.ToString();
        catalyticTempLabels[2].text = cIdealTempMax.ToString();
        catalyticTempLabels[3].text = cIdealTempMin.ToString();
        CatalyticTempDial.fillAmount = UpdateCatalyticTemp();

        StartCoroutine("UpdateWater");
    }

    IEnumerator UpdateWater()
    {
        while (true)
        {
            weewee.ProcessData(dirtyTank.Level);
            dirtyTank.ProcessData(weewee.Status, water.SystemStatus);
            water.ProcessData(dirtyTank.Level);

            distillerTempLabels[1].text = weewee.DistillerTemp.ToString();
            distillerSpeedLabels[1].text = weewee.DistillerSpeed.ToString();
            catalyticTempLabels[1].text = water.PostHeaterTemp.ToString();

            DistillerTempDial.fillAmount = UpdateDistillerTemp();
            DistillerSpeedDial.fillAmount = UpdateDistillerSpeed();
            CatalyticTempDial.fillAmount = UpdateCatalyticTemp();

            BlackTankLabel.text = weewee.UrineTankLevel.ToString();
            BlackTank.fillAmount = (float)weewee.UrineTankLevel / 100;
            BrineTankLabel.text = weewee.BrineTankLevel.ToString();
            BrineTank.fillAmount = (float)weewee.BrineTankLevel / 100;
            GrayTankLabel.text = dirtyTank.Level.ToString();
            GrayTank.fillAmount = (float)dirtyTank.Level / 100;
            CleanTankLabel.text = water.ProductTankLevel.ToString();
            CleanTank.fillAmount = (float)water.ProductTankLevel / 100;

            yield return new WaitForSeconds(.5f);
        }
    }


    float UpdateDistillerTemp()
    {
        return (float)(weewee.DistillerTemp / (dIdealMaxTemp * 1.1f)) * .75f;
    }

    float UpdateDistillerSpeed()
    {
        return (float)(weewee.DistillerSpeed / (dIdealMaxSpeed * 1.1f)) * .75f;
    }

    float UpdateCatalyticTemp()
    {
        return (float)(water.PostHeaterTemp / (cIdealTempMax * 1.1f)) * .75f;
    }

    public void ToggleProcessorOnOff()
    {
        if (water.SystemStatus == SystemStatus.Standby)
        {
            water.SystemStatus = SystemStatus.Processing;
        }
        else
        {
            water.SystemStatus = SystemStatus.Standby;
        }
    }

    public void ToggleDistillerOnOff()
    {
        if(weewee.DistillerOn == false)
        {
            weewee.DistillerOn = true;
        }
        else
        {
            weewee.DistillerOn = false;
        }
    }

    void ChangeCrewedStatus()
    {
        weewee.ChangeCrewedStatus();
        water.ChangeCrewedStatus();
    }
}
