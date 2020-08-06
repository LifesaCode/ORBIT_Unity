using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Orbit.Models;
using Orbit.Annotations;
using System;
using System.Reflection;


public class C_CabinGauges : MonoBehaviour
{
	public GameObject DecibelColored;
	public GameObject OxygenSource;
	public Image OxygenSourceDial;
	public GameObject OxygenGauge;
	public Image OxygenDial;
	public GameObject Co2Source;
	public Image Co2SourceDial;
	public GameObject Co2Gauge;
	public Image Co2Dial;
	public GameObject TempGauge;
	public Image TempDial;
	public GameObject HumidityGauge;
	public Image HumidityDial;

	TextMeshProUGUI[] o2SourceLabels;
	TextMeshProUGUI[] o2Labels;
	TextMeshProUGUI[] co2SourceLabels;
	TextMeshProUGUI[] co2labels;
	TextMeshProUGUI[] tempLabels;
	TextMeshProUGUI[] humidityLabels;
	Image[] decibelsFilled;

	float maxTemp = 30.0f;
	float maxHumidity = 80f;

	AtmosphereData cabin = new AtmosphereData();

	// Start is called before the first frame update
	void Start()
	{
		cabin.SeedData();

		decibelsFilled = DecibelColored.GetComponentsInChildren<Image>();

		o2SourceLabels = OxygenSource.GetComponentsInChildren<TextMeshProUGUI>();
		o2Labels = OxygenGauge.GetComponentsInChildren<TextMeshProUGUI>();

		co2SourceLabels = Co2Source.GetComponentsInChildren<TextMeshProUGUI>();
		co2labels = Co2Gauge.GetComponentsInChildren<TextMeshProUGUI>();

		tempLabels = TempGauge.GetComponentsInChildren<TextMeshProUGUI>();
		humidityLabels = HumidityGauge.GetComponentsInChildren<TextMeshProUGUI>();

		PropertyInfo propInfo = typeof(AtmosphereData).GetProperty("Temperature");
		SetGaugeMinMax(propInfo, humidityLabels);
		UpdateGauge(TempDial, tempLabels, (float)cabin.Temperature, maxTemp);

		propInfo = typeof(AtmosphereData).GetProperty("HumidityLevel");
		SetGaugeMinMax(propInfo, tempLabels);
		UpdateGauge(HumidityDial, humidityLabels, (float)cabin.HumidityLevel, maxHumidity);

		o2Labels[1].text = o2SourceLabels[1].text;
		o2Labels[2].text = o2SourceLabels[2].text;
		o2Labels[3].text = o2SourceLabels[3].text;
		OxygenDial.fillAmount = OxygenSourceDial.fillAmount;

		Co2Dial.fillAmount = Co2SourceDial.fillAmount;

		StartCoroutine(UpdateCabin());
	}

	IEnumerator UpdateCabin()
	{

		while (true)
		{
			cabin.ProcessData();

			UpdateDecibels();

			UpdateGauge(TempDial, tempLabels, (float)cabin.Temperature, maxTemp);
			UpdateGauge(HumidityDial, humidityLabels, (float)cabin.HumidityLevel, maxHumidity);

			// for some reason, co2 labels will only update when placed here
			co2labels[1].text = co2SourceLabels[1].text;
			co2labels[2].text = co2SourceLabels[2].text;
			co2labels[3].text = co2SourceLabels[3].text;
			Co2Dial.fillAmount = Co2SourceDial.fillAmount;

			o2Labels[1].text = o2SourceLabels[1].text;
			OxygenDial.fillAmount = OxygenSourceDial.fillAmount;

			GetAlerts();

			yield return new WaitForSeconds(0.5f);
		}
	}

	void SetGaugeMinMax(PropertyInfo propInfo, TextMeshProUGUI[] labels)
	{
		IdealRangeAttribute idealRange = (IdealRangeAttribute)Attribute.GetCustomAttribute(propInfo, typeof(IdealRangeAttribute));
		labels[2].text = idealRange.IdealMaximum.ToString();
		labels[3].text = idealRange.IdealMinimum.ToString();
	}

	void UpdateGauge(Image dial, TextMeshProUGUI[] labels, float value, float max)
	{
		labels[1].text = value.ToString();
		dial.fillAmount = (value / max * 1.0f) * .75f;
	}

	void UpdateDecibels()
	{
		int noise = (int)cabin.AmbientNoiseLevel / 10;

		for (int i = 0; i < decibelsFilled.Length; i++)
		{
			decibelsFilled[i].enabled = false;
		}

		int level = 0;
		while (level <= noise)
		{
			decibelsFilled[level].enabled = true;
			level++;
		}
	}

	void ChangeCrewedStatus(){
		if(cabin.CabinStatus == Modes.Uncrewed){
			cabin.CabinStatus = Modes.Crewed;
		}else{
			cabin.CabinStatus = Modes.Uncrewed;
		}
	}

	public void ToggleManualMode()
	{
		if (cabin.IsManualMode)
		{
			cabin.IsManualMode = false;
		}
		else
		{
			cabin.IsManualMode = true;
		}
		Debug.Log("Manual Mode Triggered");
	}

	private void GetAlerts()
	{
		IEnumerable alerts = cabin.GetAlerts();
		foreach(Alert a in alerts)
		{
			//Debug.Log(a.PropertyName.ToString() + ", " + a.AlertLevel.ToString());
		}
	}
}
