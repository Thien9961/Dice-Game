using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(TextMeshProUGUI))]
public class EventTimer : MonoBehaviour
{
    public long eventDuration=360000;
    float currentTime;
    string formattedTime;

    string GetFormattedTime(float time)
    {
        int days = Mathf.FloorToInt(time / 86400); 
        int hours = Mathf.FloorToInt((time % 86400) / 3600); 
        int minutes = Mathf.FloorToInt((time % 3600) / 60); 
        int seconds = Mathf.FloorToInt(time % 60); 

        string dd = days.ToString("00");
        string hh = hours.ToString("00");
        string mm = minutes.ToString("00");
        string ss = seconds.ToString("00");

        return $"Thời gian sự kiện: {dd} ngày {hh}:{mm}:{ss}";
    }

    // Start is called before the first frame update
    void Start()
    {
        timeText=GetComponent<TextMeshProUGUI> ();
    }

    // Update is called once per frame
    TextMeshProUGUI timeText;

    void Update()
    {
        float currentTime = eventDuration-Time.time;
        string formattedTime = GetFormattedTime(currentTime);

        timeText.text = formattedTime;
    }

}
