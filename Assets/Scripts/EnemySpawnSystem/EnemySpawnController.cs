using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private EnemyPool pool;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnDistance = 10f;

    [SerializeField] private int maxEnemies;
    private List<string> AvailbleTypes = new List<string>{"Basic"};
    private int currentEnemies;
    private new Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }


    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine(){
        while(true){

            if(currentEnemies < maxEnemies){
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

    private void SpawnEnemy(){
        
        //К примеру если прошло 5 минут то в avalibletypes добавить Elite, если 15 минут то добавить Boss
        string TypeToSpawn = AvailbleTypes[Random.Range(0,AvailbleTypes.Count)];
        GameObject enemy = pool.GetEnemy(TypeToSpawn);
        if(enemy!=null){
            currentEnemies++;
            Enemy EnemyComponent = enemy.GetComponent<Enemy>();
            int enemyHp = 1;
            
            if (EnemyComponent.GetEnemyType() == "Basic"){
                enemyHp = 5;
            }
            else if(EnemyComponent.GetEnemyType()=="Elite"){
                enemyHp = Random.Range(10,20);
                enemy.GetComponent<EnemyPathfinder>().SetMoveSpeed(Random.Range(0.9f,2f));
            }
            EnemyComponent.SetHealth(enemyHp);
            EnemyComponent.pool = pool;
            EnemyComponent.ESC = this;
            EnemyComponent.SetEnemyType(TypeToSpawn);
            enemy.transform.position = camera.ViewportToWorldPoint(PointOutOfScreen());
            enemy.SetActive(true);
        }
    }
    public void OnEnemyDeath(){
        currentEnemies--;
    }
    public void IncreaseMaxEnemies(int num){
        maxEnemies += num;
    }
    public void AddTypeToAvailble(string newType){
        AvailbleTypes.Add(newType);
    }
    public void DecreaseSpawnInterval(float number){
        spawnInterval-=number;
        if(spawnInterval<0){
            spawnInterval = 0;
        }
    }


}
