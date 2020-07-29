using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_SystemNavigation : MonoBehaviour
{
    public GameObject Cabin;
    public GameObject Power;
    public GameObject Oxygen;
    public GameObject CarbonDioxide;
    public GameObject Water;
    public GameObject Sabatier;
    public GameObject Thermal;
    public GameObject Downstates;
    public GameObject ButtonPanel;

    Image[] downstates;
    Image[] buttons;
    bool systemsOn = true;

    private void Start()
    {
        buttons = ButtonPanel.GetComponentsInChildren<Image>();
        downstates = Downstates.GetComponentsInChildren<Image>();
        GetCabin();
    }

    private void OnEnable()
    {
        //ResetButtons();
        Cabin.SetActive(true);
        //downstates[0].enabled = true;
    }

    public void GetCabin()
    {
        DisableAllSystems();
        ResetButtons();
        Cabin.SetActive(true);
        downstates[0].enabled = true;
    }

    public void GetPower()
    {
        DisableAllSystems();
        ResetButtons();
        Power.SetActive(true);
        downstates[1].enabled = true;
    }

    public void GetOxygen()
    {
        DisableAllSystems();
        ResetButtons();
        Oxygen.SetActive(true);
        downstates[2].enabled = true;
    }
    public void GetCarbonDioxide()
    {
        DisableAllSystems();
        ResetButtons();
        CarbonDioxide.SetActive(true);
        downstates[3].enabled = true;
    }

    public void GetWater()
    {
        DisableAllSystems();
        ResetButtons();
        Water.SetActive(true);
        downstates[4].enabled = true;
    }
    public void GetSabatier()
    {
        DisableAllSystems();
        ResetButtons();
        Sabatier.SetActive(true);
        downstates[5].enabled = true;
    }

    public void GetThermal()
    {
        DisableAllSystems();
        ResetButtons();
        Thermal.SetActive(true);
        downstates[6].enabled = true;
    }

    public void DisableAllSystems()
    {
        Cabin.SetActive(false);
        Oxygen.SetActive(false);
        CarbonDioxide.SetActive(false);
        Thermal.SetActive(false);
        Power.SetActive(false);
        Water.SetActive(false);
        Sabatier.SetActive(false);
    }

    public void SystemsPanelOnOff()
    {
        if (systemsOn)
        {
            DisableAllSystems();
            ResetButtons();
            systemsOn = false;
        }
        else
        {
            GetCabin();
            downstates[0].enabled = true;
            systemsOn = true;
        }
    }

    void ResetButtons()
    {
        for (int i = 0; i < downstates.Length; i++)
        {
            downstates[i].enabled = false;
            buttons[i].enabled = true;
        }
    }
}
