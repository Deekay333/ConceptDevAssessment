using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceScript : MonoBehaviour
{
    public int health = 3;
    public GameObject coalFragment;
    public GameObject ironFragment;
    public GameObject player;
    private Vector2 direction;
    private bool iron;
    private void Start()
    {
        player = GameObject.Find("Player");
        if(this.gameObject.tag == "Iron")
        {
            iron = true;
            health = 5;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Pickaxe")
        {
            transform.localScale = new Vector3(transform.localScale.x * 0.8f, transform.localScale.y * 0.8f, transform.localScale.z * 0.8f);
            health--;
            if (iron && health <= 0)
            {
                IronSpawn(12, true);
                Destroy(gameObject);
            }
            else if (iron)
            {
                IronSpawn(2, false);
            }
            else if (!iron && health <= 0)
            {
                CoalSpawn(14, true);
                Destroy(gameObject);
            }
            else
            {
                CoalSpawn(3, false);
            }
            if(player.GetComponent<PlayerScript>().iron >= 3)
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
                GameObject fragment = Instantiate(coalFragment, new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 4.97f), Quaternion.identity);
                fragment.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3f, 5f), Random.Range(0f, 6f));
            }
            else
            {
                GameObject fragment = Instantiate(coalFragment, new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 4.97f), Quaternion.identity);
                fragment.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f) + direction.x * 3, Random.Range(0f, 6f));
            }
            amount--;
        }
    }
    void IronSpawn(int amount, bool destroyed)
    {
        while (amount > 0)
        {
            if (destroyed == true)
            {
                GameObject fragment = Instantiate(ironFragment, new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 4.97f), Quaternion.identity);
                fragment.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3f, 5f), Random.Range(0f, 6f));
            }
            else
            {
                GameObject fragment = Instantiate(ironFragment, new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 4.97f), Quaternion.identity);
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
