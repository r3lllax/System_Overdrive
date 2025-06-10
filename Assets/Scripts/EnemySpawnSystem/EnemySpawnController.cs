using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private EnemyPool pool;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnDistance = 10f;

    [SerializeField] private int maxEnemies;
    private List<string> AvailbleTypes = new List<string> { "Basic" };
    private int currentEnemies;
    private int MinBasicHP = 3;
    private int MaxBasicHP = 5;
    private int MinRangedHP = 2;
    private int MaxRangedHP = 3;
    private int MinEliteHP = 10;
    private int MaxEliteHP = 20;
    private new Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }


    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {

            if (currentEnemies < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private Vector3 PointOutOfScreen()
    {
        float X = Random.Range(-0.5f, 1.5f);
        float Y = Random.Range(-0.5f, 1.5f);
        if (Random.value > 0.5f) X = X < 0.5f ? -0.5f : 1.5f;
        else Y = Y < 0.5f ? -0.5f : 1.5f;
        return new Vector3(X, Y, spawnDistance);
    }

    public void IncreaseHpStats()
    {
        MinBasicHP++;
        MaxBasicHP++;
        MinEliteHP++;
        MaxEliteHP++;
        MinRangedHP++;
        MaxRangedHP++;
    }

    private void SpawnEnemy()
    {
        string TypeToSpawn = AvailbleTypes[Random.Range(0, AvailbleTypes.Count)];
        GameObject enemy = pool.GetEnemy(TypeToSpawn);
        if (enemy != null)
        {
            currentEnemies++;
            Enemy EnemyComponent = enemy.GetComponent<Enemy>();
            int enemyHp = 1;

            if (EnemyComponent.GetEnemyType() == "Basic")
            {
                enemyHp = Random.Range(MinBasicHP, MaxBasicHP);
                EnemyComponent.SetLowExpTh(20 * SessionData.ExpMultiplier);
                EnemyComponent.SetHighExpTh(30 * SessionData.ExpMultiplier);
            }
            else if (EnemyComponent.GetEnemyType() == "Elite")
            {
                enemyHp = Random.Range(MinEliteHP, MaxEliteHP);
                EnemyComponent.SetLowExpTh(30 * SessionData.ExpMultiplier);
                EnemyComponent.SetHighExpTh(60 * SessionData.ExpMultiplier);

                enemy.GetComponent<EnemyPathfinder>().SetMoveSpeed(Random.Range(0.9f, 2f));

            }
            else if (EnemyComponent.GetEnemyType() == "Ranged")
            {
                enemyHp = Random.Range(MinRangedHP, MaxRangedHP);
                EnemyComponent.SetLowExpTh(20 * SessionData.ExpMultiplier);
                EnemyComponent.SetHighExpTh(30 * SessionData.ExpMultiplier);

                enemy.GetComponent<EnemyPathfinder>().SetMoveSpeed(Random.Range(2f, 3f));

            }
            else if (EnemyComponent.GetEnemyType() == "Boss")
            {
                enemyHp = 10000;
                EnemyComponent.SetLowExpTh(1000 * SessionData.ExpMultiplier);
                EnemyComponent.SetHighExpTh(3000 * SessionData.ExpMultiplier);

                enemy.GetComponent<EnemyPathfinder>().SetMoveSpeed(Random.Range(1.5f, 2f));

            }
            EnemyComponent.SetHealth(enemyHp);
            EnemyComponent.pool = pool;
            EnemyComponent.ESC = this;
            EnemyComponent.SetEnemyType(TypeToSpawn);
            enemy.transform.position = camera.ViewportToWorldPoint(PointOutOfScreen());
            enemy.SetActive(true);
        }
    }
    public void OnEnemyDeath()
    {
        currentEnemies--;
    }
    public void IncreaseMaxEnemies(int num)
    {
        maxEnemies += num;
    }
    public void ClearAvailableTypes()
    {
        AvailbleTypes.Clear();
    }
    public void AddTypeToAvailble(string newType)
    {
        AvailbleTypes.Add(newType);
    }
    public void DecreaseSpawnInterval(float number)
    {
        spawnInterval -= number;
        if (spawnInterval < 0)
        {
            spawnInterval = 0.5f;
        }
    }
    public void SetSpawnInterval(float value)
    {
        spawnInterval = value;
    }
     public void SetMaxEnemies(int value)
    {
        maxEnemies = value;
    }


}
