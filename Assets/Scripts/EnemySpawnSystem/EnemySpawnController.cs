using System.Collections;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private EnemyPool pool;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnDistance = 10f;

    [SerializeField] private int maxEnemies;
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
        string TypeToSpawn = "Basic";
        GameObject enemy = pool.GetEnemy(TypeToSpawn);
        if(enemy!=null){
            currentEnemies++;
            enemy.GetComponent<Enemy>().SetHealth(5);
            enemy.GetComponent<Enemy>().pool = pool;
            enemy.GetComponent<Enemy>().ESC = this;
            enemy.GetComponent<Enemy>().SetEnemyType(TypeToSpawn);
            enemy.transform.position = camera.ViewportToWorldPoint(PointOutOfScreen());
            enemy.SetActive(true);
        }
    }
    public void OnEnemyDeath(){
        currentEnemies--;
    }


}
