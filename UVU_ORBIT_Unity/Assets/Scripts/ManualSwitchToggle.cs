using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManualSwitchToggle : MonoBehaviour
{
	public Image manualBackground;
	public Image autoBackground;
	public TextMeshProUGUI manualText;
	public TextMeshProUGUI autoText;

	bool manual = false;
	Color white = new Color(255, 255, 255, 1);
	Color black = new Color(0, 0, 0, 1);

	public void ToggleManualSwitch()
	{
		if (manual)
		{
			manual = false;

			manualBackground.enabled = false;
			manualText.color = white;
			autoBackground.enabled = true;
			autoText.color = black;
		}
		else
		{
			manual = true;

			manualBackground.enabled = true;
			manualText.color = black;
			autoBackground.enabled = false;
			autoText.color = white;
		}
	}

}
