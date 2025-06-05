using System.Linq;
using UnityEngine;

public class AllWeaponsScript : MonoBehaviour
{
    [SerializeField] private AllWeapons weapons;
    [SerializeField] private GameObject weaponCardPrefab;

    void Awake()
    {
        DataManager.LoadUserProfile();
        foreach (var weapon in weapons.WeaponsList)
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
}
