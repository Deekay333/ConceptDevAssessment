using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using TMPro;

public class LanternSpawner : MonoBehaviour
{
    public GameObject lanternPrefab; // Assigned the Lantern prefab in the Unity editor
    public Transform[] spawnPoints; // Assigned the empty GameObjects where Lantern can spawn

    [Range(0f, 1f)]
    public float spawnChance = 0.5f; // Adjust the spawn chance in the Unity editor

    [Header("Vignette Settings")]
    public GameObject postProcessVolumeObject; // Reference to the GameObject with PostProcessVolume component
    public float initialVignetteIntensity = 0.212f;
    public float currentVignetteIntensity;
    public float finalVignetteIntensity = 1f;
    public float timerDuration = 120f; // 2 minutes in seconds

    private float timer;
    private bool timerStarted = false;

    private LanternSpawner lanternSpawner; // Reference to the LanternSpawner script

    void Start()
    {
        SpawnLantern();
        StartTimer();

        // Find the LanternSpawner script in the scene
        lanternSpawner = FindObjectOfType<LanternSpawner>();
        if (lanternSpawner == null)
        {
            Debug.LogError("LanternSpawner script not found in the scene!");
        }
    }

    void SpawnLantern()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Generate a random number between 0 and 1
            float randomValue = Random.value;

            // Check if the random value is less than the spawn chance
            if (randomValue < spawnChance)
            {
                // Spawn the Lantern prefab at the current spawn point
                GameObject lanternObject = Instantiate(lanternPrefab, spawnPoint.position, Quaternion.identity);

                // Attach a script to the spawned Lantern object to handle collection
                LanternCollector lanternCollector = lanternObject.AddComponent<LanternCollector>();
                lanternCollector.SetSpawner(this);
            }
        }
    }

    void StartTimer()
    {
        timer = 0f;
        timerStarted = true;
    }

    void Update()
    {
        if (timerStarted)
        {
            timer += Time.deltaTime;

            // Calculate the percentage of time elapsed
            float progress = Mathf.Clamp01(timer / timerDuration);

            // Interpolate the vignette intensity based on the time progress
            float vignetteIntensity = Mathf.Lerp(initialVignetteIntensity, finalVignetteIntensity, progress);

            // Set the vignette intensity
            SetVignetteIntensity(vignetteIntensity);

            // Update currentVignetteIntensity
            currentVignetteIntensity = vignetteIntensity;

            // Check if the timer has reached its duration
            if (timer >= timerDuration)
            {
                timerStarted = false;
                Debug.Log("Timer finished!");
            }
        }
    }

    public void SetVignetteIntensity(float intensity)
    {
        // Modify vignette intensity using Post Processing Volume
        PostProcessVolume postProcessVolume = postProcessVolumeObject.GetComponent<PostProcessVolume>();

        if (postProcessVolume != null)
        {
            Vignette vignetteLayer;
            if (postProcessVolume.profile.TryGetSettings(out vignetteLayer))
            {
                vignetteLayer.intensity.value = intensity;
            }
        }
    }
}