using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoalSpawner : MonoBehaviour
{
    public GameObject coalPrefab; // Assign the Coal prefab in the Unity editor
    public Transform[] spawnPoints; // Assign the empty GameObjects where Coal can spawn

    [Range(0f, 1f)]
    public float spawnChance = 0.5f; // Adjust the spawn chance in the Unity editor

    private int playerScore = 0; // Player's score

    public TMP_Text scoreText; // Reference to the Text Mesh Pro UI Text element


    void Start()
    {
        SpawnCoal();
        UpdateScoreText();
    }

    void SpawnCoal()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Generate a random number between 0 and 1
            float randomValue = Random.value;

            // Check if the random value is less than the spawn chance
            if (randomValue < spawnChance)
            {
                // Spawn the Coal prefab at the current spawn point
                GameObject coalObject = Instantiate(coalPrefab, spawnPoint.position, Quaternion.identity);

                // Attach a script to the spawned Coal object to handle collection
                CoalCollector coalCollector = coalObject.AddComponent<CoalCollector>();
                coalCollector.SetSpawner(this);
            }
        }
    }

    public void CollectCoal()
    {
        // Called when a Coal object is collected
        playerScore++;

        // Update the Text Mesh Pro UI Text element to display the new score
        UpdateScoreText();

        Debug.Log("Coal collected! Coal: " + playerScore);
    }

    void UpdateScoreText()
    {
        // Update the Text Mesh Pro UI Text element with the current score
        if (scoreText != null)
        {
            scoreText.text = "Coal: " + playerScore;
        }
    }
}

public class CoalCollector : MonoBehaviour
{
    private CoalSpawner spawner;

    public void SetSpawner(CoalSpawner coalSpawner)
    {
        spawner = coalSpawner;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player (you may need to adjust the condition based on your game)
        if (other.CompareTag("Player"))
        {
            // Inform the spawner that the Coal object is collected
            spawner.CollectCoal();

            // Destroy the collected Coal object
            Destroy(gameObject);

        }
    }
}