using UnityEngine;

[CreateAssetMenu(fileName = "CurrentWeapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{
    public string WeaponName;
    public float distanceFromPlayer;
    public float AnimationSpeed;
    public float PlayerSpeedMultiplier;
    public int Damage;

    public GameObject WeaponPrefab;
    public ParticleSystem AttackParticles;   
}
