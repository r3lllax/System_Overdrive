using System.Collections.Generic;
using UnityEngine;

public class KnockBackWPlayerDamage : MonoBehaviour
{
    private float radius = 4.63f;
    [SerializeField] private LayerMask enemyLayer;


    public void KnockBackClosestEnemy(float strength){
        Collider2D[] CollideEnemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        foreach (Collider2D enemy in CollideEnemies)
        {

            enemy.GetComponent<Knockback>().GetKnockBack(PlayerController.Instance.transform, strength);
        }
            
            
    }
    
}
