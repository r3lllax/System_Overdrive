using UnityEngine;

public class WeaponCardUI : MonoBehaviour
{
    [SerializeField] private GameObject WeaponPrefab;


    public void SetChosenWeapon(){
        TempData.ChoosenWeapon = WeaponPrefab;
    }
}
