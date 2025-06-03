using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malevolent_Shrine : Ability
{
    private Vector3 start;
    private Vector3 end;
    void Awake()
    {
        start = PlayerController.Instance.transform.position;

    }
    protected override void ExecuteAbility()
    {
        StartCoroutine(Slashes());
    }
    private Vector2 GenerateAbilityPoint()
    {
        Vector2 localPoint = Random.insideUnitCircle * 50;
        Vector3 globalPoint = transform.position + new Vector3(localPoint.x, localPoint.y, 0);
        return globalPoint;
    }
    private IEnumerator Slashes()
    {
        
        while (ActiveNow)
        {
            start = GenerateAbilityPoint();
            Vector3 diff = transform.position - start;
            end = start + diff * 2;
            start += new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
            end += new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
            CreateLightningEffect(start, end);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void CreateLightningEffect(Vector3 start, Vector3 end)
    {
        LightningVFX.Create(start, end, true);
    }

    
}
