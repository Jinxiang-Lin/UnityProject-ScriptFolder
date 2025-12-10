using UnityEngine;

public class Water : MonoBehaviour
{
    readonly float waterAmount = 100f; // Amount of water to be consumed
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        { 
            if (collision.CompareTag("Player"))
            {
                PlayerController player = collision.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.ConsumeWater(waterAmount);
                    Destroy(gameObject); // Destroy the water object after consumption
                }
            }
        }
    }
}
