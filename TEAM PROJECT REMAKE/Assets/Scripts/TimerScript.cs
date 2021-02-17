using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    float timeLeft = 90.0f;
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

        Destroy(gameObject, 90);

        timeLeft -= Time.deltaTime;
    }
}