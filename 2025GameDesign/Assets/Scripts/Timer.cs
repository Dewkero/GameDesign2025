using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float timerDuration = 3f * 60f;
    private float timer;

    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI seperator;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;
    [SerializeField]
    private GameManager gameManager;

    private bool timerRunning = false;
    private bool timesUp = false;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        ResetTimer();
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerRunning) return;
        { 
        timer -= Time.deltaTime;
        }
        if (timer<= 0f)
        {
            timer = 0f;
            timerRunning = false;

            if (!timesUp)
            {
                YouWin();
            }
        }

        UpdateTimerDisplay(timer);
    }

    private void ResetTimer()
    {
        timer = timerDuration;
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currenTime = string.Format("{0:00}{1:00}", minutes, seconds);
        firstMinute.text = currenTime[0].ToString();
        secondMinute.text = currenTime[1].ToString();
        firstSecond.text = currenTime[2].ToString();
        secondSecond.text = currenTime[3].ToString();
    }

    private void YouWin()
    {
        timesUp = true;
        gameManager.YouWin();
    }

    public void GameOver ()
    {
        timerRunning = false;
    }
}
