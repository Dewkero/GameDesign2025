using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float timerDuration = 4f * 60f;
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

    private bool timerRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        { 
        timer -= Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
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

    public void GameOver ()
    {
        timerRunning = false;
    }
}
