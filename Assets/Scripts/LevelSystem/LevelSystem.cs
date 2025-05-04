using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem Instance;
    
    private int CurrentLevel = 0;
    private float CurrentExpCount = 0;
    private float ExpCountToNextLevel = 100;
    private GameObject Panel;
    private GameObject Player;
    [SerializeField] private float IncreaseProcente = 0.2f;
    
    public void Awake()
    {
        Instance = this;
        Panel = GameObject.FindWithTag("Panel"); 
    }
    public void AddCurrentExp(float Num){
        CurrentExpCount+=Num;
        CheckLevelUpdate();
    }
    [ContextMenu("LEVELUP")]
    public void LevelUP(){
        DamageUI.Instance.AddText(1,transform.position,"LevelUP");
        CurrentLevel++;
        CurrentExpCount%=ExpCountToNextLevel;
        ExpCountToNextLevel = ExpCountToNextLevel + (ExpCountToNextLevel*IncreaseProcente);
        Player = GameObject.FindWithTag("Player").transform.GetChild(0).gameObject;
        Panel.GetComponent<UiUpgradePanel>().DeleteCards();
        Player.GetComponent<UpgradesController>().FilterUpgrades();
        Player.GetComponent<UpgradesController>().GenerateOfferUpdates(3);
        Panel.GetComponent<Animator>().SetTrigger("toggle");
    }
    private void CheckLevelUpdate(){
        if(CurrentExpCount >= ExpCountToNextLevel){
            LevelUP();
        }
        Debug.Log($"CurrentLevel:{CurrentLevel},CurrentExp:{CurrentExpCount},ToNextLevel:{ExpCountToNextLevel}");
    }
}
