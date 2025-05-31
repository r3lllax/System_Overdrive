using System;
using TMPro;
using UnityEngine;

public class TimerForUI : MonoBehaviour
{
    private float GlobalTimer = 0;
    private float BossTime = 600f;
    private int timerUI = 0;
    private TextMeshProUGUI text;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        GlobalTimer += Time.deltaTime;
        BossTime -= Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(BossTime);
        string str = time.ToString(@"mm\:ss");
        timerUI = Convert.ToInt32(GlobalTimer);
        text.text = str.ToString();
    }
}
