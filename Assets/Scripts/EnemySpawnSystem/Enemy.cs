using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private string Type;
    [SerializeField] private int health;
    public EnemySpawnController ESC;
    public EnemyPool pool;
    public void SetHealth(int HP){
        health = HP;
    }
    //Пока не убирать, позже переделать через инит, так как через ссылки на скрипты че то не оч мне нравится
    public void Init(EnemySpawnController SpawnController,EnemyPool enemyPool){
        ESC = SpawnController;
        pool = enemyPool;
    }
    public void TakeDamage(int Damage){
        health -=Damage;
        if(health<=0){
            Die();
        }
    }
    [ContextMenu("Die")]
    public void Die(){
        //Оюбращение к контроллеру с сообщением о смерти
        pool.ReturnEnemy(gameObject);
        ESC.OnEnemyDeath();
        
        //Обращение к пулу с целью возврата
    }
    public string GetEnemyType(){
        return Type;
    }
    public void SetEnemyType(string type){
        Type = type;
    }
}
