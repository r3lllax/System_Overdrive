using System;
using System.Collections;
using UnityEngine;

public class BossAbility2 : Ability
{
    [SerializeField] private GameObject shieldPrefab;
    private GameObject ShieldObject;


    void Awake()
    {
        PlayerIsOwner = false;
    }

    protected override void ExecuteAbility()
    {
        if (!ShieldObject)
        {
            ShieldObject = Instantiate(shieldPrefab, owner.transform);
            for (int i = 0; i < ShieldObject.transform.childCount; i++)
            {
                ShieldObject.transform.GetChild(i).GetComponent<ShieldBullet>().SetOnwer(owner);
            }
        }
        ShieldObject.SetActive(true);
        
    }

    protected override IEnumerator CooldownRoutine()
    {
        ShieldObject.SetActive(false);
        currentCooldown = cooldown;
        Reload = true;
        yield return new WaitForSeconds(cooldown);
        Reload = false;
        isReady = true;
    }

    

    void Update()
    {
        if (PlayerIsOwner)
        {
            cooldown = SessionData.AbilityCooldown;
            activeTime = SessionData.AbilityActiveTime;
        }
        if (Reload)
        {
            currentCooldown -= Time.deltaTime;
        }
        else
        {
            currentCooldown = 0f;
        }

        if (ShieldObject)
        {
            ShieldObject.transform.eulerAngles = new Vector3(ShieldObject.transform.eulerAngles.x, ShieldObject.transform.eulerAngles.y, ShieldObject.transform.eulerAngles.z + (Time.deltaTime * 60));

        }

    }
    void OnDisable()
    {
        try
        {
            ShieldObject.SetActive(false);
        }
        catch{}
    }
}
