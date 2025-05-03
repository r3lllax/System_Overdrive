using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField] private EnemySpawnController ESC;
    [SerializeField] private float timeToIncreaseDifficulty = 30f;
    [SerializeField] private int enemyInIncrease = 10;
    private float timeToElite = 3;
    private float timeToRanged = 5;
    private float timer;
    private float GlobalTimer;

    private void Update()
    {
        Debug.Log(GlobalTimer);
        timer += Time.deltaTime;
        GlobalTimer += Time.deltaTime;
        if(timer>timeToIncreaseDifficulty){
            if(timeToElite>0){
                timeToElite--;
            }
            
            if(timeToRanged>0){
                timeToRanged--;
            }
            
            if(timeToElite==0){
                ESC.AddTypeToAvailble("Elite");
            }
            if(timeToRanged==0){
                ESC.AddTypeToAvailble("Ranged");
            }
            SessionData.AddProcentesFloat(ref SessionData.EnemySpeedMultiplier,5f);
            ESC.IncreaseHpStats();
            ESC.IncreaseMaxEnemies(enemyInIncrease);
            ESC.DecreaseSpawnInterval(0.2f);
            timer = 0;
        }
    }
}
