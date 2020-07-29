using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomMenuToggle : MonoBehaviour
{
    //public GameObject StationSystems;
    public GameObject StationNavigation;
    public GameObject Tasks;
    public GameObject Communication;
    public GameObject Suit;

    SystemNavigation systems;

    // Start is called before the first frame update
    void Start()
    {
        //CollapseAll();   
    }

    public void ToggleSystemNavigation()
    {
        if (StationNavigation.activeInHierarchy)
        {
            StationNavigation.SetActive(false);
        }
        else
        {
            StationNavigation.SetActive(true);
        }
    }

    public void ToggleTasks()
    {
        if (Tasks.activeInHierarchy)
        {
            Tasks.SetActive(false);
        }
        else
        {
            Tasks.SetActive(true);
        }
    }

    public void ToggleCommunication()
    {
        if (Communication.activeInHierarchy)
        {
            Communication.SetActive(false);
        }
        else
        {
            Communication.SetActive(true);
        }
    }

    public void ToggleSuit()
    {
        if (Suit.activeInHierarchy)
        {
            Suit.SetActive(false);
        }
        else
        {
            Suit.SetActive(true);
        }
    }

    void CollapseAll()
    {
        //StationSystems.SetActive(false);
        Tasks.SetActive(false);
        Communication.SetActive(false);
        Suit.SetActive(false);
    }
    
}
