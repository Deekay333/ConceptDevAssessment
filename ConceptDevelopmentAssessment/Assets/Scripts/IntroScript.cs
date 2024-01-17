using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    private float t;
    private RawImage image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > 1 && image.color.a > 0)
        { 
            image.color -= new Color(0, 0, 0, 0.005f);
        }
        if(image.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
