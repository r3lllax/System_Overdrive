using UnityEngine;

public class CurrentWeapon : MonoBehaviour
{
    private GameObject ChoosenCharacter;
    private Weapon ChoosenWeapon;

    private void Awake(){
        ChoosenWeapon = TempData.ChoosenWeapon;
        
        GameObject Weapon = Instantiate(ChoosenWeapon.WeaponPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Weapon.transform.SetParent(gameObject.transform, false); 
        transform.parent.GetComponent<PlayerController>().SetPlayerMSWithMultiplier(ChoosenWeapon.PlayerSpeedMultiplier);
    }


    public Weapon GetWeaponData(){
        return ChoosenWeapon;
    }
}
