using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentScript : MonoBehaviour
{
    float t;
    float rand;
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        rand = Random.Range(-2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > (10 - rand))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<PlayerScript>().coal += 0.05f;
            Destroy(gameObject);
        }
    }
}
