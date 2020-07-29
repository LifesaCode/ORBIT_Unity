using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color HoverOnColor = new Color(162, 237, 242, .39f);
    public Color HoverOffColor = new Color(255, 255, 255, 0);
    public Image Target;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Target.color = HoverOnColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Target.color = HoverOffColor;
    }
}
