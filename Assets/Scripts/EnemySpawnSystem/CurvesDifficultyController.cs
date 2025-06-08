using System;
using System.Collections;
using UnityEngine;

public class CurvesDifficultyController : MonoBehaviour
{
    [SerializeField] private EnemySpawnController ESC;
    [SerializeField] private float timeToIncreaseDifficulty = 30f;
    [SerializeField] private int enemyInIncrease = 5;
    
    [Header("Curves")]
    [SerializeField] private AnimationCurve SpawnInterval;
    [SerializeField] private AnimationCurve MaxEnemies;
    [SerializeField] private float minEnemies = 15;
    [SerializeField] private float maxEnemies = 700;
    [SerializeField] private float minSpawnInterval = 0;
    [SerializeField] private float maxSpawnInterval = 2;

    private float timeToElite = 3;
    private float timeToRanged = 5;
    private float timeToBoss = 20;
    private float timer;
    private float CurveTimer;

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
        CurveTimer += Time.deltaTime;
        float currentInterval = Mathf.Clamp(SpawnInterval.Evaluate(CurveTimer / SessionData.totalGameTime), 0f, 2f);
        int currentEnemies = Mathf.RoundToInt(Mathf.Lerp(minEnemies, maxEnemies, MaxEnemies.Evaluate(CurveTimer / SessionData.totalGameTime)));
        ESC.SetSpawnInterval(currentInterval);
        ESC.SetMaxEnemies(currentEnemies);
        if (timer > timeToIncreaseDifficulty)
        {
            SessionData.AddValueFloat(ref SessionData.ExpMultiplier, 0.2f);
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
            timer = 0;
        }
    }
}
