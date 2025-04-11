using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected int damage = 0;
    protected float BulletLifeTIme;

    public void SetDamage(int Num){
        damage = Num;
    }
    public void SetBulletLifeTime(float Num){
        BulletLifeTIme = Num;
    }


    public virtual void DamageRegCollision(Collision2D collision){
        
    }
    public virtual void DamageRegTrigger(Collider2D collision){
        
    }

    public IEnumerator ReturnBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        BulletPool.Instance.ReturnBullet(bullet);
    }
}
