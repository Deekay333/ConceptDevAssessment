using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject player;
    public float t = 6f;
    // Start is called before the first frame update
    void Start()
    {
        text.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerScript>().iron >= 3)
        {
            t -= Time.deltaTime;
        }
        if(t > 0 && text.alpha < 1)
        {
            text.alpha += 0.01f;
        }
        else if(t < 0)
        {
            text.alpha -= 0.01f;
        }
    }
}
