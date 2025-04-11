using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem Instance;
    
    private int CurrentLevel = 0;
    private float CurrentExpCount = 0;
    private float ExpCountToNextLevel = 100;
    [SerializeField] private float IncreaseProcente = 0.2f;
    
    public void Awake()
    {
        Instance = this;
    }
    public void AddCurrentExp(float Num){
        CurrentExpCount+=Num;
        CheckLevelUpdate();
    }
    private void CheckLevelUpdate(){
        if(CurrentExpCount >= ExpCountToNextLevel){
            CurrentLevel++;
            CurrentExpCount%=ExpCountToNextLevel;
            ExpCountToNextLevel = ExpCountToNextLevel + (ExpCountToNextLevel*IncreaseProcente);
        }
        Debug.Log($"CurrentLevel:{CurrentLevel},CurrentExp:{CurrentExpCount},ToNextLevel:{ExpCountToNextLevel}");
    }
}
