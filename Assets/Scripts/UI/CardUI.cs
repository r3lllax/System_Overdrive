using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Weapon WeaponData;
    [SerializeField] private GameCharacter character;


    public void SetChosenWeaponAndDefaultData(){
        TempData.ChoosenWeapon = WeaponData;
        SessionData.Damage = WeaponData.Damage;
        SessionData.AttackSpeedMelee = WeaponData.AnimationSpeed;
        SessionData.CdBetweenFire = WeaponData.GunAttackSpeed;
        SessionData.CdBetweenMagazine = WeaponData.GunMagazineReloadTime;
        SessionData.SprintMultiplier = 1.5f;
        SessionData.StartSpeedMultiplier = WeaponData.PlayerSpeedMultiplier;
        SessionData.MagazineCapacity = WeaponData.GunMagazineSize;
        SessionData.OneShootChance = 0;
        SessionData.CritChance = 0;
        SessionData.BulletSpeed = WeaponData.GunBulletSpeed;
        SessionData.BulletLifeTime = WeaponData.GunBulletLifeTime;
        SessionData.MeleeSize = WeaponData.WeaponPrefab.transform.localScale;
        SessionData.BulletSize = new Vector3(1,1,1);
    }
    public void SetChosenCharacter(){
        TempData.ChoosenCharacter = character;
    }
}
