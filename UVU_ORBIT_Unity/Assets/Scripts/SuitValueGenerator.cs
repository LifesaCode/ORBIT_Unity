using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SuitValueGenerator : MonoBehaviour
{
    public TextMeshProUGUI Oxygen;
    public TextMeshProUGUI CO2;
    public TextMeshProUGUI Pressure;
    public TextMeshProUGUI Temperature;

    //void OnEnable()
    //{
    //    StartCoroutine(UpdateSuitValues());
    //}

    void OnEnable()
    {
        Oxygen.text = System.Math.Round(Random.Range(27.0f, 32.4f), 1).ToString();
        CO2.text = System.Math.Round(Random.Range(1.0f, 4.9f), 1).ToString();
        Pressure.text = System.Math.Round(Random.Range(4.0f, 5.9f), 1).ToString();
        Temperature.text = System.Math.Round(Random.Range(20.0f, 30.0f), 1).ToString();

        StartCoroutine(UpdateSuitValues());
    }

    IEnumerator UpdateSuitValues()
    {
        while (true)
        {
            Oxygen.text = System.Math.Round(Random.Range(97.0f, 99.9f), 1).ToString();
            CO2.text = System.Math.Round(Random.Range(0.01f, 1.01f), 2).ToString();
            Pressure.text = System.Math.Round(Random.Range(8.0f, 8.7f), 1).ToString();
            Temperature.text = System.Math.Round(Random.Range(20.0f, 25.1f), 1).ToString();

            yield return new WaitForSeconds(.75f);
        }

    }
}
