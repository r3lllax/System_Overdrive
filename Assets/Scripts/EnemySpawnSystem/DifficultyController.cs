using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField] private EnemySpawnController ESC;
    [SerializeField] private float timeToIncreaseDifficulty = 30f;
    [SerializeField] private int enemyInIncrease = 15;
    private float timeToElite = 2;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>timeToIncreaseDifficulty){
            if(timeToElite>0){
                timeToElite--;
            }
            else{
                timeToElite=-1;
            }
            if(timeToElite==0){
                ESC.AddTypeToAvailble("Elite");
            }
            ESC.IncreaseMaxEnemies(enemyInIncrease);
            ESC.DecreaseSpawnInterval(0.2f);
            timer = 0;
        }
    }
}
