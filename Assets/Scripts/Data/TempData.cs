using UnityEngine;

public class TempData
{
    public static GameCharacter ChoosenCharacter;
    public static Weapon ChoosenWeapon;
    public static GameObject ExpPrefab;
    public static int ActivePage = 0;
    public static bool CharacterIsLocked = false;
    public static bool WeaponIsLocked = false;
    public static bool updateUI = false;
    public static bool CharIsPicked = false;
    public static bool WeaponIsPicked = false;
    public static bool needRefreshData = false;
    public static bool FirstAppearence = true;
}
