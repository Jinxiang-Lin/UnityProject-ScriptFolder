using UnityEngine;

public class SlotsManager : MonoBehaviour
{
    private void Awake()
    {
        int id = 0;
        foreach (Transform child in transform)
        {
            var slotData = child.GetComponent<SavedSlotShareData>();
            if (slotData != null)
            {
                slotData.Id = id;
                id++;
            }
            else
            {
                Debug.LogWarning($"Child '{child.name}' is missing SavedSlotShareData.");
            }
        }
    }
}
