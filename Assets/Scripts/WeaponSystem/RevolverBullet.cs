using System;
using UnityEngine;

public class RevolverBullet : Bullet
{

    private void OnDisable()
    {
        try{
            StopAllCoroutines();
        }
        catch(Exception){
            Debug.Log("ошибка в при стопе куротины");
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        DamageRegCollision(other);

    }
    

    public override void DamageRegCollision(Collision2D collision){
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
        }

    }
}
