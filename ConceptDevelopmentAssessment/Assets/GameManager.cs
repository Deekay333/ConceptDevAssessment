using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver;
    public void EndGame()
    {
        gameOver.SetActive(true);
    }
}
