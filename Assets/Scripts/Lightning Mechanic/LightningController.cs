using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    private float procChance = 0.25f;
    private float startDamage = 1f;
    private int maxJumps = 3;
    private float jumpRadius = 5f;
    private float damageMultiplier = 0.7f;
    private float delay = 0.15f;
    public LayerMask enemyLayer;

    public void TryProcLightning(GameObject firstTarget)
    {
        startDamage = SessionData.Damage;
        if (Random.value <= procChance)
        {
            StartCoroutine(TriggerLightningChain(firstTarget));

        }
    }

    private IEnumerator TriggerLightningChain(GameObject firstTarget)
    {
        List<GameObject> affectedTargets = new List<GameObject>();
        GameObject currentTarget = firstTarget;
        float currentDamage = startDamage;
        int jumpsLeft = maxJumps;

        while (jumpsLeft > 0 && currentTarget != null)
        {
            Enemy targetEnemy = currentTarget.GetComponent<Enemy>();
            
            if (targetEnemy != null)
            {
            
                targetEnemy.TakeDamage((int)currentDamage, 0f);
                affectedTargets.Add(currentTarget);
                
                
            }
            if (jumpsLeft != maxJumps)
            {

                if (affectedTargets.Count > 0)
                {
                    CreateLightningEffect(affectedTargets[affectedTargets.Count - 2 < 0 ? 0 : affectedTargets.Count - 2].transform.position, currentTarget.transform.position);
                }
                
            }
            


            GameObject nextTarget = FindNextTarget(currentTarget.transform.position, affectedTargets);

            if (!nextTarget) { break; }

            currentTarget = nextTarget;
            currentDamage *= damageMultiplier;
            jumpsLeft--;
            yield return new WaitForSeconds(delay);
        }
        
    }
    private GameObject FindNextTarget(Vector3 origin, List<GameObject> exclude)
    {
        Collider2D[] near = Physics2D.OverlapCircleAll(origin, jumpRadius, enemyLayer);
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        foreach (Collider2D col in near)
        {
            if (exclude.Contains(col.gameObject)) { continue; }
            float distance = Vector3.Distance(origin, col.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = col.gameObject;
            }
        }
        return closest;
    }
    private void CreateLightningEffect(Vector3 start, Vector3 end)
    {
        LightningVFX.Create(start, end);
    }

}
