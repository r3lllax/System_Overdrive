using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string Name;
    public Sprite Image;
    public string Description;
    public float minVal;
    public float maxVal;
    public bool Melee;
    public bool Range;
    public bool Procente;
    public bool HasDebuff;
    public enum Stats
    {
        Health,
        MoveSpeed,
        Damage,
        ExpFinderRadius,
        AttackSpeedMelee,
        CdBetweenFire,
        CdBetweenMagazine,
        StartSpeedMultiplier,
        SprintMultiplier,
        MagazineCapacity,
        OneShootChance,
        CritChance,
        MeleeSize,
        BulletSize,
        BulletSpeed,
        BulletLifeTime,
        BulletBypassCount,
        BulletRebonceCount,
        EnemySpeedMultiplier,
        AbilityActiveTime,
        AbilityCooldown,
        BackFire,
        LightningProcChance,
        LightningJumpRadius,
        LightningDamageMultiplier,
        LightningDelay,
        LightningMaxJumps,
        offersCount,
        CritScale,
        CanDestroyEnemyBullet,
        LifeStealStrength,
        LifeStealCurrentValue,
        CanLifeSteal,
        DamageEvadeChance
        

    }
    public Stats targerStat = Stats.Health;
    public Stats debuffStat = Stats.Health; 
    public float DebufSize;
   
}
