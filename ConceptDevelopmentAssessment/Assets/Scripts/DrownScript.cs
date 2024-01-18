using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DrownScript : MonoBehaviour
{
    float t;
    bool drowning;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if (drowning)
        {
            t += Time.deltaTime;
            if (t > 6)
            {
                player.GetComponent<PlayerScript>().hit = true;
                t = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            drowning = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            drowning = false;
            t = 0;
        }
    }
}
