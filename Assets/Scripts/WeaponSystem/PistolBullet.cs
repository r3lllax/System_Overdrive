using System;
using UnityEngine;

public class PistolBullet : Bullet
{
    private int MaxBypassesCount = 1;
    private int BypassesCount;
    private void OnTriggerEnter2D(Collider2D other){
        DamageRegTrigger(other);
    }

    private void OnEnable()
    {
        BypassesCount = MaxBypassesCount;
    }
    public void IncreaseBypassCount(int Num){
        BypassesCount+=Num;
    }
    public void DecreaseBypassCount(int Num){
        BypassesCount-=Num;
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
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
                BypassesCount--;
            }
            else{
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
                BulletPool.Instance.ReturnBullet(gameObject);
            }
        }


    }
}
