using System.Collections.Generic;
using UnityEngine;

public class KnockBackWPlayerDamage : MonoBehaviour
{
    private List<Collider2D> CollideEnemies;
    private float radius = 4.63f;
    [SerializeField] private LayerMask enemyLayer;


    private void Awake()
    {
        CollideEnemies = new List<Collider2D>();
    }


    public void KnockBackClosestEnemy(float strength){
        Collider2D[] CollideEnemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        foreach (Collider2D enemy in CollideEnemies)
        {

            enemy.GetComponent<Knockback>().GetKnockBack(PlayerController.Instance.transform, strength);
        }
            
            
    }
    
}
