using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class LanternSpawner : MonoBehaviour
{
    public GameObject lanternPrefab; // Assigned the Lantern prefab in the Unity editor
    public Transform[] spawnPoints; // Assigned the empty GameObjects where Lantern can spawn

    [Range(0f, 1f)]
    public float spawnChance = 0.5f; // Adjust the spawn chance in the Unity editor

    [Header("Vignette Settings")]
    public GameObject postProcessVolumeObject; // Reference to the GameObject with PostProcessVolume component
    public float initialVignetteIntensity = 0.212f;
    public float finalVignetteIntensity = 1f;

    [Header("Timer Settings")]
    public float timerDuration = 120f; // 2 minutes in seconds
    private float timer;
    private bool timerStarted = false;
    private bool cooldownActive = false;
    public float cooldownDuration = 3f; // 3 seconds cooldown
    private float cooldownTimer = 0f;

    void Start()
    {
        SpawnLantern();
        StartTimer();
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
        if (timerStarted && !cooldownActive)
        {
            timer += Time.deltaTime;

            float progress = Mathf.Clamp01(timer / timerDuration);
            float vignetteIntensity = Mathf.Lerp(initialVignetteIntensity, finalVignetteIntensity, progress);
            SetVignetteIntensity(vignetteIntensity);

            if (timer >= timerDuration)
            {
                StartCooldown();
                Debug.Log("Timer finished!");
            }
        }

        // Check if cooldown is active
        if (cooldownActive)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer <= cooldownDuration)
            {
                // Calculate progress during cooldown
                float cooldownProgress = Mathf.Clamp01(cooldownTimer / cooldownDuration);

                // Interpolate vignette intensity during cooldown
                float vignetteIntensity = Mathf.Lerp(finalVignetteIntensity, initialVignetteIntensity, cooldownProgress);
                SetVignetteIntensity(vignetteIntensity);
            }
            else
            {
                ResetTimerAndVignette();
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

    public void StartCooldown()
    {
        cooldownActive = true;
        cooldownTimer = 0f;

        // Set vignette intensity to its final value instantly
        SetVignetteIntensity(finalVignetteIntensity);
    }

    public void ResetTimerAndVignette()
    {
        timer = 0f;
        timerStarted = true;
        cooldownActive = false;
        cooldownTimer = 0f;

        // Reset vignette intensity
        SetVignetteIntensity(initialVignetteIntensity);
    }

    //DOESNT WORK Set vignette to 1 if in water
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            // Reset vignette intensity
            SetVignetteIntensity(finalVignetteIntensity);
        }
    }
}
