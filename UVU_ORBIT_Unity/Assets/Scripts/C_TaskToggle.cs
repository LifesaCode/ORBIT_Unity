using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_TaskToggle : MonoBehaviour
{
    public Image ClosedDropdown;
    public Image OpenDropdown;
    public Image CheckAll;
    public Image Checkbox1;
    public Image Checkbox2;
    public Image Checkbox3;
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
        Debug.Log("TaskToggle start()");
        ClosedDropdown.enabled = (true);
        OpenDropdown.enabled = (false);
        CheckAll.enabled = (false);
        Checkbox1.enabled = (false);
        Checkbox2.enabled = (false);
        Checkbox3.enabled = (false);
        CheckButton1.gameObject.SetActive(false);
        CheckButton2.gameObject.SetActive(false);
        CheckButton3.gameObject.SetActive(false);
    }

    public void ToggleDropdown()
    {
        // currently showing closed state; toggle to open, show last state of checkboxes 
        if (ClosedDropdown.enabled)
        {
            dropDownOpen = true;
            ClosedDropdown.enabled = (false);
            OpenDropdown.enabled = (true);
            CheckButton1.gameObject.SetActive(true);
            CheckButton2.gameObject.SetActive(true);
            CheckButton3.gameObject.SetActive(true);

            if (checkall)
            {
                CheckAll.enabled = (true);
                Checkbox1.enabled = (true);
                Checkbox2.enabled = (true);
                Checkbox3.enabled = (true);
            }
            else
            {
                if (check1)
                    Checkbox1.enabled = (true);
                else
                    Checkbox1.enabled = (false);


                if (check2)
                    Checkbox2.enabled = (true);
                else
                    Checkbox2.enabled = (false);


                if (check3)
                    Checkbox3.enabled = (true);
                else
                    Checkbox3.enabled = (false);
            }
        }
        // dropdown open. close it
        else
        {
            dropDownOpen = false;
            OpenDropdown.enabled = (false);
            ClosedDropdown.enabled = (true);

            // turn off check images
            Checkbox1.enabled = (false);
            Checkbox2.enabled = (false);
            Checkbox3.enabled = (false);

            // disable dropdown checkbox buttons
            CheckButton1.gameObject.SetActive(false);
            CheckButton2.gameObject.SetActive(false);
            CheckButton3.gameObject.SetActive(false);
        }
    }

    public void ToggleCheckbox1()
    {
        if (Checkbox1.enabled)
        {
            check1 = false;
            checkall = false;
            Checkbox1.enabled = (false);
            CheckForAll();
        }
        else
        {
            check1 = true;
            Checkbox1.enabled = (true);
            CheckForAll();
        }
    }

    public void ToggleCheckbox2()
    {
        if (Checkbox2.enabled)
        {
            check2 = false;
            checkall = false;
            Checkbox2.enabled = (false);
            CheckForAll();
        }
        else
        {
            check2 = true;
            Checkbox2.enabled = (true);
            CheckForAll();
        }
    }

    public void ToggleCheckbox3()
    {
        if (Checkbox3.enabled)
        {
            check3 = false;
            checkall = false;
            Checkbox3.enabled = (false);
            CheckForAll();
        }
        else
        {
            check3 = true;
            Checkbox3.enabled = (true);
            CheckForAll();
        }
    }

    public void ToggleCheckAll()
    {
        if (CheckAll.enabled)
        {
            checkall = false;
            CheckAll.enabled = (false);
            check1 = false;
            Checkbox1.enabled = (false);
            check2 = false;
            Checkbox2.enabled = (false);
            check3 = false;
            Checkbox3.enabled = (false);
        }
        else
        {
            checkall = true;
            CheckAll.enabled = (true);
            if (OpenDropdown.enabled)
            {
                Checkbox1.enabled = (true);
                Checkbox2.enabled = (true);
                Checkbox3.enabled = (true);
            }
            check1 = true;
            check2 = true;
            check3 = true;
        }
    }

    void CheckForAll()
    {
        if (Checkbox1.enabled && Checkbox2.enabled && Checkbox3.enabled)
        {
            checkall = true;
            CheckAll.enabled = (true);
        }
        else
            CheckAll.enabled = (false);
    }

    void ResetDropdown()
    {
        ClosedDropdown.enabled = (true);
        OpenDropdown.enabled = (false);
        CheckAll.enabled = (false);
        Checkbox1.enabled = (false);
        Checkbox2.enabled = (false);
        Checkbox3.enabled = (false);
        CheckButton1.gameObject.SetActive(false);
        CheckButton2.gameObject.SetActive(false);
        CheckButton3.gameObject.SetActive(false);
    }
}
