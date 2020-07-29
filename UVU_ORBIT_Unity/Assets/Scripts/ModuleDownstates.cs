using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum modules
{
	Habitation,
	Power,
	ESPIRIT,
	Utilization,
	International,
	Logistics,
	European,
	MultiPurpose
}

public class ModuleDownstates : MonoBehaviour
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
	SpriteRenderer[] titles;

	public LightControls lights;

	public modules activeModule;
	
    // Start is called before the first frame update
    void Start()
    {
		titles = ModuleTitles.GetComponentsInChildren<SpriteRenderer>();
        GetHabitation();
		DisableTitles();
		titles[(int)modules.Habitation].enabled = true;
		activeModule = modules.Habitation;
		lights = gameObject.GetComponent<LightControls>();
		lights.ChangeLightSetting(activeModule);
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
		DisableAll();
		DisableTitles();
		PowerDownstate.SetActive(true);
		titles[(int)modules.Power].enabled = true;
		activeModule = modules.Power;
		lights.ChangeLightSetting(activeModule);
	}

	public void GetEsprit(){
		DisableAll();
		DisableTitles();
		EspritDownstate.SetActive(true);
		titles[(int)modules.ESPIRIT].enabled = true;
		activeModule = modules.ESPIRIT;
		lights.ChangeLightSetting(activeModule);
	}

	public void GetUtilize(){
		DisableTitles();
		DisableAll();
		UtilizeDownstate.SetActive(true);
		titles[(int)modules.Utilization].enabled = true;
		activeModule = modules.Utilization;
		lights.ChangeLightSetting(activeModule);
	}

	public void GetInternat(){
		DisableTitles();
		DisableAll();
		InternatDownstate.SetActive(true);
		titles[(int)modules.International].enabled = true;
		activeModule = modules.International;
		lights.ChangeLightSetting(activeModule);
	}
	
	public void GetLogistics(){
		DisableTitles();
		DisableAll();
		LogisticsDownstate.SetActive(true);
		titles[(int)modules.Logistics].enabled = true;
		activeModule = modules.Logistics;
		lights.ChangeLightSetting(activeModule);
	}

	public void GetHabitation(){
		DisableTitles();
		DisableAll();
		HabitatDownstate.SetActive(true);
		titles[(int)modules.Habitation].enabled = true;
		activeModule = modules.Habitation;
		lights.ChangeLightSetting(activeModule);
	}

	public void GetMultiPur(){
		DisableTitles();
		DisableAll();
		MultiPurDownstate.SetActive(true);
		titles[(int)modules.MultiPurpose].enabled = true;
		activeModule = modules.MultiPurpose;
		lights.ChangeLightSetting(activeModule);
	}

	public void GetEurope(){
		DisableTitles();
		DisableAll();
		EuropeDownstate.SetActive(true);
		titles[(int)modules.European].enabled = true;
		activeModule = modules.European;
		lights.ChangeLightSetting(activeModule);
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
