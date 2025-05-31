using System;
using System.Collections;
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
    private Image Tracker;
    private bool canContinue = true;
    private float fillAmount = 0f;
    private int levelUpsCount = 0;
    [SerializeField] private float IncreaseProcente = 0.2f;
    
    public void Awake()
    {
        Player = transform.parent.gameObject;
        Tracker = GameObject.FindWithTag("ProgressTracker").GetComponent<Image>();
        Instance = this;
        Panel = GameObject.FindWithTag("Panel"); 
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
        CurrentExpCount += Num;
        CheckLevelUpdate();
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
        Debug.Log($"levelUpsCount CurrentExpCount - {CurrentExpCount}");
        Debug.Log($"levelUpsCount ExpCountToNextLevel - {ExpCountToNextLevel}");
        Debug.Log($"levelUpsCount DEL - {CurrentExpCount / ExpCountToNextLevel}");
        Debug.Log($"levelUpsCount - {levelUpsCount}");

        StartCoroutine(LevelUpsRoutine(levelUpsCount));
        
        
        //Считает на некст сколько надо
        //Удаляю старые карты
        
    }

    private IEnumerator LevelUpsRoutine(int count)
    {
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
            levelUpsCount--;

        }
    }
    private void CheckLevelUpdate()
    {
        if (CurrentExpCount >= ExpCountToNextLevel)
        {
            LevelUP();
        }
        fillAmount = CurrentExpCount / ExpCountToNextLevel;
        Tracker.fillAmount = fillAmount;
    }
}
