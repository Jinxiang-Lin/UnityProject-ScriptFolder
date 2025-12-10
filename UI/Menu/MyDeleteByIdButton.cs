using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyDeleteByIdButton : MyButton
{
    [SerializeField] int id;
    SavedSlotShareData savedSlotShareData;
    public int Id { get => id; set => id = value; }

    public override void Start()
    {
        base.Start();
        savedSlotShareData = GetComponentInParent<SavedSlotShareData>();
        id  = savedSlotShareData.Id;

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
        SaveSystem.DeleteSave(id);
        savedSlotShareData.MyContinueByIDButtonReference.DisplayGameData();
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
