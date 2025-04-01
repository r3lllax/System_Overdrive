using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static GameObject VirusPrefab;
    public GameObject Player;
    [SerializeField] private GameObject VirusPrefabLink;
    public class PoolSettings
    {
        public string Type;
        public GameObject prefab;
        public int poolSize;
    }
    private void Awake()
    {
        VirusPrefab = VirusPrefabLink;
        pools = new List<PoolSettings>
        {
            new PoolSettings{Type = "Basic", prefab = VirusPrefab,poolSize = 100},
            // new PoolSettings{Type = "Elite", prefab = VirusPrefab,poolSize = 100},
            // new PoolSettings{Type = "Boss", prefab = VirusPrefab,poolSize = 100},
        };
    }
    private Dictionary<string,Queue<GameObject>> poolDict;
    public List<PoolSettings> pools = new List<PoolSettings>();
    private void Start()
    {

        poolDict = new Dictionary<string, Queue<GameObject>>();
        foreach(PoolSettings pool in pools){
            Queue<GameObject> enemyQueue = new Queue<GameObject>();
            for(int i = 0;i<pool.poolSize;i++){
                var enemy = Instantiate(pool.prefab, new Vector3(0,0,0), Quaternion.identity);
                enemy.GetComponent<EnemyAI>().Player = Player.transform;
                enemy.SetActive(false);
                enemyQueue.Enqueue(enemy);
            }
            poolDict.Add(pool.Type,enemyQueue);
        }
    }
    public GameObject GetEnemy(string EnemyType){

        
        if(poolDict[EnemyType].Count == 0){
            PoolSettings pool = pools.Find(p => p.Type == EnemyType);
            if (pool != null)
            {
                GameObject newEnemy = Instantiate(pool.prefab, new Vector3(0,0,0), Quaternion.identity);
                newEnemy.GetComponent<EnemyAI>().Player = Player.transform;
                newEnemy.SetActive(false);
                poolDict[EnemyType].Enqueue(newEnemy);
            }
            else{
                return null;
            }
        }
        GameObject enemy = poolDict[EnemyType].Dequeue();
        enemy.SetActive(true);
        return enemy;
    }
    public void ReturnEnemy(GameObject enemy){
        //Возврат в определенный пул
        string EnemyType = enemy.GetComponent<Enemy>().GetEnemyType();
        poolDict[EnemyType].Enqueue(enemy);
        enemy.SetActive(false);
    }

}
