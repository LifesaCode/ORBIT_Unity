using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControls : MonoBehaviour
{
	public GameObject FilledWarm;
	public GameObject FilledCool;

	SpriteRenderer[] warmLights;
	SpriteRenderer[] coolLights;
	public GameObject PowerPodLights;
	public GameObject EspiritPodLights;
	public GameObject UtilizationPodLights;
	public GameObject InternationalLights;
	public GameObject LogisticsLights;
	public GameObject EuropeanLights;

	MeshRenderer[] PowerPodRenderers;
	MeshRenderer[] EspiritPodRenderers;
	MeshRenderer[] UtilizationPodRenderers;
	MeshRenderer[] InternationalPodRenderers;
	MeshRenderer[] LogisticsPodRenderers;
	MeshRenderer[] EuropeanPodRenderers;
	MeshRenderer[] nullPod = { null, null };
	MeshRenderer[] activePod;

	int[] PowerPodSettings = { 2, 5 };
	int[] EspiritPodSettings = { 1, 3 };
	int[] UtilizationPodSettings = { 5, 0 };
	int[] InternationalPodSettings = { 1, 4 };
	int[] LogisticsPodSettings = { 2, 1 };
	int[] EuropeanPodSettings = { 4, 1 };
	int[] HabitationSettings = { 2, 4 };
	int[] MultiPurpSettings = { 3, 2 };
	int[] activeSettings;

	float opacity = .35f; // alpha of 90/255
	float alphaStep = .166f; // each button will increment light opacity by this value
	Color c;

	int currentWarmLevel;
	int currentCoolLevel;
	
    // Start is called before the first frame update
    void Start()
    {
		warmLights = FilledWarm.GetComponentsInChildren<SpriteRenderer>();
		coolLights = FilledCool.GetComponentsInChildren<SpriteRenderer>();
		PowerPodRenderers = PowerPodLights.GetComponentsInChildren<MeshRenderer>();
		EspiritPodRenderers = EspiritPodLights.GetComponentsInChildren<MeshRenderer>();
		UtilizationPodRenderers = UtilizationPodLights.GetComponentsInChildren<MeshRenderer>();
		InternationalPodRenderers = InternationalLights.GetComponentsInChildren<MeshRenderer>();
		LogisticsPodRenderers = LogisticsLights.GetComponentsInChildren<MeshRenderer>();
		EuropeanPodRenderers = EuropeanLights.GetComponentsInChildren<MeshRenderer>();

		StartupLights();
	}

	void StartupLights()
	{
		SetAlpha(PowerPodSettings[0], PowerPodRenderers[0]);
		SetAlpha(PowerPodSettings[1], PowerPodRenderers[1]);
		SetAlpha(EspiritPodSettings[0], EspiritPodRenderers[0]);
		SetAlpha(EspiritPodSettings[1], EspiritPodRenderers[1]);
		SetAlpha(UtilizationPodSettings[0], UtilizationPodRenderers[0]);
		SetAlpha(UtilizationPodSettings[1], UtilizationPodRenderers[1]);
		SetAlpha(InternationalPodSettings[0], InternationalPodRenderers[0]);
		SetAlpha(InternationalPodSettings[1], InternationalPodRenderers[1]);
		SetAlpha(LogisticsPodSettings[0], LogisticsPodRenderers[0]);
		SetAlpha(LogisticsPodSettings[1], LogisticsPodRenderers[1]);
		SetAlpha(EuropeanPodSettings[0], EuropeanPodRenderers[0]);
		SetAlpha(EuropeanPodSettings[1], EuropeanPodRenderers[1]);
	}

	public void LightsOff(){
		SetNewWarmLevel(0);
		SetAlpha(0, activePod[0]);
		SetNewCoolLevel(0);
		SetAlpha(0, activePod[1]);
	}
	
	public void WarmLevel1(){
		SetNewWarmLevel(1);
		SetAlpha(1, activePod[0]);
	}
	
	public void WarmLevel2(){
		SetNewWarmLevel(2);
		SetAlpha(2, activePod[0]);
	}
	
	public void WarmLevel3(){
		SetNewWarmLevel(3);
		SetAlpha(3, activePod[0]);
	}
	
	public void WarmLevel4(){
		SetNewWarmLevel(4);
		SetAlpha(4, activePod[0]);
	}
	
	public void WarmLevel5(){
		SetNewWarmLevel(5);
		SetAlpha(5, activePod[0]);
	}
	
	public void WarmLevel6(){
		SetNewWarmLevel(6);
		SetAlpha(6, activePod[0]);
	}
	
	void SetNewWarmLevel(int newLevel){
		
		if(newLevel == currentWarmLevel){
			DecreaseWarmLevel(currentWarmLevel, newLevel);
			if (newLevel == 0)
				currentWarmLevel = 0;
			else
				currentWarmLevel = newLevel - 1;
		}
		
		else if (currentWarmLevel > newLevel){
			DecreaseWarmLevel(currentWarmLevel, newLevel);
			currentWarmLevel = newLevel;
		}
		
		else if(currentWarmLevel < newLevel){  // currentLevel < newLevel
			IncreaseWarmLevel(currentWarmLevel, newLevel);
			currentWarmLevel = newLevel;
		}

		if(activeSettings == null)
			return;
		else
			activeSettings[0] = currentWarmLevel;
	}
	
	void IncreaseWarmLevel(int currentLevel, int newLevel){
		for(int i = currentLevel; i < newLevel; i++){
			c = warmLights[i].color;
			c.a = opacity;
			warmLights[i].color = c;
		}
	}
	
	void DecreaseWarmLevel(int currentLevel, int newLevel){
		if(currentWarmLevel == 0)
		{
			return;
		}
		else if(currentWarmLevel == newLevel)
		{
			c = warmLights[0].color;
			c.a = 0;
			warmLights[0].color = c;
		}
		else
		{
			for (int i = (currentLevel - 1); i > (newLevel - 1) ; i--)
			{
				c = warmLights[i].color;
				c.a = 0;
				warmLights[i].color = c;
			}
		}
	}

	public void CoolLevel1(){
		SetNewCoolLevel(1);
		SetAlpha(1, activePod[1]);
	}
	
	public void CoolLevel2(){
		SetNewCoolLevel(2);
		SetAlpha(2, activePod[1]);
	}
	
	public void CoolLevel3(){
		SetNewCoolLevel(3);
		SetAlpha(3, activePod[1]);
	}
	
	public void CoolLevel4(){
		SetNewCoolLevel(4);
		SetAlpha(4, activePod[1]);
	}
	
	public void CoolLevel5(){
		SetNewCoolLevel(5);
		SetAlpha(5, activePod[1]);
	}
	
	public void CoolLevel6(){
		SetNewCoolLevel(6);
		SetAlpha(6, activePod[1]);
	}
	
	void SetNewCoolLevel(int newLevel){
		
		if(newLevel == currentCoolLevel){
			DecreaseCoolLevel(currentCoolLevel, newLevel);
			if (newLevel == 0)
				currentCoolLevel = 0;
			else
				currentCoolLevel = newLevel - 1;
		}
		
		else if (currentCoolLevel > newLevel){
			DecreaseCoolLevel(currentCoolLevel, newLevel);
			currentCoolLevel = newLevel;
		}
		
		else if(currentCoolLevel < newLevel){  // currentLevel < newLevel
			IncreaseCoolLevel(currentCoolLevel, newLevel);
			currentCoolLevel = newLevel;
		}

		if (activeSettings == null)
			return;
		else
			activeSettings[1] = currentCoolLevel;
	}

	void IncreaseCoolLevel(int currentLevel, int newLevel){
		Debug.Log("IncreaseCoolLevel triggered");
		for(int i = currentLevel; i < newLevel; i++){
			c = coolLights[i].color;
			c.a = opacity;
			coolLights[i].color = c;
		}
	}
	
	void DecreaseCoolLevel(int currentLevel, int newLevel){
		if (currentCoolLevel == 0)
		{
			return;
		}
		else if (currentCoolLevel == newLevel)
		{
			c = coolLights[0].color;
			c.a = 0;
			coolLights[0].color = c;
		}
		else
		{
			for (int i = (currentLevel - 1); i > (newLevel - 1); i--)
			{
				c = coolLights[i].color;
				c.a = 0;
				coolLights[i].color = c;
			}
		}
	}
	
	void SetAlpha(int level, MeshRenderer light){
		if (light == null)
		{
			return;	 // no associated light in scene
		}

		c = light.material.color;
		c.a = level * alphaStep;
		light.material.color = c;
	}

	public void ChangeLightSetting(modules newDownstate)
	{
		ResetWarmButtons();
		ResetCoolButtons();

		if(newDownstate == modules.Power)
		{
			activePod = PowerPodRenderers;
			activeSettings = PowerPodSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else if(newDownstate == modules.ESPIRIT)
		{
			activePod = EspiritPodRenderers;
			activeSettings = EspiritPodSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else if(newDownstate == modules.Utilization)
		{
			activePod = UtilizationPodRenderers;
			activeSettings = UtilizationPodSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else if(newDownstate == modules.International)
		{
			activePod = InternationalPodRenderers;
			activeSettings = InternationalPodSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else if(newDownstate == modules.Logistics)
		{
			activePod = LogisticsPodRenderers;
			activeSettings = LogisticsPodSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else if(newDownstate == modules.European)
		{
			activePod = EuropeanPodRenderers;
			activeSettings = EuropeanPodSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else if(newDownstate == modules.Habitation)
		{
			activePod = nullPod;
			activeSettings = HabitationSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else
		{
			activePod = nullPod;
			activeSettings = MultiPurpSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
	}

	void ResetWarmButtons()
	{
		for (int i = (warmLights.Length - 1); i >= 0; i--)
		{
			c = warmLights[i].color;
			c.a = 0;
			warmLights[i].color = c;
		}
		currentWarmLevel = 0;
	}

	void ResetCoolButtons()
	{
		for (int i = (coolLights.Length - 1); i >= 0; i--)
		{
			c = coolLights[i].color;
			c.a = 0;
			coolLights[i].color = c;
		}
		currentCoolLevel = 0;
	}

}
