using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IronSpawner : MonoBehaviour
{
    public GameObject ironPrefab; // Assign the Iron prefab in the Unity editor
    public Transform[] spawnPoints; // Assign the empty GameObjects where Iron can spawn
    [Range(0f, 1f)]
    public float spawnChance = 0.5f; // Adjust the spawn chance in the Unity editor
    private int playerScore = 0; // Player's score
    public TMP_Text ironScoreText; // Reference to the Text Mesh Pro UI Text element


    void Start()
    {
        SpawnIron();
        UpdateScoreText();
    }

    void SpawnIron()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Generate a random number between 0 and 1
            float randomValue = Random.value;

            // Check if the random value is less than the spawn chance
            if (randomValue < spawnChance)
            {
                // Spawn the Iron prefab at the current spawn point
                GameObject ironObject = Instantiate(ironPrefab, spawnPoint.position, Quaternion.identity);

                // Attach a script to the spawned Iron object to handle collection
                IronCollector ironCollector = ironObject.AddComponent<IronCollector>();
                ironCollector.SetSpawner(this);
            }
        }
    }

    public void CollectIron()
    {
        // Called when a Iron object is collected
        playerScore++;

        // Update the Text Mesh Pro UI Text element to display the new score
        UpdateScoreText();

        Debug.Log("Iron collected! Iron: " + playerScore);
    }

    void UpdateScoreText()
    {
        // Update the Text Mesh Pro UI Text element with the current score
        if (ironScoreText != null)
        {
            ironScoreText.text = "Iron: " + playerScore;
        }
    }
}

public class IronCollector : MonoBehaviour
{
    private IronSpawner spawner;

    public void SetSpawner(IronSpawner ironSpawner)
    {
        spawner = ironSpawner;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player (you may need to adjust the condition based on your game)
        if (other.CompareTag("Player"))
        {
            // Inform the spawner that the Iron object is collected
            spawner.CollectIron();

            // Destroy the collected Iron object
            Destroy(gameObject);

        }
    }
}
