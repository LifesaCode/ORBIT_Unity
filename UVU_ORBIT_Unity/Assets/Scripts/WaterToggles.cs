using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterToggles : MonoBehaviour
{
    public WaterGauges gaugeScript;
    public GameObject processorSwitch;
    public GameObject distillerSwitch;

    public void ProcessorOnOff()
    {
        gaugeScript.ToggleProcessorOnOff();
    }

    public void DistillerOnOff()
    {
        gaugeScript.ToggleDistillerOnOff();
    }
    
}
