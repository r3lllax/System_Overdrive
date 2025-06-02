using Unity.Cinemachine;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject DeathEffect;
    public GameObject DamageEffect;
    [SerializeField] private string Type;
    [SerializeField] private int health;
    public float speed = 2f;
    public EnemySpawnController ESC;
    public EnemyPool pool;
    private Knockback knockback;
    private Flash flash;
    private GameObject Player;
    private float lowExpThreshold;
    private float highExpThreshold;
    private bool InCameraView;
    private LightningController lightningController;
    private SpriteRenderer sp;

    public void SetInCameraView(bool New){
        InCameraView = New;
    }
    public bool GetInCameraView(){
        return InCameraView;
    }

    public void SetLowExpTh(float Num){
        lowExpThreshold = Num;
    }
    public void SetHighExpTh(float Num){
        highExpThreshold = Num;
    }

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
        sp = GetComponent<SpriteRenderer>();
        lightningController = GameObject.FindWithTag("Player").GetComponentInChildren<LightningController>();
        Player = lightningController.transform.parent.gameObject;
    }
    public void SetHealth(int HP){
        health = HP;
    }
    public int GetHealth()
    {
        return health;
    }
    
    public void CheckDeath(){
        if(health<=0){
            knockback.Refresh();
            flash.Refresh();
            Die();
        }
    }
    public void TakeDamage(int Damage, float strength, string modifier = "default",bool tryLightning = false) {
        health = health - Damage <= 0 ? 0 : health -= Damage;
        if (tryLightning)
        {
            if (lightningController)
            {
                lightningController.TryProcLightning(this.gameObject,(float)Damage);

            }

        }
        GetComponent<CinemachineImpulseSource>().GenerateImpulse(1);
        DamageUI.Instance.AddText(Damage, transform.position, modifier);
        flash.StartCoroutine(flash.FlashRoutine());
        if (Type != "Boss")
        {
            knockback.GetKnockBack(PlayerController.Instance.transform,strength);
        }
        Instantiate(DamageEffect,transform.position,Quaternion.identity);
    }
    public void OneShot(float strength){
        health = 0;
        DamageUI.Instance.AddText(1,transform.position,"oneshoot");
        flash.StartCoroutine(flash.FlashRoutine());
        knockback.GetKnockBack(PlayerController.Instance.transform,strength);
        Instantiate(DamageEffect,transform.position,Quaternion.identity);
    }


    public void KnockBackWithoutDamage(){
        knockback.GetKnockBack(PlayerController.Instance.transform,15f);
    }
    [ContextMenu("Die")]
    public void Die(){
        //Добавить настройку эффекта
        // Instantiate(DeathEffect,transform.position,Quaternion.identity);
        if (SessionData.CanLifeSteal)
        {
            SessionData.AddValueFloat(ref SessionData.LifeStealCurrentValue, SessionData.LifeStealStrength);
            if (SessionData.LifeStealCurrentValue >= 1)
            {
                SessionData.LifeStealCurrentValue = 0;
                SessionData.AddValueInt(ref SessionData.Health, 1);
                
            }
        }
        DropEXP();
        pool.ReturnEnemy(gameObject);
        ESC.OnEnemyDeath();
        
    }
    public void DropEXP(){
        GameObject Exp = ExpPool.Instance.GetExp();
        Exp.GetComponent<Exp>().SetExpCount(Random.Range(lowExpThreshold,highExpThreshold));
        Vector2 pos = transform.position;
        pos.y += 0.1f;
        Exp.transform.position = pos;
        Exp.transform.rotation = transform.rotation;
    }
    public void TryDropExp(){
        if(Random.Range(0,10)>=3){
            LevelSystem.Instance.AddCurrentExp(Random.Range(lowExpThreshold,highExpThreshold));
        }
    }
    public string GetEnemyType(){
        return Type;
    }
    public void SetEnemyType(string type){
        Type = type;
    }
}
