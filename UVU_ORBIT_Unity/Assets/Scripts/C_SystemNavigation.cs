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

    public Canvas CabinDynamicElements;
    public Canvas PowerDynamicElements;
    public Canvas OxygenDynamicElements;
    public Canvas Co2DynamicElements;
    public Canvas WaterDynamicElements;
    public Canvas SabatierDynamicElements;
    public Canvas ThermalDynamicElements;


    private void Start()
    {
        buttons = ButtonPanel.GetComponentsInChildren<Image>();
        downstates = Downstates.GetComponentsInChildren<Image>();
        GetCabin();
    }

    private void OnEnable()
    {
       // Cabin.SetActive(true);
    }

    public void GetCabin()
    {
        DisableAllSystems();
        ResetButtons();
        CabinDynamicElements.enabled = true;
        Cabin.SetActive(true);
        downstates[0].enabled = true;
    }

    public void GetPower()
    {
        DisableAllSystems();
        ResetButtons();
        PowerDynamicElements.enabled = true;
        Power.SetActive(true);
        downstates[1].enabled = true;
    }

    public void GetOxygen()
    {
        DisableAllSystems();
        ResetButtons();
        OxygenDynamicElements.enabled = true;
        Oxygen.SetActive(true);
        downstates[2].enabled = true;
    }
    public void GetCarbonDioxide()
    {
        DisableAllSystems();
        ResetButtons();
        Co2DynamicElements.enabled = true;
        CarbonDioxide.SetActive(true);
        downstates[3].enabled = true;
    }

    public void GetWater()
    {
        DisableAllSystems();
        ResetButtons();
        WaterDynamicElements.enabled = true;
        Water.SetActive(true);
        downstates[4].enabled = true;
    }
    public void GetSabatier()
    {
        DisableAllSystems();
        ResetButtons();
        SabatierDynamicElements.enabled = true;
        Sabatier.SetActive(true);
        downstates[5].enabled = true;
    }

    public void GetThermal()
    {
        DisableAllSystems();
        ResetButtons();
        ThermalDynamicElements.enabled = true;
        Thermal.SetActive(true);
        downstates[6].enabled = true;
    }

    public void DisableAllSystems()
    {
        CabinDynamicElements.enabled = false;
        PowerDynamicElements.enabled = false;
        OxygenDynamicElements.enabled = false;
        Co2DynamicElements.enabled = false;
        WaterDynamicElements.enabled = false;
        SabatierDynamicElements.enabled = false;
        ThermalDynamicElements.enabled = false;

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
