using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseSpawner : MonoBehaviour
{
    public GameObject[] collectiblePrefabs; // Assign the collectible prefabs in the Unity editor
    public Transform[] spawnPoints; // Assign the empty GameObjects where collectibles can spawn

    [Range(0f, 1f)]
    public float spawnChance = 0.5f; // Adjust the spawn chance in the Unity editor

    private int playerScore = 0; // Player's score

    public TMP_Text scoreText; // Reference to the Text Mesh Pro UI Text element

    void Start()
    {
        SpawnCollectible();
        UpdateScoreText();
    }

    void SpawnCollectible()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            float randomValue = Random.value;

            if (randomValue < spawnChance)
            {
                int randomIndex = Random.Range(0, collectiblePrefabs.Length);
                GameObject collectibleObject = Instantiate(collectiblePrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

                CollectibleCollector collector = collectibleObject.AddComponent<CollectibleCollector>();
                collector.SetSpawner(this);
            }
        }
    }

    public void Collect()
    {
        playerScore++;
        UpdateScoreText();
        Debug.Log("Collectible collected! Score: " + playerScore);
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + playerScore;
        }
    }
}

public class CollectibleCollector : MonoBehaviour
{
    private BaseSpawner spawner;

    public void SetSpawner(BaseSpawner spawner)
    {
        this.spawner = spawner;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawner.Collect();
            Destroy(gameObject);
        }
    }
}
