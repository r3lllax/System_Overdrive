using System;
using TMPro;
using UnityEngine;

public class TimerForUI : MonoBehaviour
{
    private float GlobalTimer = 0;
    private int timerUI = 0;
    private TextMeshProUGUI text;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        GlobalTimer += Time.deltaTime;
        timerUI = Convert.ToInt32(GlobalTimer);
        text.text = timerUI.ToString();
    }
}
