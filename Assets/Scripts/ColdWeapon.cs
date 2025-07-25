using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class ColdWeapon : MonoBehaviour
{
    private Animator anim;
    private List<Collider2D> CollideEnemy;
    [SerializeField]private int Damage;
    [SerializeField]private float AnimationSpeed;
    [SerializeField]private AudioClip[] clips;
    private AudioSource As;
    public void AttackStart()
    {
        As.volume = DataManager.CurrentUser.Settings.EffectsVolume;
        if (anim.speed < 0.7)
        {
            As.clip = clips[0];
        }
        else
        {
            As.clip = clips[1];
        }
        As.Play();
        GetComponent<CinemachineImpulseSource>().GenerateImpulse(1);
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void UpdateData(){
        Damage = SessionData.Damage;
        AnimationSpeed = SessionData.AttackSpeedMelee;
        anim.speed = AnimationSpeed;
    }
    private void Update()
    {
        if (Time.timeScale <= 0)
        {
            As.Stop();
        }
        if(SessionData.NeedRefresh){
            UpdateData();
        }

    }
    public void AttackEnd(){
        As.Stop();
        anim.SetBool("Attack",false);
        anim.SetBool("SwingDown",!anim.GetBool("SwingDown"));
        anim.SetBool("SwingUp",!anim.GetBool("SwingUp"));
        CollideEnemy.Clear();
        WeaponFollow.AnimEnd = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    private void Awake()
    {
        UpgradesController.PlayerType = "Melee";
        As = GetComponent<AudioSource>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        CollideEnemy =  new List<Collider2D>();
        Damage = SessionData.Damage;
        AnimationSpeed = SessionData.AttackSpeedMelee;
        anim.speed = AnimationSpeed;

    }
    private bool TryOneShot(){
        if(Random.Range(0,SessionData.ProcenteScaleMax+1)<=SessionData.OneShootChance){
            return true;
        }
        else return false;
    }

    private bool TryCrit(){
        if(Random.Range(0,SessionData.ProcenteScaleMax+1)<=SessionData.CritChance){
            return true;
        }
        else return false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == 7 && anim.GetBool("Attack"))
        {
            if (!CollideEnemy.Contains(collision))
            {
                CollideEnemy.Add(collision);
                if (TryOneShot() == true)
                {
                    collision.GetComponent<Enemy>().OneShot(20f);
                }
                else if (TryCrit())
                {
                    collision.GetComponent<Enemy>().TakeDamage((int)((float)Damage * SessionData.CritScale), 15f, "crit", tryLightning: true);
                }
                else
                {
                    collision.GetComponent<Enemy>().TakeDamage(Damage, 15f, tryLightning: true);
                }
            }
        }
    }
    
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && anim.GetBool("Attack"))
        {
            if (!CollideEnemy.Contains(collision))
            {
                CollideEnemy.Add(collision);
                if (TryOneShot() == true)
                {
                    collision.GetComponent<Enemy>().OneShot(20f);
                }
                else if (TryCrit())
                {
                    collision.GetComponent<Enemy>().TakeDamage((int)((float)Damage * SessionData.CritScale), 15f, "crit", tryLightning: true);
                }
                else
                {
                    collision.GetComponent<Enemy>().TakeDamage(Damage, 15f, tryLightning: true);
                }
            }
        }
    }

}