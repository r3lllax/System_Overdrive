using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;

public static class DataManager
{
    private static string profilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    private static string gameFolder = Path.Combine(profilePath, "System_Overdrive");
    private static string savePath = $"{gameFolder}\\Profile.json";

    public static UserProfile CurrentUser;
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
            return JsonConvert.DeserializeObject<UserProfile>(File.ReadAllText(savePath));
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
    public static void CreateNewProfile()
    {
        if (!Directory.Exists(gameFolder))
        {
            Directory.CreateDirectory(gameFolder);
        }
        NewProfile(gameFolder);
    }
    private static void NewProfile(string path)
    {
        UserProfile newUser = new UserProfile();
        File.WriteAllText(savePath, JsonConvert.SerializeObject(newUser));
    }
}

public class UserProfile
{
    public string ProfileName = "Default";
    public int Coins = 0;
    public string[] UnlockedWeapon = new string[]{"Pistol"};
    public string[] UnlockedCharacters = new string[]{"Char1"};
    public Settings Settings = new Settings();
    public void ShowInfo()
    {
        Debug.Log($"ProfileName:{ProfileName},Coins:{Coins},UnlockedWeapon:{UnlockedWeapon.ToString()},UnlockedCharacters{UnlockedCharacters.ToString()},Settings{Settings.MusicVolume}");
    }
}
public class Settings
{
    public bool EnableEnemyDeathEffect = false;
    public float MusicVolume = 1f;
    public float EffectsVolume = 1f;
}