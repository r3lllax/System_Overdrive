using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;

public static class DataManager
{
    private static string profilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    private static string gameFolder = Path.Combine(profilePath, "System_Overdrive");
    private static string savePath = $"{gameFolder}\\Profile.json";

    public static UserProfile CurrentUser;
    public static bool TryBuyCharacter(GameCharacter product)
    {

        if (CurrentUser.Coins < product.Price)
        {
            Debug.Log("Ошибка покупки");
        }
        else
        {
            Debug.Log(product.name);
            CurrentUser.Coins -= product.Price;
            CurrentUser.UnlockedCharacters.Add(product.name);
            SaveUserProfile();
            TempData.needRefreshData = true;
            return true;
        }
        
        
        return false;
    }
    public static bool TryBuyWeapon(Weapon product)
    {
        if (CurrentUser.Coins < product.Price)
        {
            Debug.Log("Ошибка покупки");
        }
        else
        {
            Debug.Log(product.name);
            CurrentUser.Coins -= product.Price;
            CurrentUser.UnlockedWeapon.Add(product.name);
            SaveUserProfile();
            TempData.needRefreshData = true;
            return true;
            
        }

        
        return false;
    }
    public static bool TryBuyEthernalSkill(EthernalUpgrade upgrade)
    {
        if (CurrentUser.Coins < upgrade.CalculateCurrentPrice())
        {
            Debug.Log("Ошибка покупки");
        }
        else
        {
            EthernalUpgrade foundObject = CurrentUser.EthernalUpdates.Find(obj => obj.targetStat == upgrade.targetStat);
            if (foundObject != null)
            {
                CurrentUser.Coins -= upgrade.CalculateCurrentPrice();
                foundObject.Count++;
                SaveUserProfile();
                TempData.needRefreshData = true;
            }
            
            
            return true;
            
        }

        
        return false;
    }
    public static bool SaveFileExists()
    {
        if (!File.Exists(savePath))
        {
            return false;
        }
        return true;
    }
    public static UserProfile GetUserProfile(string path)
    {
        if (!Directory.Exists(gameFolder))
        {
            Directory.CreateDirectory(gameFolder);
        }
        if (SaveFileExists())
        {
            return JsonConvert.DeserializeObject<UserProfile>(File.ReadAllText(savePath),new JsonSerializerSettings{ObjectCreationHandling=ObjectCreationHandling.Replace});
        }
        return null;
    }

    public static void LoadUserProfile()
    {
        CurrentUser = GetUserProfile(profilePath);
        if (CurrentUser != null)
        {
            CurrentUser.ShowInfo();
        }
        
    }
    public static void SaveUserProfile()
    {
        File.WriteAllText(savePath, JsonConvert.SerializeObject(CurrentUser));
    }
    public static void CreateNewProfile(string us = "Default")
    {
        if (!Directory.Exists(gameFolder))
        {
            Directory.CreateDirectory(gameFolder);
        }
        NewProfile(gameFolder,us);
    }
    private static void NewProfile(string path,string username="Default")
    {
        UserProfile newUser = new UserProfile();
        newUser.ProfileName = username;
        File.WriteAllText(savePath, JsonConvert.SerializeObject(newUser));
    }
}

public class UserProfile
{
    public string ProfileName = "Default";
    public int Coins = 0;
    public List<string> UnlockedWeapon = new List<string> { "Pistol" };
    public List<string> UnlockedCharacters = new List<string> { "Char1" };
    public List<EthernalUpgrade> EthernalUpdates = new List<EthernalUpgrade>
    {
        new EthernalUpgrade("Ученый","Увеличивает",EthernalUpgrade.Stats.ExpMultiplier,false,0.01f,0.3f,0,3000),
        new EthernalUpgrade("Уклонист","Увеличивает",EthernalUpgrade.Stats.DamageEvadeChance,true,1f,0.3f,0,6000),
        new EthernalUpgrade("Критическое мышление","Увеличивает",EthernalUpgrade.Stats.CritChance,true,1f,0.3f,0,8000),
        new EthernalUpgrade("Зоркий глаз","Увеличивает",EthernalUpgrade.Stats.ExpFinderRadius,false,0.05f,0.3f,0,1000),
        new EthernalUpgrade("Будда","Увеличивает",EthernalUpgrade.Stats.BackFire,false,1,1f,0,10000),
    };
    public Settings Settings = new Settings();
    public void ShowInfo()
    {
        Debug.Log($"ProfileName:{ProfileName},Coins:{Coins},EUCount {EthernalUpdates.Count}");
        foreach (var item in EthernalUpdates)
        {
            Debug.Log(item.Title);
        }
    }
}

public class Settings
{
    public bool EnableEnemyDeathEffect = false;
    public bool EnablePostProcessing = true;
    public int Vsync = 0;
    public float MusicVolume = 1f;
    public float EffectsVolume = 1f;
}