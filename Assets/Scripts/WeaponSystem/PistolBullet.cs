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
        try
        {
            StopAllCoroutines();
        }
        catch (Exception)
        {
            Debug.Log("ошибка в при стопе куротины");
            
        }
    }    
    public override void DamageRegTrigger(Collider2D collision){
        if (collision.gameObject.layer == targetLayerNum && collision.gameObject.activeSelf==true)
        {
            Debug.Log(SessionData.BulletBypassCount);
            if (TryOneShot() == true && collision.gameObject.GetComponent<Enemy>().GetEnemyType() != "Boss")
            {
                collision.gameObject.GetComponent<Enemy>().OneShot(10f);
            }
            else if (TryCrit())
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage((int)(damage * SessionData.CritScale), 5f, "crit", true);
            }
            else
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, 5f, tryLightning: true);
            }
            if (BypassesCount > 0)
            {
                BypassesCount--;
            }
            else
            {
                StopAllCoroutines();
                BulletPool.Instance.ReturnBullet(gameObject);
                
            }
                
        
        }


    }
}
