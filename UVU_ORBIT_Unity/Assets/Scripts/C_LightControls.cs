using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_LightControls : MonoBehaviour
{
	public GameObject FilledWarm;
	public GameObject FilledCool;

	Image[] warmLights;
	Image[] coolLights;
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

	int[] PowerPodSettings = {0, 0};
	int[] EspiritPodSettings = { 0, 0 };
	int[] UtilizationPodSettings = { 0, 0 };
	int[] InternationalPodSettings = { 0, 0 };
	int[] LogisticsPodSettings = { 0, 0 };
	int[] EuropeanPodSettings = { 0, 0 };
	int[] HabitationSettings = { 0, 0 };
	int[] MultiPurpSettings = { 0, 0 };
	int[] activeSettings = { 0, 0 };

	float opacity = .35f; // alpha of 90/255
	float alphaStep = .166f; // each button will increment light opacity by this value
	Color c;

	int currentWarmLevel;
	int currentCoolLevel;
	bool crewed = true;

	// Start is called before the first frame update
	void Start()
	{
		CrewedStatusSource.ChangeCrewedStatus += ChangeCrewedStatus;

		warmLights = FilledWarm.GetComponentsInChildren<Image>();
		coolLights = FilledCool.GetComponentsInChildren<Image>();
		PowerPodRenderers = PowerPodLights.GetComponentsInChildren<MeshRenderer>();
		EspiritPodRenderers = EspiritPodLights.GetComponentsInChildren<MeshRenderer>();
		UtilizationPodRenderers = UtilizationPodLights.GetComponentsInChildren<MeshRenderer>();
		InternationalPodRenderers = InternationalLights.GetComponentsInChildren<MeshRenderer>();
		LogisticsPodRenderers = LogisticsLights.GetComponentsInChildren<MeshRenderer>();
		EuropeanPodRenderers = EuropeanLights.GetComponentsInChildren<MeshRenderer>();

		StartupLights();
		ChangeLightSetting(modules.Habitation);
	}

	void StartupLights()
	{
		// warm settings
		PowerPodSettings[0] = 2;
		EspiritPodSettings[0] = 1;
		UtilizationPodSettings[0] = 5;
		InternationalPodSettings[0] = 1;
		LogisticsPodSettings[0] = 2;
		EuropeanPodSettings[0] = 4;
		HabitationSettings[0] = 2;
		MultiPurpSettings[0] = 3;

		// cool settings
		PowerPodSettings[1] = 5;
		EspiritPodSettings[1] = 3;
		UtilizationPodSettings[1] = 0;
		InternationalPodSettings[1] = 4;
		LogisticsPodSettings[1] = 1;
		EuropeanPodSettings[1] = 1;
		HabitationSettings[1] = 4;
		MultiPurpSettings[1] = 2;

		ChangeLightSetting(modules.Power);
		ChangeLightSetting(modules.ESPIRIT);
		ChangeLightSetting(modules.Utilization);
		ChangeLightSetting(modules.International);
		ChangeLightSetting(modules.Logistics);
		ChangeLightSetting(modules.European);
		ChangeLightSetting(modules.Habitation);
		ChangeLightSetting(modules.MultiPurpose);

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

	public void LightsOff()
	{
		SetNewWarmLevel(0);
		SetAlpha(0, activePod[0]);
		SetNewCoolLevel(0);
		SetAlpha(0, activePod[1]);
	}

	void AllLightsOff()
	{
		ChangeLightSetting(modules.Power);
		LightsOff();

		ChangeLightSetting(modules.ESPIRIT);
		LightsOff();

		ChangeLightSetting(modules.Utilization);
		LightsOff();

		ChangeLightSetting(modules.International);
		LightsOff();

		ChangeLightSetting(modules.Logistics);
		LightsOff();

		ChangeLightSetting(modules.European);
		LightsOff();

		ChangeLightSetting(modules.Habitation);
		LightsOff();

		ChangeLightSetting(modules.MultiPurpose);
		LightsOff();
}

	void ChangeCrewedStatus()
	{
		if (crewed)
		{
			AllLightsOff();
			crewed = false;
		}
		else
		{
			StartupLights();
			crewed = true;
		}
	}

public void WarmLevel1()
	{
		SetNewWarmLevel(1);
		SetAlpha(1, activePod[0]);
	}

	public void WarmLevel2()
	{
		SetNewWarmLevel(2);
		SetAlpha(2, activePod[0]);
	}

	public void WarmLevel3()
	{
		SetNewWarmLevel(3);
		SetAlpha(3, activePod[0]);
	}

	public void WarmLevel4()
	{
		SetNewWarmLevel(4);
		SetAlpha(4, activePod[0]);
	}

	public void WarmLevel5()
	{
		SetNewWarmLevel(5);
		SetAlpha(5, activePod[0]);
	}

	public void WarmLevel6()
	{
		SetNewWarmLevel(6);
		SetAlpha(6, activePod[0]);
	}

	void SetNewWarmLevel(int newLevel)
	{
		// ignore lights off request when lights are already off
		if((currentWarmLevel == 0) && (newLevel == 0))
		{
			return;
		}

		// decrease lighting level
		if (currentWarmLevel >= newLevel)
		{
			DecreaseWarmLevel(currentWarmLevel, newLevel);			
			
			if(newLevel == currentWarmLevel)
				currentWarmLevel = newLevel - 1;
			else
				currentWarmLevel = newLevel;
		}
		// increase lighting level
		else if (currentWarmLevel < newLevel)
		{  
			// currentLevel < newLevel
			IncreaseWarmLevel(currentWarmLevel, newLevel);
			currentWarmLevel = newLevel;
		}

		if (activeSettings == null)
			return;
		else
			activeSettings[0] = currentWarmLevel;
	}

	void IncreaseWarmLevel(int currentLevel, int newLevel)
	{
		if(currentLevel == 6)
			return;
		for (int i = currentLevel; i < newLevel; i++)
		{
			warmLights[i].enabled = true;
		}
	}

	void DecreaseWarmLevel(int currentLevel, int newLevel)
	{
		if(currentLevel == newLevel){
			warmLights[currentLevel - 1].enabled = false;
			return;
		}

		int startIndex;
		int endIndex;
		
		// set starting index
		if(currentLevel > 0)
			startIndex = currentLevel - 1;
		else
			startIndex = 0;
		
		// set ending index
		if(newLevel > 0)
			endIndex = newLevel - 1;
		else
			endIndex = -1;

		for(int i = startIndex; i > endIndex; i--)
		{
			warmLights[i].enabled = false;
		}
	}

	public void CoolLevel1()
	{
		SetNewCoolLevel(1);
		SetAlpha(1, activePod[1]);
	}

	public void CoolLevel2()
	{
		SetNewCoolLevel(2);
		SetAlpha(2, activePod[1]);
	}

	public void CoolLevel3()
	{
		SetNewCoolLevel(3);
		SetAlpha(3, activePod[1]);
	}

	public void CoolLevel4()
	{
		SetNewCoolLevel(4);
		SetAlpha(4, activePod[1]);
	}

	public void CoolLevel5()
	{
		SetNewCoolLevel(5);
		SetAlpha(5, activePod[1]);
	}

	public void CoolLevel6()
	{
		SetNewCoolLevel(6);
		SetAlpha(6, activePod[1]);
	}

	void SetNewCoolLevel(int newLevel)
	{
		// ignore lights off request when lights are already off
		if((currentCoolLevel == 0) && (newLevel == 0))
		{
			return;
		}

		// decrease lighting level
		if (currentCoolLevel >= newLevel)
		{
			DecreaseCoolLevel(currentCoolLevel, newLevel);			
			
			if(newLevel == currentCoolLevel)
				currentCoolLevel = newLevel - 1;
			else
				currentCoolLevel = newLevel;
		}
		// increase lighting level
		else if (currentCoolLevel < newLevel)
		{  
			// currentLevel < newLevel
			IncreaseCoolLevel(currentCoolLevel, newLevel);
			currentCoolLevel = newLevel;
		}

		if (activeSettings == null)
			return;
		else
			activeSettings[1] = currentCoolLevel;
	}

	void IncreaseCoolLevel(int currentLevel, int newLevel)
	{
		if(currentLevel == 6)
			return;
		for (int i = currentLevel; i < newLevel; i++)
		{
			coolLights[i].enabled = true;
		}
	}

	void DecreaseCoolLevel(int currentLevel, int newLevel)
	{
		
		if(currentLevel == newLevel){
			coolLights[currentLevel - 1].enabled = false;
			return;
		}

		int startIndex;
		int endIndex;
		
		// set starting index
		if(currentLevel > 0)
			startIndex = currentLevel - 1;
		else
			startIndex = 0;
		
		// set ending index
		if(newLevel > 0)
			endIndex = newLevel - 1;
		else
			endIndex = -1;

		for(int i = startIndex; i > endIndex; i--)
		{
			coolLights[i].enabled = false;
		}
	}

	void SetAlpha(int level, MeshRenderer light)
	{
		if (light == null)
		{
			return;  // no associated light in scene
		}

		c = light.material.color;
		c.a = level * alphaStep;
		light.material.color = c;
	}

	public void ChangeLightSetting(modules newDownstate)
	{
		ResetWarmButtons();
		ResetCoolButtons();

		if (newDownstate == modules.Power)
		{
			activePod = PowerPodRenderers;
			activeSettings = PowerPodSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else if (newDownstate == modules.ESPIRIT)
		{
			activePod = EspiritPodRenderers;
			activeSettings = EspiritPodSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else if (newDownstate == modules.Utilization)
		{
			activePod = UtilizationPodRenderers;
			activeSettings = UtilizationPodSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else if (newDownstate == modules.International)
		{
			activePod = InternationalPodRenderers;
			activeSettings = InternationalPodSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else if (newDownstate == modules.Logistics)
		{
			activePod = LogisticsPodRenderers;
			activeSettings = LogisticsPodSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else if (newDownstate == modules.European)
		{
			activePod = EuropeanPodRenderers;
			activeSettings = EuropeanPodSettings;
			SetNewWarmLevel(activeSettings[0]);
			SetNewCoolLevel(activeSettings[1]);
		}
		else if (newDownstate == modules.Habitation)
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
			warmLights[i].enabled = false;
		}
		currentWarmLevel = 0;
	}

	void ResetCoolButtons()
	{
		for (int i = (coolLights.Length - 1); i >= 0; i--)
		{
			coolLights[i].enabled = false;
		}
		currentCoolLevel = 0;
	}

}
