using System;
using UnityEngine;

public class PistolBullet : Bullet
{
    private void OnEnable()
    {
        StartCoroutine(ReturnBulletAfterTime(gameObject, BulletLifeTIme));
    }
    private void OnTriggerEnter2D(Collider2D other){
        DamageRegTrigger(other);
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
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
        }


    }
}
