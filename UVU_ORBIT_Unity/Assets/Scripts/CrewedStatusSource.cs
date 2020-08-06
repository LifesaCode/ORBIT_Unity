using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrewedStatusSource : MonoBehaviour
{
	public Image crewedBackground;
	public Image uncrewedBackground;
	public TextMeshProUGUI crewedText;
	public TextMeshProUGUI uncrewedText;

	bool crewed = true;
	Color white = new Color(255, 255, 255, 1);
	Color black = new Color(0, 0, 0, 1);

	public delegate void ChangeCrewed();
	public static event ChangeCrewed ChangeCrewedStatus;

public void OnCrewedChange()
	{
		if(ChangeCrewedStatus != null)
		{
			ChangeCrewedStatus();
		}

		if(crewed){
			crewed = false;

			crewedBackground.enabled = false;
			crewedText.color = white;
			uncrewedBackground.enabled = true;
			uncrewedText.color = black;
			
		}else{
			crewed = true;

			crewedBackground.enabled = true;
			crewedText.color = black;	
			uncrewedBackground.enabled = false;
			uncrewedText.color = white;
			
		}
	}
	
}