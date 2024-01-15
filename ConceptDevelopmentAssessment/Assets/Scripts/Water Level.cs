using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaterLevel : MonoBehaviour
{
    public float startY = -50f;
    public float endY = 130f;
    public float totalTime = 120f; // 2 minutes
    public float elapsedTime = 0f;

    // Timer for the player
    public float playerTimer = 0f;
    public bool playerInWater = false;

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
            // Calculate the new Y position based on elapsed time
            float newY = Mathf.Lerp(startY, endY, elapsedTime / totalTime);

            // Update the object's position
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Check if the player is in the water
            if (playerInWater)
            {
                // Increment player timer
                playerTimer += Time.deltaTime;

                // Check if player has been in the water for more than 30 seconds
                if (playerTimer >= 10f)
                {
                    // Destroy the player object
                    DestroyPlayer();
                }
            }
        }
    }

    // Called when the player enters the water
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInWater = true;
            playerTimer = 0f;

            // Set vignette intensity to 1 when player enters the water
            if (lanternSpawner != null)
            {
                lanternSpawner.SetVignetteIntensity(1f);
            }
        }
    }

    // Called when the player exits the water
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInWater = false;
            playerTimer = 0f;
        }
    }

    // Destroy the player object and perform any other necessary actions
    private void DestroyPlayer()
    {
        // You can add more actions before destroying the player object if needed
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }
}