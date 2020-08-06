using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Orbit.Models;
using Orbit.Annotations;
using System;
using System.Reflection;


public class C_OxygenGauges : MonoBehaviour
{
    public GameObject O2LevelGauge;
    public Image O2LevelDial;
    TextMeshProUGUI[] levelLabels;
    float levelIdealMax;
    float levelIdealMin;

    public GameObject O2OutputGauge;
    public Image O2OutputDial;
    TextMeshProUGUI[] outputLabels;
    float outputIdealMax;
    float outputIdealMin;

    public GameObject OxygenCells;
    public TextMeshProUGUI NumCellsLabel;
    Image[] oxygenCells;

    public Image WaterStorage;
    public TextMeshProUGUI waterStorageLabel;
    public Image h2Storage;
    public TextMeshProUGUI h2StorageLabel;

    public GameObject StandbyButton;
    public GameObject GrayOutStandby;

    double freshLevel;
    double h2Level;

    OxygenGenerator o2generator = new OxygenGenerator();

    // Start is called before the first frame update
    void Start()
    {
        CrewedStatusSource.ChangeCrewedStatus += ChangeCrewedStatus;
        o2generator.SeedData();
        C_WaterGauges.OnFreshWaterUpdate += GetFreshLevel;
        C_SabatierGauges.OnH2Update += GetH2Level;

        levelLabels = O2LevelGauge.GetComponentsInChildren<TextMeshProUGUI>();
        outputLabels = O2OutputGauge.GetComponentsInChildren<TextMeshProUGUI>();
        oxygenCells = OxygenCells.GetComponentsInChildren<Image>();

        PropertyInfo propInfo = typeof(OxygenGenerator).GetProperty("OxygenLevel");
        IdealRangeAttribute idRangeAtt = (IdealRangeAttribute)Attribute.GetCustomAttribute(propInfo, typeof(IdealRangeAttribute));
        levelIdealMin = (float)idRangeAtt.IdealMinimum;
        levelIdealMax = (float)idRangeAtt.IdealMaximum;

        levelLabels[1].text = o2generator.OxygenLevel.ToString();
        levelLabels[2].text = levelIdealMax.ToString();
        levelLabels[3].text = levelIdealMin.ToString();
        O2LevelDial.fillAmount = UpdateOxygenLevel();

        propInfo = typeof(OxygenGenerator).GetProperty("SystemOutput");
        idRangeAtt = (IdealRangeAttribute)Attribute.GetCustomAttribute(propInfo, typeof(IdealRangeAttribute));
        outputIdealMin = (float)idRangeAtt.IdealMinimum;
        outputIdealMax = (float)idRangeAtt.IdealMaximum;

        outputLabels[1].text = o2generator.OxygenLevel.ToString();
        outputLabels[2].text = "100%";
        outputLabels[3].text = "0";
        O2OutputDial.fillAmount = UpdateOxygenOutput();

        StartCoroutine("UpdateOxygen");
    }

    IEnumerator UpdateOxygen()
    {
        while (true)
        {
            o2generator.ProcessData();

            levelLabels[1].text = o2generator.OxygenLevel.ToString();
            outputLabels[1].text = o2generator.SystemOutput.ToString();

            O2LevelDial.fillAmount = UpdateOxygenLevel();
            O2OutputDial.fillAmount = UpdateOxygenOutput();

            WaterStorage.fillAmount = (float)freshLevel / 100;
            waterStorageLabel.text = freshLevel.ToString();
            h2Storage.fillAmount = (float)h2Level / 100;
            h2StorageLabel.text = h2Level.ToString();
            NumCellsLabel.text = o2generator.NumActiveCells.ToString();

            UpdateCells();

            yield return new WaitForSeconds(.5f);
        }
    }

    float UpdateOxygenLevel()
    {
        return (float)(o2generator.OxygenLevel / (levelIdealMax * 1.1)) * .75f;
    }

    float UpdateOxygenOutput()
    {
        // 2700 = 10 cells * output of 270L per cell
        return (float)(o2generator.SystemOutput / 2700) * .75f;
    }

    public void GetFreshLevel(double level)
    {
        freshLevel = level;
    }

    public void GetH2Level(double level)
    {
        h2Level = level;
    }


    void UpdateCells()
    {
        Color c;
        int cellson = o2generator.NumActiveCells;
        if (cellson > 0)
        {
            for (int i = 0; i < cellson; i++)
            {
                c = oxygenCells[i].color;
                c.a = 1;
                oxygenCells[i].color = c;
            }
            for (int i = (cellson); i < 10; i++)
            {
                c = oxygenCells[i].color;
                c.a = 0;
                oxygenCells[i].color = c;
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                c = oxygenCells[i].color;
                c.a = 0;
                oxygenCells[i].color = c;
            }
        }
    }

    public void ChangeCrewedStatus()
    {
        o2generator.ChangeCrewedStatus();
    }

    public void ToggleManualMode()
    {
        if (o2generator.IsManualMode)
        {
            o2generator.IsManualMode = false;
            StandbyButton.SetActive(false);
            GrayOutStandby.SetActive(true);
        }
        else
        {
            o2generator.IsManualMode = true;
            StandbyButton.SetActive(true);
            GrayOutStandby.SetActive(false);
        }
    }

    public void ToggleSystemState()
    {
        if(o2generator.Status == SystemStatus.Standby)
        {
            o2generator.Status = SystemStatus.Processing;
        }
        else
        {
            o2generator.Status = SystemStatus.Standby;
			o2generator.NumActiveCells = 0;
        }
    }

    public void IncreaseCells()
    {
        o2generator.IncreaseActiveCells();
        UpdateCells();
    }

    public void DecreaseCells()
    {
        o2generator.DecreaseActiveCells();
        UpdateCells();
    }
}
