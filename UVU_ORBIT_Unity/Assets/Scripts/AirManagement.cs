using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirManagement : MonoBehaviour
{
    public GameObject Oxygen;
    public GameObject CO2;
    public GameObject ToggleButton;

    private void OnEnable()
    {
        CO2.SetActive(false);
        Oxygen.SetActive(true);
        ToggleButton.SetActive(true);
    }
}
