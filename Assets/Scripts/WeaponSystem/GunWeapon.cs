using System.Collections;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GunWeapon : MonoBehaviour
{
    private Weapon CurrentWeapon;
    [SerializeField] private float MagazineSize;
    [SerializeField] private float FireSpeed;
    [SerializeField] private float BulletSpeed;
    [SerializeField] private float BulletLifeTime;
    [SerializeField] private float MagazineReloadTime;
    [SerializeField] private int Damage;
    [SerializeField] private int BypassesCount;
    private bool unlimitedAmmos = false;
    private TextMeshProUGUI AmmoCountUI;
    private float CurrentMagazineSize;
    public float ReloadTime;
    private bool backFireProcessing = false;
    private Vector3 bulletPos;
    public void SetUnlimitedAmmo(bool newState)
    {
        unlimitedAmmos = newState;
    }
    private void UpdateData()
    {
        MagazineSize = SessionData.MagazineCapacity;
        FireSpeed = SessionData.CdBetweenFire;
        MagazineReloadTime = SessionData.CdBetweenMagazine;
        BulletSpeed = SessionData.BulletSpeed;
        Damage = SessionData.Damage;
        BulletLifeTime = SessionData.BulletLifeTime;
    }

    private void Awake()
    {
        UpgradesController.PlayerType = "Range";
        CurrentWeapon = TempData.ChoosenWeapon;
        MagazineSize = CurrentWeapon.GunMagazineSize;
        FireSpeed = CurrentWeapon.GunAttackSpeed;
        MagazineReloadTime = CurrentWeapon.GunMagazineReloadTime;
        BulletSpeed = CurrentWeapon.GunBulletSpeed;
        Damage = CurrentWeapon.Damage;
        BulletLifeTime = CurrentWeapon.GunBulletLifeTime;
        CurrentMagazineSize = MagazineSize;
        AmmoCountUI = GameObject.FindWithTag("AmmoCountUI").GetComponent<TextMeshProUGUI>();
        

    }
    public void ForcedReload()
    {
        if (CurrentMagazineSize == MagazineSize)
        {
            return;
        }
        ReloadTime = MagazineReloadTime;
        CurrentMagazineSize = MagazineSize;
        StartCoroutine(AnimReload());
    }
    public void AttackStart() {
        var FireEffect = Instantiate(CurrentWeapon.AttackParticles, CurrentWeapon.AttackParticles.transform.position, CurrentWeapon.AttackParticles.transform.rotation);
        FireEffect.SetActive(true);
        FireEffect.transform.SetParent(transform, false);
    }
    public void AttackEnd() {

    }
    public void TryFire() {
        if (CurrentMagazineSize > 0)
        {
            if (SessionData.BackFire > 0)
            {
                if (!backFireProcessing)
                {
                    StartCoroutine(BackAttack());
                }
                
            }
            else
            {
                Fire();
            }
            
        }

    }


    private IEnumerator BackAttack()
    {
        backFireProcessing = true;
        for (int i = 0; i < SessionData.BackFire + 1; i++)
        {
            if (i > 0)
            {
                yield return new WaitForSeconds(SessionData.CdBetweenFire / 2f / SessionData.BackFire);
            }
            if (!unlimitedAmmos)
            {
                if (i == 0)
                {
                    CurrentMagazineSize -= 1;
                    GetComponent<CinemachineImpulseSource>().GenerateImpulse(1);
                }
            }


            GameObject bullet = BulletPool.Instance.GetBullet();
            bullet.GetComponent<Bullet>().SetDamage(Damage);
            bullet.GetComponent<Bullet>().SetBulletLifeTime(BulletLifeTime);
            Vector3 pos = transform.position;
            pos.y += 0.1f;

            if (i >= 1)
            {
                Vector3 diff = PlayerController.Instance.transform.position - pos;
                pos += diff * 2;

            }
            bullet.transform.position = pos;
            bullet.transform.rotation = transform.rotation;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (i == 0)
            {
                rb.linearVelocity = transform.right * BulletSpeed;
            }
            else
            {
                rb.linearVelocity = -transform.right * BulletSpeed;
            }

            bullet.GetComponent<Bullet>().StartCoroutine(bullet.GetComponent<Bullet>().ReturnBulletAfterTime(bullet, BulletLifeTime));
            if (i == 0)
            {
                AttackStart();
            }

        }
        Reload();
        backFireProcessing = false;
    }

   
    private void Fire()
    {
        if (!unlimitedAmmos)
        {
            CurrentMagazineSize -= 1;
        }

        GetComponent<CinemachineImpulseSource>().GenerateImpulse(1);

        GameObject bullet = BulletPool.Instance.GetBullet();
        bullet.GetComponent<Bullet>().SetDamage(Damage);
        bullet.GetComponent<Bullet>().SetBulletLifeTime(BulletLifeTime);
        Vector2 pos = transform.position;
        pos.y += 0.1f;
        bullet.transform.position = pos;
        bullet.transform.rotation = transform.rotation;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * BulletSpeed;
        //Перенос куротины в пулю был для того, чтобы при удалении оружия из слота, куртоина продолжала отчет до конца жизни пули, в то время как 
        //Куротина находилась тут, при удалении в момент когда есть активные пули, они оставались бескончено, так как объект с куротиной удалялся
        bullet.GetComponent<Bullet>().StartCoroutine(bullet.GetComponent<Bullet>().ReturnBulletAfterTime(bullet, BulletLifeTime));

        AttackStart();
        Reload();
    }
    
    private void Reload(){
        if(CurrentMagazineSize==0){
            ReloadTime = MagazineReloadTime;
            CurrentMagazineSize = MagazineSize;
            StartCoroutine(AnimReload());
        }
        else{
            ReloadTime = FireSpeed;
            
        }
        
    }

    private IEnumerator AnimReload(){
        if(ReloadTime<1){
            GetComponent<Animator>().speed +=1/ReloadTime;
        }
        GetComponent<Animator>().SetBool("Reload",true);
        yield return new WaitForSeconds(ReloadTime);
        GetComponent<Animator>().SetBool("Reload",false);
        GetComponent<Animator>().speed =1;
    }

    private void DrawAmmos()
    {
        string CMST = "";
        string MST = "";
        if (CurrentMagazineSize.ToString().Length <= 1) {
            CMST = $"0{CurrentMagazineSize}";
        }
        else {
            CMST = CurrentMagazineSize.ToString();
        }
        if (MagazineSize.ToString().Length <= 1) {
            MST = $"0{MagazineSize}";
        }
        else {
            MST = MagazineSize.ToString();
        }

        if (AmmoCountUI)
        {
            if (unlimitedAmmos)
            {
                AmmoCountUI.text = "Бесконечные";
            }
            else if (ReloadTime > 0 && (CurrentMagazineSize == 0 || CurrentMagazineSize == MagazineSize))
            {
                AmmoCountUI.text = "Перезарядка";
            }
            else
            {
                AmmoCountUI.text = $"{CMST}/{MST}";
            }
            
        }
    }

    private void Update()
    {
        DrawAmmos();
        if (SessionData.NeedRefresh)
        {
            UpdateData();
            // SessionData.NeedRefresh=false;
        }
        if (ReloadTime > 0)
        {

            ReloadTime -= Time.deltaTime;
        }
        

    }

}
