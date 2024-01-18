using System.Xml.Serialization;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    // Water level parameters
    public float startY = -50f;
    public float endY = 130f;
    public float totalTime = 120f; // 2 minutes
    private float elapsedTime = 0f;

    // Player timer
    public float playerTimer = 0f;
    private bool playerInWater = false;

    private LanternSpawner lanternSpawner; // Reference to the LanternSpawner script
    void Start()
    {
        // Find the LanternSpawner script in the scene
        lanternSpawner = FindObjectOfType<LanternSpawner>();
        if (lanternSpawner == null)
        {
            Debug.LogError("LanternSpawner script not found in the scene!");
        }
    }

    void Update()
    {
        // Check if the total time has elapsed
        if (elapsedTime < totalTime)
        {
            // Update water level position
            float newY = Mathf.Lerp(startY, endY, elapsedTime / totalTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Check if the player is in the water
            if (playerInWater)
            {
                // Increment player timer
                playerTimer += Time.deltaTime;

                // Check if player has been in the water for more than 10 seconds
                if (playerTimer >= 4f)
                {
                    // Makes the player take damage
                    GameObject.FindWithTag("Player").GetComponent<PlayerScript>().health -= 1;
                    playerTimer = 0f;
                }
            }
        }
    }
}