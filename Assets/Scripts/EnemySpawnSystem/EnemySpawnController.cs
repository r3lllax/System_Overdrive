using System.Collections;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private EnemyPool pool;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnDistance = 10f;

    [SerializeField] private int maxEnemies;
    private string[] AvailbleTypes = {"Basic"};
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
        string TypeToSpawn = AvailbleTypes[Random.Range(0,AvailbleTypes.Length)];
        GameObject enemy = pool.GetEnemy(TypeToSpawn);
        Debug.Log(enemy);
        if(enemy!=null){
            currentEnemies++;
            Enemy EnemyComponent = enemy.GetComponent<Enemy>();
            EnemyComponent.SetHealth(5);
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


}
