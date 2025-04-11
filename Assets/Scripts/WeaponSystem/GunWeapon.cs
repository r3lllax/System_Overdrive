using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GunWeapon : MonoBehaviour
{
    private Weapon CurrentWeapon;
    [SerializeField] private float MagazineSize;
    [SerializeField] private float FireSpeed;
    [SerializeField] private float BulletSpeed;
    [SerializeField] private float MagazineReloadTime;
    [SerializeField] private int Damage;
    private Timer timer;
    private float CurrentMagazineSize;
    public float ReloadTime;

    private void Awake()
    {
        CurrentWeapon = TempData.ChoosenWeapon;
        MagazineSize = CurrentWeapon.GunMagazineSize;
        FireSpeed = CurrentWeapon.GunAttackSpeed;
        MagazineReloadTime = CurrentWeapon.GunMagazineReloadTime;
        BulletSpeed = CurrentWeapon.GunBulletSpeed;
        Damage = CurrentWeapon.Damage;
        CurrentMagazineSize = MagazineSize;
        
    }
    public void AttackStart(){
        var FireEffect = Instantiate(CurrentWeapon.AttackParticles,CurrentWeapon.AttackParticles.transform.position,CurrentWeapon.AttackParticles.transform.rotation);
        FireEffect.SetActive(true);
        FireEffect.transform.SetParent(transform,false);
    }
    public void AttackEnd(){
        
    }
    public void TryFire(){
        if(CurrentMagazineSize>0){
            Fire();
        }

    }

    private void Fire(){
        CurrentMagazineSize-=1;

        GameObject bullet = BulletPool.Instance.GetBullet();
        Debug.Log(Damage);
        bullet.GetComponent<Bullet>().SetDamage(Damage);
        Vector2 pos = transform.position;
        pos.y += 0.1f;
        bullet.transform.position = pos;
        bullet.transform.rotation = transform.rotation;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * BulletSpeed;

        StartCoroutine(ReturnBulletAfterTime(bullet, 5f));

        AttackStart();
        Reload();
    }
    IEnumerator ReturnBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        BulletPool.Instance.ReturnBullet(bullet);
    }
    private void Reload(){
        if(CurrentMagazineSize==0){
            ReloadTime = MagazineReloadTime;
            CurrentMagazineSize = MagazineSize;
        }
        else{
            ReloadTime = FireSpeed;
            
        }
        
    }

    private void Update()
    {
        if(ReloadTime>0){
            ReloadTime-=Time.deltaTime;
        }
        
    }

}
