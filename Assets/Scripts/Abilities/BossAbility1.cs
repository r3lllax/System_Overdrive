using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbility1 : Ability
{
    private float agrRadius = 20;
    private float ExplosionsCount = 30;
    private List<Vector2> Points;
    [SerializeField] private GameObject ExplosionPrefab;
    
    void Awake()
    {
        Points = new List<Vector2>();
        PlayerIsOwner = false;
    }
    private Vector2 GenerateAbilityPoint()
    {
        Vector2 localPoint = Random.insideUnitCircle * agrRadius;
        Vector3 globalPoint = transform.position + new Vector3(localPoint.x, localPoint.y, 0);
        return globalPoint;
    }
    protected override void ExecuteAbility()
    {
        Points.Clear();
        for (int i = 0; i < ExplosionsCount; i++)
        {
            Points.Add(GenerateAbilityPoint());
        }
        
        StartCoroutine(SpawnExplosions());
        
    }
    private IEnumerator SpawnExplosions()
    {
        foreach (Vector2 point in Points)
        {
            
            var BE = Instantiate(ExplosionPrefab, point, Quaternion.identity);
            BE.GetComponent<BossExplosion>().SetOnwer(owner);
            yield return new WaitForSeconds(activeTime / Points.Count);
        }
    }

}
