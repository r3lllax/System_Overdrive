using UnityEngine;

public class test : MonoBehaviour
{
    [ContextMenu("CANDESTROY")]
    public void CANDESTROY(){
        SessionData.SetBool(ref SessionData.CanDestroyEnemyBullet,true);
        SessionData.ShowData();
    }
    [ContextMenu("Add10")]
    public void jumpcount(){
        SessionData.AddValueInt(ref SessionData.LightningMaxJumps,10);
        SessionData.ShowData();
    }
    [ContextMenu("Add1000ToCritScale")]
    public void Add50ToCritScale(){
        SessionData.AddProcentesFloatScale(ref SessionData.CritScale,1000);
        SessionData.ShowData();
    }
    [ContextMenu("AddBackFire")]
    public void AddBackFire(){
        SessionData.AddValueInt(ref SessionData.BackFire,1);
        SessionData.ShowData();
    }
    [ContextMenu("50Chancelightning")]
    public void Chancelightning(){
        SessionData.AddProcentesChance(ref SessionData.LightningProcChance,30);
        SessionData.ShowData();
    }
    [ContextMenu("Add5MagazineCapacity")]
    public void Add5MagazineCapacity(){
        SessionData.AddValueInt(ref SessionData.MagazineCapacity,5);
        SessionData.ShowData();
    }
    [ContextMenu("GAMESPEED")]
    public void GAMESPEED(){
        Time.timeScale =5;
    }
    [ContextMenu("GAMESPEED1/2")]
    public void GAMESPEEDhalf(){
        Time.timeScale =0.5f;
    }
    [ContextMenu("cdAb0")]
    public void cdAb0(){
        SessionData.SetValueFloat(ref SessionData.AbilityCooldown,0.1f);

    }
    
    
    [ContextMenu("GAMESPEEDDef")]
    public void GAMESPEEDDef(){
        Time.timeScale =1;
    }
    [ContextMenu("Add30ProcentHp")]
    public void Add30ProcentHp(){
        SessionData.AddValueInt(ref SessionData.Health,1);
        SessionData.ShowData();
    }
    [ContextMenu("BulletLifeTime1")]
    public void BulletLifeTime1(){
        SessionData.SetValueFloat(ref SessionData.BulletLifeTime,1);
        SessionData.ShowData();
    }
    [ContextMenu("IncreaseExpRadius100Procente")]
    public void IncreaseExpRadius(){
        SessionData.AddProcentesFloat(ref SessionData.ExpFinderRadius,100);
        SessionData.ShowData();
    }
    [ContextMenu("Add20ProcenteMeleeSpeed")]
    public void Add20ProcenteMeleeSpeed(){
        SessionData.AddProcentesFloat(ref SessionData.AttackSpeedMelee,20);
        SessionData.ShowData();
    }
    [ContextMenu("Add30ProcenteBulletSpeed")]
    public void Add30ProcenteBulletSpeed(){
        SessionData.AddProcentesFloat(ref SessionData.BulletSpeed,30);
        SessionData.ShowData();
    }
    [ContextMenu("Minus50ProcentCDFireSpeed")]
    public void Minus50ProcentCDFireSpeed(){
        SessionData.DecreaseProcentesFloat(ref SessionData.CdBetweenFire,50);
        SessionData.ShowData();
    }
    [ContextMenu("Minus50ProcentCDMagazine")]
    public void Minus50ProcentCDMagazine(){
        SessionData.DecreaseProcentesFloat(ref SessionData.CdBetweenMagazine,50);
        SessionData.ShowData();
    }
    [ContextMenu("AddDamage100Procente")]
    public void AddDamage33Procente(){
        SessionData.AddProcentesInt(ref SessionData.Damage,100);
        SessionData.ShowData();
    }
    [ContextMenu("SetOneShotChance100")]
    public void SetOneShotChance100(){
        SessionData.SetProcentesChance(ref SessionData.OneShootChance,100);
        SessionData.ShowData();
    }
    [ContextMenu("SetCritChance100")]
    public void SetCritChance100(){
        SessionData.SetProcentesChance(ref SessionData.CritChance,100);
        SessionData.ShowData();
    }
    [ContextMenu("SetCritChance50")]
    public void SetCritChance50(){
        SessionData.SetProcentesChance(ref SessionData.CritChance,50);
        SessionData.ShowData();
    }
    [ContextMenu("IncreaseSprint200Procente")]
    public void IncreaseSprint200Procente(){
        SessionData.AddProcentesFloat(ref SessionData.SprintMultiplier,200);
        SessionData.ShowData();
    }
    [ContextMenu("AddBypassCount")]
    public void AddBypassCount(){
        SessionData.AddValueInt(ref SessionData.BulletBypassCount,5);
        SessionData.ShowData();
    }
    [ContextMenu("AddBounceCount")]
    public void AddBounceCount(){
        SessionData.AddValueInt(ref SessionData.BulletRebonceCount,5);
        SessionData.ShowData();
    }
    [ContextMenu("AddBulletSize")]
    public void AddBulletSize(){
        SessionData.AddValueV3(ref SessionData.BulletSize,10f);
        SessionData.ShowData();
    }
    [ContextMenu("AddMeleeSize")]
    public void AddMeleeSize(){
            
        SessionData.ShowData();
    }
    [ContextMenu("AddMS")]
    public void AddMS(){
        SessionData.AddValueFloat(ref SessionData.MoveSpeed,15);
        SessionData.ShowData();
    }
    

}
