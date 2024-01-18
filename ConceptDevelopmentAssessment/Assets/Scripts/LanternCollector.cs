using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LanternCollector : MonoBehaviour
{
    private LanternSpawner spawner;
    private bool canCollect = false;

    void Start()
    {
        // Enable trigger detection
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void SetSpawner(LanternSpawner lanternSpawner)
    {
        spawner = lanternSpawner;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the player (you may need to adjust the condition based on your game)
        if (other.CompareTag("Player"))
        {
            // Enable lantern collection for the player
            canCollect = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collided object is the player (you may need to adjust the condition based on your game)
        if (other.CompareTag("Player"))
        {
            // Disable lantern collection when the player exits the trigger zone
            canCollect = false;
        }
    }

    void Update()
    {
        // Check if the player presses the "E" key and can collect the lantern
        if (canCollect && Input.GetKeyDown(KeyCode.E))
        {
            // Access the LanternSpawner script and reset the timer
            if (spawner != null)
            {
                spawner.ResetTimer();

                // Destroy the collected Lantern object
                Destroy(gameObject);
            }
        }
    }
}
