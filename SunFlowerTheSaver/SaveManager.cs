using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    public SaverState CurrentState { get; set; }
    public SavedState SavedState { get; set; }
    public UnsavedState UnsavedState { get; set; }
    public SavingTransitionState SavingTransitionState { get; set; }
    public Animator Ani { get => animator; set => animator = value; }

    Animator animator;
    public GameData GameDataSaved { get; private set; } // Assuming GameData is a class that holds the game state
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        SavedState = new SavedState(this);
        UnsavedState = new UnsavedState(this);
        SavingTransitionState = new SavingTransitionState(this);
        CurrentState = UnsavedState; // Start in the unsaved state
        CurrentState.Enter(); // Enter the initial state
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (CurrentState is SavedState == false)
            {
                Scene scene = SceneManager.GetActiveScene();
                GameDataSaved = new GameData()
                {
                    id = PlayerPrefs.GetInt("CurrentID", -1),
                    current_water_level = collision.GetComponent<PlayerController>().WateringState.CurrentWaterLevel,
                    currentSceneIndex = scene.buildIndex,
                    posX = collision.transform.position.x,
                    posY = collision.transform.position.y,
                    sceneName = scene.name, // Use the scene name instead of index
                    lastSaveTime = System.DateTime.Now // Optional: Set the last save time
                };
                Debug.Log($"saved id is {GameDataSaved.id}");
                ChangeSaverState(SavingTransitionState); // Change to saved state when player enters the trigger
            }
            
        }
    }
    public void ChangeSaverState(SaverState newState)
    { 
        CurrentState.Exit(); // Exit the current state
        CurrentState = newState;
        CurrentState.Enter(); // Enter the new state
    }
}
