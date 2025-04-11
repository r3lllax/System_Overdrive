using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 0;


    public void SetDamage(int Num){
        damage = Num;
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
            BulletPool.Instance.ReturnBullet(gameObject);
        }
        else{
            BulletPool.Instance.ReturnBullet(gameObject);
        }
        
    }

}
