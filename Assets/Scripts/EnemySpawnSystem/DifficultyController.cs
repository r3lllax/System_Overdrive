using System.Collections;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField] private EnemySpawnController ESC;
    [SerializeField] private float timeToIncreaseDifficulty = 30f;
    [SerializeField] private int enemyInIncrease = 5;
    private float timeToElite = 3;
    private float timeToRanged = 5;
    private float timeToBoss = 20/**1*/;
    private float timer;

    private IEnumerator Boss()
    {
        ESC.ClearAvailableTypes();
        ESC.AddTypeToAvailble("Boss");
        yield return new WaitForSeconds(4f);
        ESC.AddTypeToAvailble("Basic");
        ESC.AddTypeToAvailble("Elite");
        ESC.AddTypeToAvailble("Ranged");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToIncreaseDifficulty)
        {
            SessionData.AddValueFloat(ref SessionData.ExpMultiplier, 0.05f);
            if (timeToElite > 0)
            {
                timeToElite--;
            }

            if (timeToRanged > 0)
            {
                timeToRanged--;
            }
            if (timeToBoss > 0)
            {
                timeToBoss--;
            }

            if (timeToElite == 0)
            {
                ESC.AddTypeToAvailble("Elite");
            }
            if (timeToRanged == 0)
            {
                ESC.AddTypeToAvailble("Ranged");
            }
            if (timeToBoss == 0)
            {
                StopAllCoroutines();
                StartCoroutine(Boss());
            } 
            
            SessionData.AddProcentesFloat(ref SessionData.EnemySpeedMultiplier, 3.5f);
            ESC.IncreaseHpStats();
            ESC.IncreaseMaxEnemies(enemyInIncrease);
            ESC.DecreaseSpawnInterval(0.2f);
            timer = 0;
        }
    }
}
