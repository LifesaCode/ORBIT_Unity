using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// enum modules located in ModuleDownstates

public class C_ModuleDownstates : MonoBehaviour
{
	public GameObject PowerDownstate;
	public GameObject EspritDownstate;
	public GameObject UtilizeDownstate;
	public GameObject InternatDownstate;
	public GameObject LogisticsDownstate;
	public GameObject HabitatDownstate;
	public GameObject MultiPurDownstate;
	public GameObject EuropeDownstate;
	public GameObject ModuleTitles;
	Image[] titles;

	public C_LightControls lights;
	
    // Start is called before the first frame update
    void Start()
    {
		//lights = gameObject.GetComponent<C_LightControls>();
		titles = ModuleTitles.GetComponentsInChildren<Image>();
		DisableTitles();
		DisableAll();
		HabitatDownstate.SetActive(true);
		titles[(int)modules.Habitation].enabled = true;
		//lights.ChangeLightSetting(activeModule);
    }

	void DisableTitles()
	{
		titles[(int)modules.Habitation].enabled = false;
		titles[(int)modules.Power].enabled = false;
		titles[(int)modules.ESPIRIT].enabled = false;
		titles[(int)modules.Utilization].enabled = false;
		titles[(int)modules.International].enabled = false;
		titles[(int)modules.Logistics].enabled = false;
		titles[(int)modules.European].enabled = false;
		titles[(int)modules.MultiPurpose].enabled = false;
	}

	public void GetPower(){
		Debug.Log("Power Downstate called");
		DisableAll();
		DisableTitles();
		PowerDownstate.SetActive(true);
		titles[(int)modules.Power].enabled = true;
		lights.ChangeLightSetting(modules.Power);
	}

	public void GetEsprit(){
		DisableAll();
		DisableTitles();
		EspritDownstate.SetActive(true);
		titles[(int)modules.ESPIRIT].enabled = true;
		lights.ChangeLightSetting(modules.ESPIRIT);
	}

	public void GetUtilize(){
		DisableTitles();
		DisableAll();
		UtilizeDownstate.SetActive(true);
		titles[(int)modules.Utilization].enabled = true;
		lights.ChangeLightSetting(modules.Utilization);
	}

	public void GetInternat(){
		DisableTitles();
		DisableAll();
		InternatDownstate.SetActive(true);
		titles[(int)modules.International].enabled = true;
		lights.ChangeLightSetting(modules.International);
	}
	
	public void GetLogistics(){
		DisableTitles();
		DisableAll();
		LogisticsDownstate.SetActive(true);
		titles[(int)modules.Logistics].enabled = true;
		lights.ChangeLightSetting(modules.Logistics);
	}

	public void GetHabitation(){
		DisableTitles();
		DisableAll();
		HabitatDownstate.SetActive(true);
		titles[(int)modules.Habitation].enabled = true;
		lights.ChangeLightSetting(modules.Habitation);
	}

	public void GetMultiPur(){
		DisableTitles();
		DisableAll();
		MultiPurDownstate.SetActive(true);
		titles[(int)modules.MultiPurpose].enabled = true;
		lights.ChangeLightSetting(modules.MultiPurpose);
	}

	public void GetEurope(){
		DisableTitles();
		DisableAll();
		EuropeDownstate.SetActive(true);
		titles[(int)modules.European].enabled = true;
		lights.ChangeLightSetting(modules.European);
	}
	
    void DisableAll(){
		PowerDownstate.SetActive(false);
		EspritDownstate.SetActive(false);
		UtilizeDownstate.SetActive(false);
		InternatDownstate.SetActive(false);
		LogisticsDownstate.SetActive(false);
		HabitatDownstate.SetActive(false);
		MultiPurDownstate.SetActive(false);
		EuropeDownstate.SetActive(false);
	}
}

