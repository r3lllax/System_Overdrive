using UnityEngine;

public class CurrentWeapon : MonoBehaviour
{
    private GameObject ChoosenCharacter;
    private GameObject ChoosenWeapon;
    private Weapon WeaponData;

    

    private void Start()
    {
        ChoosenWeapon = TempData.ChoosenWeapon;
        GameObject Weapon = Instantiate(ChoosenWeapon, new Vector3(0, 0, 0), Quaternion.identity);
        Weapon.transform.SetParent(gameObject.transform, false);
        WeaponData = ChoosenWeapon.GetComponent<WeaponFollow>().currentWeapon;
        transform.parent.GetComponent<PlayerController>().SetPlayerMSWithMultiplier(WeaponData.PlayerSpeedMultiplier);

    }
    public Weapon GetWeaponData(){
        return WeaponData;
    }
}
