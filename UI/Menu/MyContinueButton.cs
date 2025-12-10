using UnityEngine;
using UnityEngine.EventSystems;

public class MyContinueButton : MyButton
{
    public override void Start()
    {
        base.Start();
        // Additional initialization if needed
    }
    public override void Update()
    {
        base.Update();
        // Additional update logic if needed
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        targetScale = originalScale * pressedScale;
        isScaling = true;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        targetScale = originalScale;
        isScaling = true;
        CurtainTransition.Instance.TransitionToContinue();
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (isPressed)
        {
            isPressed = false;
            targetScale = originalScale;
            isScaling = true;
        }
    }
}
