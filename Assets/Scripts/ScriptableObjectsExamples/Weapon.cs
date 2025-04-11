using UnityEngine;

[CreateAssetMenu(fileName = "CurrentWeapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{
    public string WeaponName;
    public float distanceFromPlayer;
    public float AnimationSpeed;
    public float GunAttackSpeed;
    public float GunMagazineSize;
    public float GunBulletSpeed;
    public float GunMagazineReloadTime;
    public float PlayerSpeedMultiplier;
    public int Damage;

    public GameObject WeaponPrefab;
    public GameObject BulletPrefab;
    public GameObject AttackParticles;   
}
