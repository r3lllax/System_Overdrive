using System.Collections.Generic;
using UnityEngine;

public class ColdWeapon : MonoBehaviour
{
    [SerializeField]private Weapon currentWeapon;
    private Animator anim;
    private List<Collider2D> CollideEnemy;
    [SerializeField]private int Damage;
    [SerializeField]private float AnimationSpeed;
    public void AttackStart(){
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void SetCurrentWeapon(Weapon weapon){
        this.currentWeapon = weapon;
    }
    private void Update()
    {
        anim.speed = AnimationSpeed;
    }
    public void AttackEnd(){
        anim.SetBool("Attack",false);
        CollideEnemy.Clear();
        WeaponFollow.AnimEnd = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        CollideEnemy =  new List<Collider2D>();
        Damage = currentWeapon.Damage;
        AnimationSpeed = currentWeapon.AnimationSpeed;
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 && anim.GetBool("Attack")){
            if(!CollideEnemy.Contains(collision)){
                CollideEnemy.Add(collision);
                collision.GetComponent<Enemy>().TakeDamage(Damage);
            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 && anim.GetBool("Attack")){
            if(!CollideEnemy.Contains(collision)){
                CollideEnemy.Add(collision);
                collision.GetComponent<Enemy>().TakeDamage(Damage);
            }
        }
    }

}