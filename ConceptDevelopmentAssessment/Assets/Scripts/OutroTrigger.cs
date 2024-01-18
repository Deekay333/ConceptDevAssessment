using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroTrigger : MonoBehaviour
{
    public GameObject YouWin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        YouWin.SetActive(true);
    }
}
