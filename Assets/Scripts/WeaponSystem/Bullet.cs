using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 0;


    public void SetDamage(int Num){
        damage = Num;
    }


    public virtual void DamageRegCollision(Collision2D collision){
        // if (collision.gameObject.layer == 7)
        // {
        //     collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
        //     BulletPool.Instance.ReturnBullet(gameObject);
        // }
        // else{
        //     BulletPool.Instance.ReturnBullet(gameObject);
        // }
    }
    public virtual void DamageRegTrigger(Collider2D collision){
        // if (collision.gameObject.layer == 7)
        // {
        //     collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
        //     BulletPool.Instance.ReturnBullet(gameObject);
        // }
        // else{
        //     BulletPool.Instance.ReturnBullet(gameObject);
        // }
    }
}
