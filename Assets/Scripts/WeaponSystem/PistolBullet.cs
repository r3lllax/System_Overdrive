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
        if (collision.gameObject.layer == 7)
        {
            if(BypassesCount>0){
                if(TryOneShot()==true){
                    collision.gameObject.GetComponent<Enemy>().OneShot(10f);
                }
                else{
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
                }
                BypassesCount--;
            }
            else{
                if(TryOneShot()==true){
                    collision.gameObject.GetComponent<Enemy>().OneShot(10f);
                }
                else{
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
                }
                BulletPool.Instance.ReturnBullet(gameObject);
            }
        }


    }
}
