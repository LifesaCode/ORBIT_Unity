using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SpriteHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color HoverOnColor = new Color(168, 221, 231, .3f);
    public Color HoverOffColor = new Color(168, 221, 231, 0);
    Image Target;

    private void Start()
    {
        Target = gameObject.GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Target.color = HoverOnColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Target.color = HoverOffColor;
    }
}
