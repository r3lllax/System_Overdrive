using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject DeathEffect;
    public GameObject DamageEffect;
    [SerializeField]private string Type;
    [SerializeField] private int health;
    public EnemySpawnController ESC;
    public EnemyPool pool;
    private Knockback knockback;
    private Flash flash;
    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }
    public void SetHealth(int HP){
        health = HP;
    }
    //Пока не убирать, позже переделать через инит, так как через ссылки на скрипты че то не оч мне нравится
    public void Init(EnemySpawnController SpawnController,EnemyPool enemyPool){
        ESC = SpawnController;
        pool = enemyPool;
    }
    public void CheckDeath(){
        if(health<=0){
            knockback.Refresh();
            flash.Refresh();
            Die();
        }
    }
    public void TakeDamage(int Damage, float strength){
        health = health-Damage<=0 ? 0 : health-=Damage;
        flash.StartCoroutine(flash.FlashRoutine());
        knockback.GetKnockBack(PlayerController.Instance.transform,strength);
        Instantiate(DamageEffect,transform.position,Quaternion.identity);
        
        
    }
    public void KnockBackWithoutDamage(){
        knockback.GetKnockBack(PlayerController.Instance.transform,15f);
    }
    [ContextMenu("Die")]
    public void Die(){
        //Оюбращение к контроллеру с сообщением о смерти
        Instantiate(DeathEffect,transform.position,Quaternion.identity);
        pool.ReturnEnemy(gameObject);
        ESC.OnEnemyDeath();
        
    }
    public string GetEnemyType(){
        return Type;
    }
    public void SetEnemyType(string type){
        Type = type;
    }
}
