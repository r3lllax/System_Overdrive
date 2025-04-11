using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Обращаемся к классу TEMPDATA и в переменную SpeedMultiply получаем множитель скорости с оружием
    private Weapon Data;
    [SerializeField] private GameObject DamagePrefab; // Также брать из СО
    [SerializeField]private float Health; // Получаем с префаба или СО
    private float MoveSpeed; //Точно также
    private float PlayerSpeedMultiplier = 1f;

    private void Awake()
    {
        //Пока что
        Health = 4;
        MoveSpeed = 4;
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
