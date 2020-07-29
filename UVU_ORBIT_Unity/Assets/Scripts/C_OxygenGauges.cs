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
    Image[] childCells;

    public Image WaterStorage;
    public TextMeshProUGUI waterStorageLabel;
    public Image h2Storage;
    public TextMeshProUGUI h2StorageLabel;

    // get water tank data from WaterManagement
    public Image FreshTankLevel;
    public TextMeshProUGUI FreshTankValue;
    // get h2 tank data from SabatierManagement
    public Image SabatierH2Tank;
    public TextMeshProUGUI SabatierH2TankValue;

    OxygenGenerator o2generator = new OxygenGenerator();

    // Start is called before the first frame update
    void Start()
    {
        CrewedStatusSource.ChangeCrewedStatus += ChangeCrewedStatus;
        o2generator.SeedData();

        levelLabels = O2LevelGauge.GetComponentsInChildren<TextMeshProUGUI>();
        outputLabels = O2OutputGauge.GetComponentsInChildren<TextMeshProUGUI>();
        childCells = OxygenCells.GetComponentsInChildren<Image>();

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

            WaterStorage.fillAmount = FreshTankLevel.fillAmount;
            waterStorageLabel.text = FreshTankValue.text;
            h2Storage.fillAmount = SabatierH2Tank.fillAmount;
            h2StorageLabel.text = SabatierH2TankValue.text;
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

    void UpdateCells()
    {
        Color c;
        if (childCells.Length > 0)
        {
            for (int i = 1; i < o2generator.NumActiveCells; i++)
            {
                c = childCells[i].color;
                c.a = 1;
                childCells[i].color = c;
            }
            for (int i = (o2generator.NumActiveCells); i < 11; i++)
            {
                c = childCells[i].color;
                c.a = 0;
                childCells[i].color = c;
            }
        }
        else
        {
            for (int i = 1; i < 11; i++)
            {
                c = childCells[i].color;
                c.a = 0;
                childCells[i].color = c;
            }
        }
    }

    public void ChangeCrewedStatus()
    {
        o2generator.ChangeCrewedStatus();
    }

    public void ToggleManualMode()
    {
        o2generator.IsManualMode = !o2generator.IsManualMode;
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
        if (o2generator.NumActiveCells < 10)
        {
            o2generator.NumActiveCells += 1;
        }
    }

    public void DecreaseCells()
    {
        if (o2generator.NumActiveCells > 1)
        {
            o2generator.NumActiveCells -= 1;
        }
    }
}
