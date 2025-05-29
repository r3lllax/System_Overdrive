using System.Collections;
using Unity.Cinemachine;
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

    private void SetPlayerMSWithMultiplier(float speed){
        this.MoveSpeed *= speed;
    }

    private void UpdateData(){
        
        Health = SessionData.Health;
        sprintMultiplier = SessionData.SprintMultiplier;
        MoveSpeed = SessionData.MoveSpeed;
        MoveSpeed = MoveSpeed<0?0:MoveSpeed;
        PlayerSpeedMultiplier = SessionData.StartSpeedMultiplier;
        SetPlayerMSWithMultiplier(PlayerSpeedMultiplier);
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
        //GetComponent<PlayerController>().SetPlayerMSWithMultiplier(PlayerSpeedMultiplier);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            
            TakeDamage(1);
            
        }
    }

    public void TakeDamage(float Damage, bool knock = true){
        Health = Health-Damage<0?0:Health-Damage;
        GetComponent<CinemachineImpulseSource>().GenerateImpulse(1);
        Instantiate(DamagePrefab,transform.position,Quaternion.identity);
        StartCoroutine(DamageRoutine());
        if (knock)
        {
            try
            {
                transform.GetChild(transform.childCount - 1).GetComponent<KnockBackWPlayerDamage>().KnockBackClosestEnemy();
            }
            catch
            {
                transform.GetChild(transform.childCount-2).GetComponent<KnockBackWPlayerDamage>().KnockBackClosestEnemy();
            }

        }
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
