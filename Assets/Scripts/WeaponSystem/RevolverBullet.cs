using System;
using UnityEngine;

public class RevolverBullet : Bullet
{   
    
    void Start()
    {
        MaxBounceCount = SessionData.BulletRebonceCount;
        BounceCount = MaxBounceCount;
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
    private void OnCollisionEnter2D(Collision2D other) {
        DamageRegCollision(other);

    }
    private void Update()
    {
        if(SessionData.NeedRefresh){
            RefreshSize();
            MaxBounceCount = SessionData.BulletRebonceCount;
            // SessionData.NeedRefresh = false;
        }
    }

    public override void DamageRegCollision(Collision2D collision){
        if (collision.gameObject.layer == targetLayerNum)
        {
            // collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
            if(BounceCount>0){
                if(TryOneShot()==true && collision.gameObject.GetComponent<Enemy>().GetEnemyType()!="Boss"){
                    collision.gameObject.GetComponent<Enemy>().OneShot(10f);
                }
                else if(TryCrit()){
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage*2,5f,"crit");
                }
                else{
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
                }
                BounceCount--;
            }
            else{
                if(TryOneShot()==true && collision.gameObject.GetComponent<Enemy>().GetEnemyType()!="Boss"){
                    collision.gameObject.GetComponent<Enemy>().OneShot(10f);
                }
                else if(TryCrit()){
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage*2,5f,"crit");
                }
                else{
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage,5f);
                }
                BulletPool.Instance.ReturnBullet(gameObject);
            }
        }

    }
}
