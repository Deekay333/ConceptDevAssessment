using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI TMPTitle;
    public TextMeshProUGUI TMPPlay;
    public TextMeshProUGUI TMPInstructions;
    public TextMeshProUGUI TMPQuit;
    public Image button;
    public float t;
    private bool play;
    private bool loaded;
    private bool animated;
    public new GameObject animation;
    public VideoPlayer videoPlayer;
    private bool looped;
    private void Start()
    {
        animation.SetActive(false);
        videoPlayer.Stop();
        animated = false;
        loaded = false;
        play = false;
        t = 0;

    }
    public void PlayGame()
    {
        play = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void FixedUpdate()
    {
        if (play)
        {
            t += Time.fixedDeltaTime;
            if(t < 2)
            {
                TMPTitle.alpha -= 0.02f;
                TMPPlay.alpha -= 0.02f;
                TMPInstructions.alpha -= 0.02f;
                TMPQuit.alpha -= 0.02f;
                button.color -= new Color(0, 0, 0, 0.02f);
            }
            else if(t > 2.1f)
            {
                TMPTitle.alpha = 1;
                TMPPlay.alpha = 1;
                TMPInstructions.alpha = 1;
                TMPQuit.alpha = 1;
                button.color = new Color(0, 0, 0, 1);
            }
        }
        if(t >= 2 && animated == false)
        {
            videoPlayer.Play();
            animated = true;
        }
        if(t >= 2.1f && animated == true && looped == false)
        {
            animation.SetActive(true);
            looped = true;
        }
        if(t >= 10.1 && loaded == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            loaded = true;
        }
    }
}
