using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionData
{
    public static int Health;//✓
    public static float MoveSpeed;//✓
    public static int Damage;//✓
    public static int offersCount = 3;//✓
    public static float ExpFinderRadius = 3;//✓
    public static float AttackSpeedMelee;//✓
    public static float CdBetweenFire;//✓
    public static float CdBetweenMagazine;//✓
    public static float ExpMultiplier = 1f;//

    public static float StartSpeedMultiplier;// - NotForUser
    public static float SprintMultiplier;//✓
    public static int MagazineCapacity;//✓
    public static float OneShootChance;//✓
    public static float totalGameTime = 650f;//✓
    public static float CritChance;//✓
    public static float CritScale = 2f;//✓
    public static float EnemySpeedMultiplier = 1f;// - - NotForUser
    public static Vector3 MeleeSize;//✓
    public static Vector3 BulletSize;//✓
    public static float BulletSpeed;//✓
    public static float BulletLifeTime;//✓
    public static float AbilityCooldown = 0f;//✓
    public static float AbilityActiveTime = 0f;//✓
    public static int BulletBypassCount = 1;//✓
    public static int BulletRebonceCount = 1;//✓
    /// <summary>
    /// 
    /// </summary>

    public static float DamageEvadeChance = 0;//✓
    public static float LightningProcChance = 0;//✓
    public static int LightningMaxJumps = 3;//✓
    public static int ChooseUpgradesCount = 1;//✓
    public static float LightningJumpRadius = 5;//✓
    public static float LightningDamageMultiplier = 0.7f;//✓
    public static float LightningDelay = 0.15f;//✓
    public static float LifeStealStrength = 0.01f;//✓
    public static float LifeStealCurrentValue = 0f;//✓
    public static bool CanLifeSteal = false;
    public static bool NeedRefresh;
    // public static bool BackFire = false;
    public static bool CanDestroyEnemyBullet = false;
    public static int BackFire = 0;
    public static float ProcenteScaleMax = 1000;

    sealed class VariableReference
    {
        public Func<object> Get { get; private set; }
        public VariableReference(Func<object> getter)
        {
            Get = getter;
        }
    }

    private static Dictionary<string, VariableReference> UpgradeValue = new Dictionary<string, VariableReference>
    {
        {"Health",new VariableReference(() => Health)},
        {"MoveSpeed",new VariableReference(() => MoveSpeed.ToString("0.00"))},
        {"Damage",new VariableReference(() => Damage)},
        {"ExpFinderRadius",new VariableReference(() => ExpFinderRadius.ToString("0.00"))},
        {"AttackSpeedMelee",new VariableReference(() => AttackSpeedMelee.ToString("0.00"))},
        {"CdBetweenFire",new VariableReference(() => ((int)(1f/CdBetweenFire)))},
        {"CdBetweenMagazine",new VariableReference(() => CdBetweenMagazine.ToString("0.00"))},
        {"StartSpeedMultiplier",new VariableReference(() => StartSpeedMultiplier)},
        {"SprintMultiplier",new VariableReference(() => SprintMultiplier.ToString("0.00"))},
        {"MagazineCapacity",new VariableReference(() => MagazineCapacity)},
        {"OneShootChance",new VariableReference(() => ScaleValueToProcente(OneShootChance).ToString("0.00"))},
        {"CritChance",new VariableReference(() => ScaleValueToProcente(CritChance).ToString("0.00"))},
        {"EnemySpeedMultiplier",new VariableReference(() => EnemySpeedMultiplier.ToString("0.00"))},
        {"MeleeSize",new VariableReference(() => MeleeSize.x.ToString("0.00"))},
        {"BulletSize",new VariableReference(() => BulletSize.x.ToString("0.00"))},
        {"BulletSpeed",new VariableReference(() => BulletSpeed.ToString("0.00"))},
        {"BulletLifeTime",new VariableReference(() => BulletLifeTime.ToString("0.00"))},
        {"BulletBypassCount",new VariableReference(() => BulletBypassCount)},
        {"BulletRebonceCount",new VariableReference(() => BulletRebonceCount)},
        {"AbilityActiveTime",new VariableReference(() => AbilityActiveTime.ToString("0.00"))},
        {"AbilityCooldown",new VariableReference(() => AbilityCooldown.ToString("0.00"))},
        {"BackFire",new VariableReference(() => BackFire)},
        {"LightningProcChance",new VariableReference(() => ScaleValueToProcente(LightningProcChance).ToString("0.00"))},
        {"LightningMaxJumps",new VariableReference(() => LightningMaxJumps)},
        {"LightningJumpRadius",new VariableReference(() => LightningJumpRadius.ToString("0.00"))},
        {"LightningDamageMultiplier",new VariableReference(() => LightningDamageMultiplier.ToString("0.00"))},
        {"LightningDelay",new VariableReference(() => LightningDelay.ToString("0.00"))},
        {"offersCount",new VariableReference(() => offersCount)},
        {"CritScale",new VariableReference(() => (CritScale*100).ToString("0.00"))},
        {"CanDestroyEnemyBullet",new VariableReference(() => CanDestroyEnemyBullet)},
        {"LifeStealStrength",new VariableReference(() => ScaleValueToProcente(LifeStealStrength).ToString("0.000"))},
        {"LifeStealCurrentValue",new VariableReference(() => LifeStealCurrentValue)},
        {"CanLifeSteal",new VariableReference(() => CanLifeSteal)},
        {"DamageEvadeChance",new VariableReference(() => ScaleValueToProcente(DamageEvadeChance).ToString("0.00"))},
        {"ChooseUpgradesCount",new VariableReference(() => ChooseUpgradesCount)},



    };
    public static void Reset()
    {
        Health=0;
        MoveSpeed=0;
        Damage=0;
        offersCount=3;
        ExpFinderRadius=3;
        AttackSpeedMelee=0;
        CdBetweenFire=0;
        CdBetweenMagazine=0;
        ExpMultiplier=1;
        StartSpeedMultiplier=0;
        SprintMultiplier=0;
        MagazineCapacity=0;
        OneShootChance=0;
        CritChance=0;
        CritScale=2;
        EnemySpeedMultiplier=1;
        MeleeSize=Vector3.zero;
        BulletSize=Vector3.zero;
        BulletSpeed=0;
        BulletLifeTime=0;
        AbilityCooldown=0;
        AbilityActiveTime=0;
        BulletBypassCount=1;
        BulletRebonceCount=1;
        DamageEvadeChance=0;
        LightningProcChance=0;
        LightningMaxJumps=3;
        ChooseUpgradesCount=1;
        LightningJumpRadius=5;
        LightningDamageMultiplier=0.7f;
        LightningDelay=0.15f;
        LifeStealStrength=0.01f;
        LifeStealCurrentValue=0;
        CanLifeSteal=false;
        CanDestroyEnemyBullet=false;
        BackFire=0;
    }
    private static Dictionary<string[], string> PostfixDict = new Dictionary<string[], string>
    {
        {new string[]{"CritScale","LightningProcChance","DamageEvadeChance","CritChance","OneShootChance","LifeStealStrength"},"%"},
        {new string[]{"BulletLifeTime","AbilityActiveTime","AbilityCooldown"}," сек."},
        {new string[]{"CdBetweenFire"}," в сек."},
        {new string[]{"SprintMultiplier","EnemySpeedMultiplier","MeleeSize","BulletSize","LightningDamageMultiplier"},"x"},
    };
    public static string GetValue(string stat)
    {
        foreach (var key in PostfixDict)
        {
            if (key.Key.Contains(stat))
            {
                return $"{UpgradeValue[stat].Get()}{key.Value}";
            }
        }
        return $"{UpgradeValue[stat].Get()}";

    

    }
    
    
  
    //test
    public static void ShowData()
    {
        string data = $@"
            ===========================================
                        Health = {Health}
            ===========================================
            ===========================================
                    MoveSpeed = {MoveSpeed}
            ===========================================
            ===========================================
                        Damage = {Damage}
            ===========================================
            ===========================================
                        OneShootChance = {ScaleValueToProcente(OneShootChance)}
            ===========================================
            ===========================================
                        SprintMultiplier = {SprintMultiplier}
            ===========================================
            ===========================================
                AttackSpeedMelee = {AttackSpeedMelee}
            ===========================================
            ===========================================
                    CdBetweenFire = {CdBetweenFire}
            ===========================================
            ===========================================
                    CdBetweenMagazine = {CdBetweenMagazine}
            ===========================================
            ===========================================
                    SprintMultiplier = {SprintMultiplier}
            ===========================================
            ===========================================
                    MagazineCapacity = {MagazineCapacity}
            ===========================================
            ===========================================
                        MeleeSize = {MeleeSize}
            ===========================================
            ===========================================
                        BulletSize = {BulletSize}
            ===========================================
            ===========================================
                    BulletBypassCount = {BulletBypassCount}
            ===========================================
            ===========================================
                    BulletRebonceCount = {BulletRebonceCount}
            ===========================================
            ===========================================
                    DamageEvade = {ScaleValueToProcente(DamageEvadeChance)}
            ===========================================
            ===========================================
                    ExpFinderRadius = {ExpFinderRadius}
            ===========================================
            ===========================================
                    CritChance = {ScaleValueToProcente(CritChance)}
            ===========================================
            ===========================================
                    ExpMultiplier = {ExpMultiplier}
            ===========================================
            
        ";
        Debug.Log(data);
    }
    //test

    public static float ProcenteToScaleValue(float procente){
        return procente * 10f;
    }
    public static float ScaleValueToProcente(float scaleValue){
        if(scaleValue==0){return 0;}
        return scaleValue/10;
    }

    ///Chances
    public static void AddProcentesChance(ref float variable,float procente){
        float add = ProcenteToScaleValue(procente);
        Debug.Log(add);
        variable +=add;
        NeedRefresh = true;
    }
    public static void AddValueChance(ref float variable,float value){
        
        float temp = ProcenteToScaleValue(ScaleValueToProcente(variable)+value);
        variable=temp;
        NeedRefresh = true;
    }
    public static void DecreaseProcentesChance(ref float variable,float procente){

        float add = ProcenteToScaleValue(procente);
        variable-= (add>=variable)?variable:add;
        NeedRefresh = true;
    }
    public static void DecreaseValueChance(ref float variable,float value){

        float temp = ProcenteToScaleValue(ScaleValueToProcente(variable)-value);
        variable=temp;
        NeedRefresh = true;
    }
    public static void SetProcentesChance(ref float variable,float procente){
        variable = ProcenteToScaleValue(procente);
        NeedRefresh = true;
    }
    public static void SetValueChance(ref float variable,float value){
        
        variable=ProcenteToScaleValue(value);
        NeedRefresh = true;
    }
    /// Chances

    ///INT
    public static void AddProcentesInt(ref int variable,float procente){
        if(variable==0){return;}
        double Var = Convert.ToDouble(variable);
        double Add = Math.Round(Var/100*procente);
        variable += Convert.ToInt32(Add);
        NeedRefresh = true;
    }
    public static void AddValueInt(ref int variable,int addValue){
        variable+=addValue;
        NeedRefresh = true;
    }
    public static void DecreaseProcentesInt(ref int variable,float procente){
        if(variable==0){return;}
        
        double Var = Convert.ToDouble(variable);
        double Dec = Math.Round(Var/100*procente);
        variable -= Convert.ToInt32(Dec);
        NeedRefresh = true;

    }
    public static void DecreaseValueInt(ref int variable,int DecValue){
        
        variable-=DecValue;
        NeedRefresh = true;
    }
    public static void SetProcentesInt(ref int variable,float procente){
        if(variable==0){return;}
        
        double Var = Convert.ToDouble(variable);
        double Add = Math.Round(Var/100*procente);
        variable = Convert.ToInt32(Add);
        NeedRefresh = true;
    }
    public static void SetValueInt(ref int variable,int SetValue){
       
        variable=SetValue;
        NeedRefresh = true;
    }
    ///INT
     
    ///FLOAT
    public static void AddProcentesFloat(ref float variable,float procente){
        if(variable==0){return;}
        float Add = variable/100*procente;
        variable += Add;
        NeedRefresh = true;
    }
    public static void AddProcentesFloatScale(ref float variable,float procente){
        if(variable==0){return;}
        variable += procente/100;
        NeedRefresh = true;
    }
    public static void AddValueFloat(ref float variable, float addValue)
    {
        variable = variable + addValue <= 0 ? 0.1f : variable + addValue;
        NeedRefresh = true;
    }
    public static void DecreaseProcentesFloat(ref float variable,float procente){
        if(variable==0){return;}
        float Dec = variable/100*procente;
        variable -= Dec;
        NeedRefresh = true;
    }
    public static void DecreaseValueFloat(ref float variable,float decValue){

        variable = variable<=0?0.1f:variable-=decValue;
        
        NeedRefresh = true;
    }
    public static void SetProcentesFloat(ref float variable,float procente){
        if(variable==0){return;}
        
        float New = variable/100*procente;
        variable = New;
        NeedRefresh = true;
    }
    public static void SetValueFloat(ref float variable,float New){
        variable = New;
        NeedRefresh = true;
    }
    ///FLOAT
    
    ///V3
    public static void AddProcentesV3(ref Vector3 variable,float procente){
        float addX = variable.x==0?0:(variable.x/100*procente);
        float addY = variable.y==0?0:(variable.y/100*procente);
        float addZ = variable.z==0?0:(variable.z/100*procente);
        variable.x +=addX;
        variable.y +=addY;
        variable.z +=addZ;
        NeedRefresh = true;

    }
    public static void AddValueV3(ref Vector3 variable,float add){
        variable.x +=add;
        variable.y +=add;
        variable.z +=add;
        NeedRefresh = true;
    }
    public static void DecreaseProcentesV3(ref Vector3 variable,float procente){
        float decX = variable.x==0?0:(variable.x/100*procente);
        float decY = variable.y==0?0:(variable.y/100*procente);
        float decZ = variable.z==0?0:(variable.z/100*procente);
        float NewX = variable.x-=decX;
        float NewY = variable.y-=decY;
        float NewZ = variable.z-=decZ;
        variable.x = NewX>0?NewX:0;
        variable.y = NewY>0?NewY:0;
        variable.z = NewZ>0?NewZ:0;
        NeedRefresh = true;
    }
    public static void DecreaseValueV3(ref Vector3 variable,float dec){
        float NewX = variable.x-=dec;
        float NewY = variable.y-=dec;
        float NewZ = variable.z-=dec;
        variable.x =NewX<0?0:NewX;
        variable.y =NewY<0?0:NewY;
        variable.z =NewZ<0?0:NewZ;
        NeedRefresh = true;
    }
    public static void SetProcentesV3(ref Vector3 variable,float procente){
        float NewX = variable.x==0?0:(variable.x/100*procente);
        float NewY = variable.y==0?0:(variable.y/100*procente);
        float NewZ = variable.z==0?0:(variable.z/100*procente);
        variable.x = NewX;
        variable.y = NewY;
        variable.z = NewZ;
        NeedRefresh = true;
    }
    public static void SetValueV3(ref Vector3 variable,float New){
        variable.x =New;
        variable.y =New;
        variable.z =New;
        NeedRefresh = true;
    }
    ///V3
    ///Bool
    public static void SetBool(ref bool variable,bool value){
        variable = true;
    }
}
