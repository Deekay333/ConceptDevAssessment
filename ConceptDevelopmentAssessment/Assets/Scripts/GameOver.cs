using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private float t;
    public GameObject GameObject;
    public GameObject GameObject2;
    public GameObject GameObject3;
    public GameObject BlackScreen;
    public TextMeshProUGUI text;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    private bool fade;
    private bool menu;
    public void Restart()
    {
        fade = true;
        menu = false;
    }
    public void MainMenu()
    {
        fade = true;
        menu = true;
    }
    private void FixedUpdate()
    {
        if (text.color.a < 1 && !fade)
        {
            t += Time.fixedDeltaTime;
            GameObject.GetComponent<Image>().color += new Color(0, 0, 0, 0.005f);
            GameObject2.GetComponent<Image>().color += new Color(0, 0, 0, 0.005f);
            GameObject3.GetComponent<Image>().color += new Color(0, 0, 0, 0.005f);
            BlackScreen.GetComponent<RawImage>().color += new Color(0, 0, 0, 0.005f);
            text.color += new Color(0, 0, 0, 0.005f);
            text2.color += new Color(0, 0, 0, 0.005f);
            text3.color += new Color(0, 0, 0, 0.005f);
        }
        else if(fade && text.color.a > 0)
        {
            GameObject.GetComponent<Image>().color -= new Color(0, 0, 0, 0.01f);
            GameObject2.GetComponent<Image>().color -= new Color(0, 0, 0, 0.01f);
            GameObject3.GetComponent<Image>().color -= new Color(0, 0, 0, 0.01f);
            text.color -= new Color(0, 0, 0, 0.01f);
            text2.color -= new Color(0, 0, 0, 0.01f);
            text3.color -= new Color(0, 0, 0, 0.01f);
        }
        else if(text.color.a <= 0 && menu)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            this.gameObject.SetActive(false);
            fade = false;
        }
        else if (text.color.a <= 0 && !menu)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            this.gameObject.SetActive(false);
            fade = false;
        }
    }
}
