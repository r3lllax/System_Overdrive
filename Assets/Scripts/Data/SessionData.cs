using System;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;

public class SessionData
{
    public static float Health;//✓
    public static float MoveSpeed;//✓
    public static int Damage;//✓
    public static float ExpFinderRadius = 3;//✓
    public static float AttackSpeedMelee;//✓
    public static float CdBetweenFire;//✓
    public static float CdBetweenMagazine;//✓
    public static float StartSpeedMultiplier;//✓
    public static float SprintMultiplier;//✓
    public static float MagazineCapacity;//✓
    public static float OneShootChance;//✓
    public static Vector3 MeleeSize;//✓
    public static Vector3 BulletSize;//✓
    public static float BulletSpeed;//✓
    public static float BulletLifeTime;//✓
    public static int BulletBypassCount = 1;//✓
    public static int BulletRebonceCount = 1;//✓
    public static bool NeedRefresh;
    public static float ProcenteScaleMax = 1000;

    //test
    public static void ShowData(){
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
        variable+=add;
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
        Debug.Log($"Переменная:{variable}");
        float Add = variable/100*procente;
        Debug.Log($"Добавка:{Add}");
        variable += Add;
        Debug.Log($"Переменная:{variable}");
        NeedRefresh = true;
    }
    public static void AddValueFloat(ref float variable,float addValue){
        variable+=addValue;
        NeedRefresh = true;
    }
    public static void DecreaseProcentesFloat(ref float variable,float procente){
        if(variable==0){return;}
        float Dec = variable/100*procente;
        variable -= Dec;
        NeedRefresh = true;
    }
    public static void DecreaseValueFloat(ref float variable,float decValue){

        variable-=decValue;
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
}
