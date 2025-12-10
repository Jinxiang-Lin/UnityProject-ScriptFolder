using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    //end of test
    public GameObject playerPrefab;
    //public GameObject curtainManagerPrefab;
    private GameObject playerInstance;

    readonly Vector2 defaultSpawnPoint = Vector2.zero;
    int currentId = -1;
    public static GameManager Instance { get; private set; }
    public int CurrentId { get => currentId; private set => currentId = value; }

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        currentId = PlayerPrefs.GetInt("CurrentID", -1);
        Debug.Log("Current ID: " + currentId);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /*if (GameRequestData.SelectedButton == ButtonType.NewGame)
        {
            GeneratePlayer();
            SetFollowTarget();
        }*/
        LoadGame(currentId);
        
        SaveSystem.ShowAllSaveSlots(); // Show all save slots after loading
        //GameRequestData.SelectedButton = ButtonType.None;
        //GameRequestData.SelectedSlot = -1; // Reset selected slot after loading
    }
    public void GeneratePlayer()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }
        playerInstance = Instantiate(playerPrefab, defaultSpawnPoint, Quaternion.identity);

       
    }
    public void SetFollowTarget()
    {
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.SetFollowTarget(playerInstance.transform);
        }
    }
    void LoadGame(int id)
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }

        GameData gameData = SaveSystem.LoadGameData(id);
        if (gameData == null)
        {
            Debug.LogError("Game data is null. Cannot load.");
            playerInstance = Instantiate(playerPrefab, defaultSpawnPoint, Quaternion.identity);
        }
        else
        {
            Vector2 spawnPoint;
            if (gameData != null)
            {

                //spawnPoint = new Vector2(gameData.posX, gameData.posY);
                spawnPoint = defaultSpawnPoint;
                Debug.LogError("this line now is for testing only, " +
                    "player enter new scene need to have a default position," +
                    "now is useing default as (0,0)");
            }
            else
            {
                spawnPoint = defaultSpawnPoint;
            }

            playerInstance = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
           
        }

        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.SetFollowTarget(playerInstance.transform);
        }
    }
    
}
