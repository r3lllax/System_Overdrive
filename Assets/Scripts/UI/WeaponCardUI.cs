using UnityEngine;

public class WeaponCardUI : MonoBehaviour
{
    [SerializeField] private Weapon WeaponData;


    public void SetChosenWeapon(){
        TempData.ChoosenWeapon = WeaponData;
    }
}
