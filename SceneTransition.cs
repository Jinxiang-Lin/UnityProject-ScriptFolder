using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // Load the next scene or perform the transition logic here
            Debug.Log("Player has entered the transition area.");
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            CurtainTransition.Instance.TransitionToScene(nextSceneIndex);
            // Example: SceneManager.LoadScene("NextSceneName");
        }
    }
}
