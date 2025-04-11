using UnityEngine;

public class PistolBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D other){
        DamageRegTrigger(other);
    }
        
    public override void DamageRegTrigger(Collider2D collision){
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
        }


    }
}
