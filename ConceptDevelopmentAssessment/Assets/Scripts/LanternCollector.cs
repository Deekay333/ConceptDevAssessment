using UnityEngine;

public class LanternCollector : MonoBehaviour
{
    private LanternSpawner spawner;
    private bool canCollect = false;

    void Start()
    {
        // Enable trigger detection
        GetComponent<Collider>().isTrigger = true;
    }

    public void SetSpawner(LanternSpawner lanternSpawner)
    {
        spawner = lanternSpawner;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player (you may need to adjust the condition based on your game)
        if (other.CompareTag("Player"))
        {
            // Enable lantern collection for the player
            canCollect = true;
        }
    }

    void OnTriggerExit(Collider other)
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
            // Destroy the collected Lantern object
            Destroy(gameObject);

            // Restart the timer and vignette effects
            spawner.ResetTimerAndVignette();
        }
    }
}