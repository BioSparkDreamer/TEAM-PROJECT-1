using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    public static float timeLeft = 90.0f;

    void Start()
    {
        startTime = Time.time;
        timerText = GetComponent<Text>();
    }

    void Update()
    {
        float t = Time.time - startTime;

        string seconds = (t % 60).ToString("f1");

        timerText.text = "" + timeLeft.ToString("f1");

        if (timeLeft <= 0)
        {
            Destroy(gameObject);
        }

        timeLeft -= Time.deltaTime;
    }
}