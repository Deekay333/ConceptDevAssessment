using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoalScript : MonoBehaviour
{
    public int health;
    public GameObject coalFragment;
    public GameObject player;
    private Vector2 direction;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Pickaxe")
        {
            transform.localScale = new Vector2(transform.localScale.x * 0.8f, transform.localScale.y * 0.8f);
            health--;
            if(health <= 0)
            {
                CoalSpawn(14, true);
                Destroy(gameObject);
            }
            else
            {
                CoalSpawn(3, false);
            }
            if (player.GetComponent<PlayerScript>().iron >= 3)
            {
                health--;
            }
        }
    }
    void CoalSpawn(int amount, bool destroyed)
    {
        while (amount > 0)
        {
            if(destroyed == true)
            {
                GameObject fragment = Instantiate(coalFragment, new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
                fragment.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3f, 5f), Random.Range(0f, 6f));
            }
            else
            {
                GameObject fragment = Instantiate(coalFragment, new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
                fragment.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f) + direction.x * 3, Random.Range(0f, 6f));
            }
            amount--;
        }
    }
    private void Update()
    {
        direction = player.transform.position - transform.position;
        direction.Normalize();
    }
}
