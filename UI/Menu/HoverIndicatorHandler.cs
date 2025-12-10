using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverIndicatorHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    public float fadeSpeed = 5f;
    public Color hoverColor = Color.white;

    private Color originalColor;
    private bool isHovering = false;

    void Start()
    {
        if (buttonText == null)
            buttonText = GetComponentInChildren<TextMeshProUGUI>();

        originalColor = buttonText.color;
    }

    void Update()
    {
        if (isHovering)
            buttonText.color = Color.Lerp(buttonText.color, hoverColor, Time.deltaTime * fadeSpeed);
        else
            buttonText.color = Color.Lerp(buttonText.color, originalColor, Time.deltaTime * fadeSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
