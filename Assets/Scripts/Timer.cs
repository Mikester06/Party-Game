using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue = 10;
    public Text timerText;
    public Text loseText;
    //public Text instructionText;
    private AudioController musicController;
    public bool isLoseMusicNotPlaying = true;

    void Start()
    {
        loseText.text = "";
        //instructionText.text = "Collect all the coins before the timer expires. You can only move while jumping.";
        musicController = FindObjectOfType<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            Time.timeScale = 0;
            loseText.text = "You lose! Press ESC to quit.";
            if (isLoseMusicNotPlaying)
            {
                musicController.LoseMusic();
                isLoseMusicNotPlaying = false;
            }
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
