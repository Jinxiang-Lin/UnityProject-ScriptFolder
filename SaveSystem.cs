using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string GetSavePath(int id)
    {
        if (id < 0 || id >= 5) // Assuming 5 slots
        {
            Debug.LogError("Invalid save slot ID: " + id);
            return null;
        }
        
        return Path.Combine(Application.persistentDataPath, $"save_slot_{id}.json");
    }
    public static bool IsSlotUsed(int id)
    {
        return File.Exists(GetSavePath(id));
    }
    public static int GetFirstAvailableSlot()
    {
        for (int i = 0; i < 5; i++) 
        {
            if (!IsSlotUsed(i))
            {
                return i;
            }
        }
        return -1; // No available slots
    }
    public static void SaveGameData(GameData data, int id)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSavePath(id), json);
    }

    public static GameData LoadGameData(int id)
    {
        Debug.Log("Loading game data for slot: " + id);
        string path = GetSavePath(id);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.LogWarning("Save file not found: " + path);
            return null;
        }
    }

    public static bool SaveExists(int id)
    {
        return File.Exists(GetSavePath(id));
    }

    public static void DeleteSave(int id)
    {
        string path = GetSavePath(id);
        if (File.Exists(path))
            File.Delete(path);
    }
    public static void ShowAllSaveSlots()
    {
        for (int i = 0; i < 5; i++) // Assuming 10 slots
        {
            if (IsSlotUsed(i))
            { 
                GameData data = LoadGameData(i);
                if (data != null)
                {
                    Debug.Log($"Slot {i} is used ");
                }
                else
                {
                    Debug.Log($"Slot {i} is empty or data is corrupted.");
                }
            }
        }
    }
}

