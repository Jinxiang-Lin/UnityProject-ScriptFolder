using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    protected float pressedScale = 0.87f;
    protected float transitionSpeed = 13f;

    protected Vector3 originalScale;
    protected Vector3 targetScale;
    protected bool isPressed = false;
    protected bool isScaling = false;

    public virtual void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }
    public virtual void Update()
    {
        if (isScaling)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * transitionSpeed);

            // Stop scaling if close enough to target
            if (Vector3.Distance(transform.localScale, targetScale) < 0.001f)
            {
                transform.localScale = targetScale;
                isScaling = false;
            }
        }
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        
    }

}
