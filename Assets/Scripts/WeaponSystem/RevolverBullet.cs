using UnityEngine;

public class RevolverBullet : Bullet
{
    
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
