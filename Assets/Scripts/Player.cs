using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Обращаемся к классу TEMPDATA и в переменную SpeedMultiply получаем множитель скорости с оружием
    private Weapon Data;
    [SerializeField] private GameObject DamagePrefab;
    [SerializeField]private float Health;
    private float MoveSpeed;
    private float sprintMultiplier;
    private float PlayerSpeedMultiplier;

    private void UpdateData(){
        
        Health = SessionData.Health;
        sprintMultiplier = SessionData.SprintMultiplier;
        MoveSpeed = SessionData.MoveSpeed;
        PlayerSpeedMultiplier = SessionData.StartSpeedMultiplier;
    }
    public float GetSprintMultiplier(){
        return sprintMultiplier;
    }
    private void Awake()
    {
        UpdateData();
    }
    private void Update()
    {
            UpdateData();
            // SessionData.NeedRefresh = false;
        
        
    }
    private void Start()
    {
        GetComponent<PlayerController>().SetPlayerMSWithMultiplier(PlayerSpeedMultiplier);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7){
            TakeDamage(1);
        }
    }

    public void TakeDamage(float Damage){
        Health = Health-Damage<0?0:Health-Damage;
        Instantiate(DamagePrefab,transform.position,Quaternion.identity);
        StartCoroutine(DamageRoutine());
        transform.GetChild(transform.childCount-1).GetComponent<KnockBackWPlayerDamage>().KnockBackClosestEnemy();
        if(CheckDeath()){
            Death();
        }
    }
    private IEnumerator DamageRoutine(){
        GetComponent<SpriteRenderer>().color = new Color32(255,0,0,255);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
    }
    private bool CheckDeath(){
        return Health>0?false:true;
    }
    public void Death(){
        ///Парткиклы, выключение управления, скрипт запуска экрана результата
    }
    public float GetMoveSpeed(){
        return MoveSpeed;
    }
    public void SetMoveSpeed(float MS){
        this.MoveSpeed = MS;
    }
}
