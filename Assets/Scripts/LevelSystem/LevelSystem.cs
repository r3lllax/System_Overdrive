using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem Instance;
    
    private int CurrentLevel = 0;
    private float CurrentExpCount = 0;
    private float ExpCountToNextLevel = 100;
    private GameObject Panel;
    private GameObject Player;
    private GameObject Tracker;
    private bool canContinue = true;
    private float fillAmount = 0f;
    private int levelUpsCount = 0;
    private GameObject LevelUpObj;
    private TextMeshProUGUI LevelTextUI;
    [SerializeField] private float IncreaseProcente = 0.1f;

    private Queue<float> expQueue = new Queue<float>();

    public void Awake()
    {
        Player = transform.parent.gameObject;
        LevelUpObj = Player.transform.Find("LevelUP").gameObject;
        Tracker = GameObject.FindWithTag("ProgressTracker");
        Instance = this;
        Panel = GameObject.FindWithTag("Panel");
        LevelTextUI = Tracker.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        LevelTextUI.text = $"Level {CurrentLevel + 1}";
    }
    private void Start() {
        StartCoroutine(levelCheckV3());
    }
    public void Continue(bool newState)
    {
        canContinue = newState;
    }
    public float GetFillAmount()
    {
        return fillAmount;
    }
    public int GetLevelUpsCount()
    {
        return levelUpsCount;
    }
    public void AddCurrentExp(float Num)
    {
        AddExp(Num);
    }
    private void Update() {
        //CheckLevelUpdateV2();
    }
    [ContextMenu("LEVELUP")]
    public void LevelUP(){
        DamageUI.Instance.AddText(1,transform.position,"LevelUP");

        levelUpsCount = 0;
        while (CurrentExpCount > 0f)
        {
            if (CurrentExpCount != 0)
            {
                CurrentExpCount = CurrentExpCount - ExpCountToNextLevel < 0 ? 0 : CurrentExpCount - ExpCountToNextLevel;
                ExpCountToNextLevel = ExpCountToNextLevel + (ExpCountToNextLevel * IncreaseProcente);
                if (CurrentExpCount > 0)
                {
                    levelUpsCount++;
                    CurrentLevel++;
                }
            }
        }
        
        StartCoroutine(LevelUpsRoutine(levelUpsCount));
    }

    public void AddExp(float amt)
    {
        expQueue.Enqueue(amt);
    }

    private float CalculateExpDelay(float expChunk, int currentLevel, float expRequired)
    {
        float baseDelay = 0.5f;
        float levelRedFac = 0.6f;
        float chunkSizeFactor = 0.002f;
        float leveldelay = baseDelay * Mathf.Pow(levelRedFac, currentLevel - 1);
        float chunkMP = 1 + (chunkSizeFactor * expChunk);
        return leveldelay * chunkMP;
    }

    private IEnumerator levelCheckV3()
    {
        while (true)
        {
            if (expQueue.Count > 0)
            {
                var exp = expQueue.Dequeue();
                CurrentExpCount += exp;
                CheckLevelUpdate();
                yield return new WaitForSeconds(CalculateExpDelay(exp, CurrentLevel, ExpCountToNextLevel));
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }




        }
    }
   
    private IEnumerator LevelUpsRoutine(int count)
    {
        Debug.Log(count);
        LevelUpObj.GetComponent<Animator>().SetTrigger("LevelUP");
        for (int i = 0; i < count; i++)
        {

            canContinue = false;
            Panel.GetComponent<UiUpgradePanel>().DeleteCards();
            Player.GetComponent<UpgradesController>().FilterUpgrades();
            Player.GetComponent<UpgradesController>().GenerateOfferUpdates(SessionData.offersCount);
            if (i == 0)
            {
                Panel.GetComponent<Animator>().SetTrigger("toggle");
            }

            yield return new WaitUntil(() => canContinue);

        }
    }
    

   

    private void CheckLevelUpdate()
    {

        if (CurrentExpCount >= ExpCountToNextLevel)
        {

            LevelUP();
        }
        fillAmount = CurrentExpCount / ExpCountToNextLevel;
        LevelTextUI.text = $"Level {CurrentLevel + 1}";
        Tracker.GetComponent<Image>().fillAmount = fillAmount;
    }
}
