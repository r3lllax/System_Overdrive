using UnityEngine;

[CreateAssetMenu(fileName = "CurrentWeapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{
    public string WeaponName;
    public float distanceFromPlayer;
    public float AnimationSpeed;
    public float GunAttackSpeed;
    public int GunMagazineSize;
    public float GunBulletSpeed;
    public float GunBulletLifeTime;
    public float GunMagazineReloadTime;
    public float PlayerSpeedMultiplier;
    public int Damage;
    public int Price;

    public GameObject WeaponPrefab;
    public GameObject BulletPrefab;
    public GameObject AttackParticles;   
}
