using UnityEngine;
using TMPro;

public class WaterLevel : MonoBehaviour
{
    // Water level parameters
    [Header("Water")]
    public float startY = -50f;
    public float endY = 130f;
    public float totalTime = 120f; // 2 minutes
    private float elapsedTime = 0f;

    //Scripts
    [Header("Scripts")]
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
        }
    }
}
