using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]protected int MaxBypassesCount = 0;
    [SerializeField]protected int BypassesCount;
    [SerializeField]protected int MaxBounceCount;
    [SerializeField]protected int BounceCount;
    protected int damage = 0;
    protected float BulletLifeTIme;

    public void SetDamage(int Num){
        damage = Num;
    }
    public void SetBulletLifeTime(float Num){
        BulletLifeTIme = Num;
    }
    public void SetBypassCount(int num){
        MaxBypassesCount = num;
    }
    public void SetBounceCount(int num){
        MaxBounceCount = num;
    }
    protected bool TryOneShot(){
        var Rand = Random.Range(0,SessionData.ProcenteScaleMax+1);
        Debug.Log($"{Rand},{SessionData.OneShootChance}");
        if(Random.Range(0,SessionData.ProcenteScaleMax+1)<=SessionData.OneShootChance){
            Debug.Log("ONESHOOT");
            return true;
        }
        else return false;
    }

    protected void RefreshSize(){
        gameObject.transform.localScale = SessionData.BulletSize;
    }

    public virtual void DamageRegCollision(Collision2D collision){
        
    }
    public virtual void DamageRegTrigger(Collider2D collision){
        
    }
    public virtual int GetBulletBypassCount(){
        return MaxBypassesCount;
    }
    public virtual void IncreaseBypassCount(int Num){
    }
    public virtual void DecreaseBypassCount(int Num){
    }

    public IEnumerator ReturnBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        BulletPool.Instance.ReturnBullet(bullet);
    }
}
