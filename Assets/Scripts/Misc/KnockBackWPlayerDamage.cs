using System.Collections.Generic;
using UnityEngine;

public class KnockBackWPlayerDamage : MonoBehaviour
{
    private List<Collider2D> CollideEnemies;

    private void Awake()
    {
        CollideEnemies = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 7){
            if(!CollideEnemies.Contains(other)){
                CollideEnemies.Add(other);
                
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7){
            if(CollideEnemies.Contains(collision)){
                CollideEnemies.Remove(collision);
            }
        }
    }
    
    public void KnockBackClosestEnemy(){
        foreach(Collider2D enemy in CollideEnemies){
            enemy.GetComponent<Knockback>().GetKnockBack(PlayerController.Instance.transform,15f);
        }
    }
}
