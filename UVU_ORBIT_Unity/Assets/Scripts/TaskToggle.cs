using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskToggle : MonoBehaviour
{
    public GameObject ClosedDropdown;
    public GameObject OpenDropdown;
    public GameObject CheckAll;
    public GameObject Checkbox1;
    public GameObject Checkbox2;
    public GameObject Checkbox3;
    public Button ToggleDownstate;
    public Button CheckAllButton;
    public Button CheckButton1;
    public Button CheckButton2;
    public Button CheckButton3;

    bool dropDownOpen = false;
    bool checkall = false;
    bool check1 = false;
    bool check2 = false;
    bool check3 = false;

    // Start is called before the first frame update
    void Start()
    {
        ClosedDropdown.SetActive(true);
        OpenDropdown.SetActive(false);
        CheckAll.SetActive(false);
        Checkbox1.SetActive(false);
        Checkbox2.SetActive(false);
        Checkbox3.SetActive(false);
        CheckButton1.gameObject.SetActive(false);
        CheckButton2.gameObject.SetActive(false);
        CheckButton3.gameObject.SetActive(false);
    }

    public void ToggleDropdown()
    {
        // currently showing closed state; toggle to open, show last state of checkboxes 
        if (ClosedDropdown.activeInHierarchy)
        {
            dropDownOpen = true;
            ClosedDropdown.SetActive(false);
            OpenDropdown.SetActive(true);
            CheckAllButton.gameObject.SetActive(true);
            CheckButton1.gameObject.SetActive(true);
            CheckButton2.gameObject.SetActive(true);
            CheckButton3.gameObject.SetActive(true);

            if (checkall)
            {
                CheckAll.SetActive(true);
                Checkbox1.SetActive(true);
                Checkbox2.SetActive(true);
                Checkbox3.SetActive(true);
                return;
            }
            else
            {                
                if (check1)
                    Checkbox1.SetActive(true);
                else
                    Checkbox1.SetActive(false);
            
            
                if (check2)
                    Checkbox2.SetActive(true);
                else
                    Checkbox2.SetActive(false);
            
            
                if (check1)
                    Checkbox3.SetActive(true);
                else
                    Checkbox3.SetActive(false);
            }
        }
        // dropdown open. close it
        else
        {
            dropDownOpen = false;
            OpenDropdown.SetActive(false);
            Checkbox1.SetActive(false);
            Checkbox2.SetActive(false);
            Checkbox3.SetActive(false);
            CheckButton1.gameObject.SetActive(false);
            CheckButton2.gameObject.SetActive(false);
            CheckButton3.gameObject.SetActive(false);

            CheckButton1.enabled = false;
            CheckButton2.enabled = false;
            CheckButton3.enabled = false;

            ClosedDropdown.SetActive(true);
        }
    }

    public void ToggleCheckbox1()
    {
        if (Checkbox1.activeInHierarchy)
        {
            check1 = false;
            checkall = false;
            Checkbox1.SetActive(false);
            CheckForAll();
        }
        else
        {
            check1 = true;
            Checkbox1.SetActive(true);
            CheckForAll();
        }
    }

    public void ToggleCheckbox2()
    {
        if (Checkbox2.activeInHierarchy)
        {
            check2 = false;
            checkall = false;
            Checkbox2.SetActive(false);
            CheckForAll();
        }
        else
        {
            check2 = true;
            Checkbox2.SetActive(true);
            CheckForAll();
        }
    }

    public void ToggleCheckbox3()
    {
        if (Checkbox3.activeInHierarchy)
        {
            check3 = false;
            checkall = false;
            Checkbox3.SetActive(false);
            CheckForAll();
        }
        else
        {
            check3 = true;
            Checkbox3.SetActive(true);
            CheckForAll();
        }
    }

    public void ToggleCheckAll()
    {
        if (CheckAll.activeInHierarchy)
        {
            checkall = false;
            CheckAll.SetActive(false);
            check1 = false;
            Checkbox1.SetActive(false);
            check2 = false;
            Checkbox2.SetActive(false);
            check3 = false;
            Checkbox3.SetActive(false);
        }
        else
        {
            checkall = true;
            CheckAll.SetActive(true);
            if (OpenDropdown.activeInHierarchy)
            {
                Checkbox1.SetActive(true);
                Checkbox2.SetActive(true);
                Checkbox3.SetActive(true);
            }
            check1 = true;
            check2 = true;
            check3 = true;
        }
    }

    void CheckForAll()
    {
        if (Checkbox1.activeInHierarchy && Checkbox2.activeInHierarchy && Checkbox3.activeInHierarchy)
        {
            checkall = true;
            CheckAll.SetActive(true);
        }
        else
            CheckAll.SetActive(false);
    }

    void ResetDropdown()
    {
        ClosedDropdown.SetActive(true);
        OpenDropdown.SetActive(false);
        CheckAll.SetActive(false);
        Checkbox1.SetActive(false);
        Checkbox2.SetActive(false);
        Checkbox3.SetActive(false);
        CheckButton1.gameObject.SetActive(false);
        CheckButton2.gameObject.SetActive(false);
        CheckButton3.gameObject.SetActive(false);
    }
}
