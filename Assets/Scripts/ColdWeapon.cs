using System.Collections.Generic;
using UnityEngine;

public class ColdWeapon : MonoBehaviour
{
    private Animator anim;
    private List<Collider2D> CollideEnemy;
    [SerializeField]private int Damage;
    public void AttackStart(){
        transform.GetChild(0).gameObject.SetActive(true);
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
        Damage = 2;
        
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