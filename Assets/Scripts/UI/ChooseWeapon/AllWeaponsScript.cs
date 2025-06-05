using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllWeaponsScript : MonoBehaviour
{
    [SerializeField] private AllWeapons AllWeapons;
    [SerializeField] private GameObject weaponCardPrefab;

    void Awake()
    {
        DataManager.LoadUserProfile();
        LoadWeapons();
    }
    public void LoadWeapons()
    {
        for (int i =0;i<transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        foreach (var weapon in AllWeapons.WeaponsList)
        {
            GameObject card = Instantiate(weaponCardPrefab, transform);
            if (DataManager.CurrentUser.UnlockedWeapon.Contains(weapon.name))
            {
                card.GetComponent<WeaponCardInGrid>().isLocked = false;
            }
            else
            {
                card.GetComponent<WeaponCardInGrid>().isLocked = true;
            }
            card.GetComponent<WeaponCardInGrid>().weapon = weapon;
        }
    }
    void Update()
    {
        if (TempData.needRefreshData)
        {
            LoadWeapons();
        }
    }
    void LateUpdate()
    {
        TempData.needRefreshData = false;
    }
}
