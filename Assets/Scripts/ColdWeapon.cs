using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class ColdWeapon : MonoBehaviour
{
    private Animator anim;
    private List<Collider2D> CollideEnemy;
    [SerializeField]private int Damage;
    [SerializeField]private float AnimationSpeed;
    public void AttackStart(){
        GetComponent<CinemachineImpulseSource>().GenerateImpulse(1);
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void UpdateData(){
        Damage = SessionData.Damage;
        AnimationSpeed = SessionData.AttackSpeedMelee;
        Debug.Log(gameObject.transform.localScale);
        anim.speed = AnimationSpeed;
    }
    private void Update()
    {   
        
        if(SessionData.NeedRefresh){
            Debug.Log($"TEST = SessionData.NeedRefresh:{SessionData.NeedRefresh}");
            UpdateData();
            
            SessionData.ShowData();
        }

    }
    public void AttackEnd(){
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
        var Rand = Random.Range(0,SessionData.ProcenteScaleMax+1);
        Debug.Log($"{Rand},{SessionData.OneShootChance}");
        if(Random.Range(0,SessionData.ProcenteScaleMax+1)<=SessionData.OneShootChance){
            Debug.Log("ONESHOOT");
            return true;
        }
        else return false;
    }

    private bool TryCrit(){
        var Rand = Random.Range(0,SessionData.ProcenteScaleMax+1);
        Debug.Log($"{Rand},{SessionData.CritChance}");
        if(Random.Range(0,SessionData.ProcenteScaleMax+1)<=SessionData.CritChance){
            Debug.Log("Crit");
            return true;
        }
        else return false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 && anim.GetBool("Attack")){
            if(!CollideEnemy.Contains(collision)){
                CollideEnemy.Add(collision);
                if(TryOneShot()==true){
                    collision.GetComponent<Enemy>().OneShot(20f);
                }
                else if(TryCrit()){
                    collision.GetComponent<Enemy>().TakeDamage(Damage*2, 15f,"crit",tryLightning:true);
                }
                else{
                    collision.GetComponent<Enemy>().TakeDamage(Damage, 15f,tryLightning:true);
                }
            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 && anim.GetBool("Attack")){
            if(!CollideEnemy.Contains(collision)){
                CollideEnemy.Add(collision);
                if(TryOneShot()==true){
                    collision.GetComponent<Enemy>().OneShot(20f);
                }
                else if(TryCrit()){
                    collision.GetComponent<Enemy>().TakeDamage(Damage*2, 15f,"crit",tryLightning:true);
                }
                else{
                    collision.GetComponent<Enemy>().TakeDamage(Damage, 15f,tryLightning:true);
                }
            }
        }
    }

}