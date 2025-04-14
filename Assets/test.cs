using UnityEngine;

public class test : MonoBehaviour
{
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
        SessionData.AddValueV3(ref SessionData.MeleeSize,1f);
        SessionData.ShowData();
    }
    [ContextMenu("AddMS")]
    public void AddMS(){
        SessionData.AddValueFloat(ref SessionData.MoveSpeed,15);
        SessionData.ShowData();
    }
    
}
