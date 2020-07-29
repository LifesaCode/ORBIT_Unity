using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemToggle : MonoBehaviour
{
    public GameObject PrimarySystem;
    public GameObject SecondarySystem;

    public void ToggleSystems()
    {
        Debug.Log("Toggle button pressed");
       
        if(PrimarySystem.activeInHierarchy == true)
        {
            Debug.Log("Make Secondary System Active");
            PrimarySystem.SetActive(false);
            SecondarySystem.SetActive(true);
        }
        else
        {
            Debug.Log("Make Primary System Active");
            SecondarySystem.SetActive(false);
            PrimarySystem.SetActive(true);
        }
    }
}
