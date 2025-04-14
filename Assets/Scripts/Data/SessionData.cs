using System;
using Unity.VisualScripting;
using UnityEngine;

public class SessionData
{
    public static float Health;
    public static float MoveSpeed;
    public static int Damage;
    public static float AttackSpeedMelee;
    public static float CdBetweenFire;
    public static float CdBetweenMagazine;
    public static float SprintMultiplier;
    public static float MagazineCapacity;
    public static float OneShootChance = 235;
    public static Vector3 MeleeSize = new Vector3(2,1,0);
    public static Vector3 BulletSize;
    public static int BulletBypassCount;
    public static int BulletRebonceCount;
    public static bool NeedRefresh;
    private float ProcenteScaleMax = 1000;

    public static float ProcenteToScaleValue(float procente){
        return procente * 10f;
    }
    public static float ScaleValueToProcente(float scaleValue){
        if(scaleValue==0){return 0;}
        return scaleValue/10;
    }

    ///Chances
    public static void AddProcentesChance(ref float variable,float procente){
        NeedRefresh = true;
        float add = ProcenteToScaleValue(procente);
        variable+=add;

    }
    public static void AddValueChance(ref float variable,float value){
        NeedRefresh = true;
        float temp = ProcenteToScaleValue(ScaleValueToProcente(variable)+value);
        variable=temp;
    }
    public static void DecreaseProcentesChance(ref float variable,float procente){
        NeedRefresh = true;
        float add = ProcenteToScaleValue(procente);
        variable-= (add>=variable)?variable:add;
    }
    public static void DecreaseValueChance(ref float variable,float value){
        NeedRefresh = true;
        float temp = ProcenteToScaleValue(ScaleValueToProcente(variable)-value);
        variable=temp;
    }
    public static void SetProcentesChance(ref float variable,float procente){
        NeedRefresh = true;
        if(variable==0){return;}
        float New = variable/100*procente;
        variable = New;
    }
    public static void SetValueChance(ref float variable,float value){
        NeedRefresh = true;
        variable=ProcenteToScaleValue(value);
    }
    /// Chances

    ///INT
    public static void AddProcentesInt(ref int variable,float procente){
        if(variable==0){return;}
        NeedRefresh = true;
        double Var = Convert.ToDouble(variable);
        double Add = Math.Round(Var/100*procente);
        variable += Convert.ToInt32(Add);

    }
    public static void AddValueInt(ref int variable,int addValue){
        NeedRefresh = true;
        variable+=addValue;
    }
    public static void DecreaseProcentesInt(ref int variable,float procente){
        if(variable==0){return;}
        NeedRefresh = true;
        double Var = Convert.ToDouble(variable);
        double Dec = Math.Round(Var/100*procente);
        variable -= Convert.ToInt32(Dec);
    }
    public static void DecreaseValueInt(ref int variable,int DecValue){
        NeedRefresh = true;
        variable-=DecValue;
    }
    public static void SetProcentesInt(ref int variable,float procente){
        if(variable==0){return;}
        NeedRefresh = true;
        double Var = Convert.ToDouble(variable);
        double Add = Math.Round(Var/100*procente);
        variable = Convert.ToInt32(Add);
    }
    public static void SetValueInt(ref int variable,int SetValue){
        NeedRefresh = true;
        variable=SetValue;
    }
    ///INT
     
    ///FLOAT
    public static void AddProcentesFloat(ref float variable,float procente){
        if(variable==0){return;}
        NeedRefresh = true;
        float Add = variable/100*procente;
        variable += Add;
    }
    public static void AddValueFloat(ref float variable,float addValue){
        NeedRefresh = true;
        variable+=addValue;
    }
    public static void DecreaseProcentesFloat(ref float variable,float procente){
        if(variable==0){return;}
        NeedRefresh = true;
        float Dec = variable/100*procente;
        variable -= Dec;
    }
    public static void DecreaseValueFloat(ref float variable,float decValue){
        NeedRefresh = true;
        variable-=decValue;
    }
    public static void SetProcentesFloat(ref float variable,float procente){
        if(variable==0){return;}
        NeedRefresh = true;
        float New = variable/100*procente;
        variable = New;
    }
    public static void SetValueFloat(ref float variable,float New){
        NeedRefresh = true;
        variable = New;
    }
    ///FLOAT
    
    ///V3
    public static void AddProcentesV3(ref Vector3 variable,float procente){
        NeedRefresh = true;
        float addX = variable.x==0?0:(variable.x/100*procente);
        float addY = variable.y==0?0:(variable.y/100*procente);
        float addZ = variable.z==0?0:(variable.z/100*procente);
        variable.x +=addX;
        variable.y +=addY;
        variable.z +=addZ;
    }
    public static void AddValueV3(ref Vector3 variable,float add){
        NeedRefresh = true;
        variable.x +=add;
        variable.y +=add;
        variable.z +=add;
    }
    public static void DecreaseProcentesV3(ref Vector3 variable,float procente){
        NeedRefresh = true;
        float decX = variable.x==0?0:(variable.x/100*procente);
        float decY = variable.y==0?0:(variable.y/100*procente);
        float decZ = variable.z==0?0:(variable.z/100*procente);
        float NewX = variable.x-=decX;
        float NewY = variable.y-=decY;
        float NewZ = variable.z-=decZ;
        variable.x = NewX>0?NewX:0;
        variable.y = NewY>0?NewY:0;
        variable.z = NewZ>0?NewZ:0;

    }
    public static void DecreaseValueV3(ref Vector3 variable,float dec){
        NeedRefresh = true;
        float NewX = variable.x-=dec;
        float NewY = variable.y-=dec;
        float NewZ = variable.z-=dec;
        variable.x =NewX<0?0:NewX;
        variable.y =NewY<0?0:NewY;
        variable.z =NewZ<0?0:NewZ;
    }
    public static void SetProcentesV3(ref Vector3 variable,float procente){
        NeedRefresh = true;
        float NewX = variable.x==0?0:(variable.x/100*procente);
        float NewY = variable.y==0?0:(variable.y/100*procente);
        float NewZ = variable.z==0?0:(variable.z/100*procente);
        variable.x = NewX;
        variable.y = NewY;
        variable.z = NewZ;
    }
    public static void SetValueV3(ref Vector3 variable,float New){
        NeedRefresh = true;
        variable.x =New;
        variable.y =New;
        variable.z =New;
    }
    ///V3
}
