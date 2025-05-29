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
        StartCoroutine(ActiveAbility());
    }

    private IEnumerator ActiveAbility()
    {
        if (!ShieldObject)
        {
            Debug.Log($"{owner.tag} - OWTAG");
            ShieldObject = Instantiate(shieldPrefab, owner.transform);
            for (int i = 0; i < ShieldObject.transform.childCount; i++)
            {
                ShieldObject.transform.GetChild(i).GetComponent<ShieldBullet>().SetOnwer(owner);
            }
        }
        ShieldObject.SetActive(true);
        yield return new WaitForSeconds(activeTime);
        ShieldObject.SetActive(false);
    }

    void Update()
    {

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
