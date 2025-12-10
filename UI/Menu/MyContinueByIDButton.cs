using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class MyContinueByIDButton : MyButton
{
    [SerializeField] int id;
    [SerializeField] TextMeshProUGUI IDText;

    public int Id { get => id; set => id = value; }

    public override void Start()
    {
        base.Start();
        id = GetComponentInParent<SavedSlotShareData>().Id;
        DisplayGameData();
    }
    
    public override void Update()
    {
        base.Update();
        // Additional update logic if needed
    }
    public void DisplayGameData()
    {

        GameData gameData = SaveSystem.LoadGameData(id);
        if (gameData != null)
        {
            IDText.text = "ID: " + gameData.id;
        }
        else
        {
            IDText.text = "No saved data";
        }
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
        CurtainTransition.Instance.TransitionToContinueByID(id);
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
