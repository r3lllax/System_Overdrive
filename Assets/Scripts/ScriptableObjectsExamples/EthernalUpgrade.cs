using UnityEngine;

public class EthernalUpgrade
{
    public string Title = "Название";
    public string Description = "Увеличивает";
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
        DamageEvadeChance,
        ChooseUpgradesCount,
        ExpMultiplier

    }
    public Stats targetStat = Stats.Health;
    public int Count = 0;
    public bool isPercent = false;
    public float AddStrength = 1;
    public float PriceInflationMultiplier = 0.3f;
    public int StartPrice = 1;
    public EthernalUpgrade(string title, string description,Stats stat,bool precent,float add,float inflation, int count, int price)
    {
        this.Title = title;
        this.targetStat = stat;
        this.isPercent = precent;
        this.AddStrength = add;
        this.PriceInflationMultiplier = inflation;
        this.Description = description;
        this.Count = count;
        this.StartPrice = price;
    }
    public int CalculateCurrentPrice()
    {
        return StartPrice + ((int)(StartPrice * PriceInflationMultiplier) * Count);
    }
}
