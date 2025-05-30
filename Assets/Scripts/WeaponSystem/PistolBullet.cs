using System;
using UnityEngine;

public class PistolBullet : Bullet
{
    private void Start()
    {
        
        MaxBypassesCount = SessionData.BulletBypassCount;
        BypassesCount = MaxBypassesCount;
    }
    private void Update()
    {
        RefreshSize();
    }
    private void OnTriggerEnter2D(Collider2D other){
        DamageRegTrigger(other);
    }

    private void OnEnable()
    {

    }
    public override void IncreaseBypassCount(int Num){
        MaxBypassesCount+=Num;
    }
    public override void DecreaseBypassCount(int Num){
        MaxBypassesCount-=Num;
    }

    private void OnDisable()
    {
        try{
            StopAllCoroutines();
        }
        catch(Exception){
            Debug.Log("ошибка в при стопе куротины");
        }
    }    
    public override void DamageRegTrigger(Collider2D collision){
        if (collision.gameObject.layer == targetLayerNum)
        {
            if(BypassesCount>0){
                if(TryOneShot()==true && collision.gameObject.GetComponent<Enemy>().GetEnemyType()!="Boss"){
                    collision.gameObject.GetComponent<Enemy>().OneShot(10f);
                }
                else if(TryCrit()){
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage*2,5f,"crit",true);
                }
                else{
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f,tryLightning:true);
                }
                BypassesCount--;
            }
            else{
                if(TryOneShot()==true && collision.gameObject.GetComponent<Enemy>().GetEnemyType()!="Boss"){
                    collision.gameObject.GetComponent<Enemy>().OneShot(10f);
                }
                else if(TryCrit()){
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage*2,5f,"crit",tryLightning:true);
                }
                else{
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f,tryLightning:true);
                }
                BulletPool.Instance.ReturnBullet(gameObject);
            }
        }


    }
}
