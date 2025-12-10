
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurtainTransition : MonoBehaviour
{
    public static CurtainTransition Instance { get; private set; }

    public RectTransform leftCurtain;
    public RectTransform rightCurtain;
    public float duration = 1f;

    private bool isClosing = false;

    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Start open on initial scene
        StartCoroutine(OpenCurtain());
    }
    public void TransitionToScene(int i)
    {
        if (!isClosing)
        {
            StartCoroutine(CloseCurtainAndLoad(i));
            Debug.Log("Transitioning from scene to scene: " + i);
        }
    }
    
    #region new game transition
    public void TransitionToNewGame()
    {
        if (!isClosing)
        {
            StartCoroutine(CloseCurtainAndLoadNewGame());
            Debug.Log("Transitioning to new game");
        }
    }
    IEnumerator CloseCurtainAndLoadNewGame()
    {
        isClosing = true;
        yield return StartCoroutine(MoveCurtains(Vector2.zero, Vector2.zero));

        // Wait 1 frame to ensure full closure before loading  
        yield return null;
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameData gameData = new GameData
        {
            id = SaveSystem.GetFirstAvailableSlot(),
            current_water_level = 40f,
            currentSceneIndex = 2,
            posX = 0f,
            posY = 0f,
            sceneName = "S2", // Set the scene name for the new game
            lastSaveTime = System.DateTime.Now // Optional: Set the last save time
        };
        PlayerPrefs.SetInt("CurrentID", gameData.id); // Save the current id in PlayerPrefs
        if (gameData.id == -1)
        {
            Debug.LogError("No available save slots found!");
            for (int i = 0; i < 5; i++)
            {
                SaveSystem.DeleteSave(i); // Delete all save slots if no available slot found
            }
            yield break; // Exit if no slots are available
        }
        SaveSystem.SaveGameData(gameData, gameData.id); // Save the new game data
        SceneManager.LoadScene(gameData.sceneName); // New Game
    }
    #endregion
    #region continue transition
    public void TransitionToContinue()
    {
        if (!isClosing)
        {
            StartCoroutine(CloseCurtainAndLoadContinue());
            Debug.Log("Transitioning to continue slots");
        }
    }
    IEnumerator CloseCurtainAndLoadContinue()
    {
        isClosing = true;
        yield return StartCoroutine(MoveCurtains(Vector2.zero, Vector2.zero));
        yield return null;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("ContinueSlots");
    }
    #endregion
    #region continue by ID transition
    public void TransitionToContinueByID(int id)
    {
        if (SaveSystem.SaveExists(id))
        {
            if (!isClosing)
            {
                StartCoroutine(CloseCurtainAndLoadFromContinueByID(id));
                Debug.Log("Transitioning from continue slots to scene: " + id);
            }
        }
        else
                    {
            Debug.LogWarning($"No save data found for ID: {id}");
        }

    }
    IEnumerator CloseCurtainAndLoadFromContinueByID(int id)
    {
        isClosing = true;
        PlayerPrefs.SetInt("CurrentID", id);
        yield return StartCoroutine(MoveCurtains(Vector2.zero, Vector2.zero));

        // Wait 1 frame to ensure full closure before loading  
        yield return null;

        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log($"Transitioning from continue slots to scene: id is {id}");
        GameData gd = SaveSystem.LoadGameData(id); // Load the game data for the selected slot
        //SaveSystem.SaveGameData(gd, id); // Save the new game data
        SceneManager.LoadScene(gd.sceneName);
    }
    #endregion
    IEnumerator CloseCurtainAndLoad(int i)
    {
        isClosing = true;
        yield return StartCoroutine(MoveCurtains(Vector2.zero, Vector2.zero));

        // Wait 1 frame to ensure full closure before loading  
        yield return null;

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(i);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Re-link curtain RectTransforms in case the Canvas was destroyed/reloaded
        // (if they're child objects, they persist; otherwise you'll need to reassign)

        StartCoroutine(OpenCurtain());
        isClosing = false;
    }

    IEnumerator OpenCurtain()
    {
        float screenWidth = Screen.width;
        Vector2 leftTarget = new Vector2(-screenWidth / 2, 0);
        Vector2 rightTarget = new Vector2(screenWidth / 2, 0);
        
        yield return StartCoroutine(MoveCurtains(leftTarget, rightTarget));
    }

    IEnumerator MoveCurtains(Vector2 leftTarget, Vector2 rightTarget)
    {
        Vector2 leftStart = leftCurtain.anchoredPosition;
        Vector2 rightStart = rightCurtain.anchoredPosition;

        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            leftCurtain.anchoredPosition = Vector2.Lerp(leftStart, leftTarget, t);
            rightCurtain.anchoredPosition = Vector2.Lerp(rightStart, rightTarget, t);
            yield return null;
        }

        leftCurtain.anchoredPosition = leftTarget;
        rightCurtain.anchoredPosition = rightTarget;
    }
}

