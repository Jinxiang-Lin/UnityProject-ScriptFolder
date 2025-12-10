using UnityEngine;

public class SavedSlotShareData : MonoBehaviour
{
    [SerializeField] int id;
    public MyContinueByIDButton MyContinueByIDButtonReference;
    public MyDeleteByIdButton MyDeleteByIdButtonReference;
    public int Id { get => id; set => id = value; }
    private void Awake()
    {
        foreach (var button in GetComponentsInChildren<MyButton>())
        {
            if (button is MyContinueByIDButton continueButton)
            {
                MyContinueByIDButtonReference = continueButton;
            }
            else if (button is MyDeleteByIdButton deleteButton)
            {
                MyDeleteByIdButtonReference = deleteButton;
            }
        }
    }
}
