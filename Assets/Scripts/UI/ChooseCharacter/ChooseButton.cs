using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseButton : MonoBehaviour
{
    private ScenesController controller;
    [SerializeField] private GameObject weaponSelectPage;
    [SerializeField] private GameObject characterSelectPage;
    void Awake()
    {
        controller = GetComponent<ScenesController>();
    }

    public void tryPickCharacter()
    {
        if (TempData.CharacterIsLocked || TempData.ChoosenCharacter == null) { return; }
        if (TempData.ChoosenCharacter && !TempData.CharIsPicked)
        {
            TempData.CharIsPicked = true;
            SessionData.Health = TempData.ChoosenCharacter.Health;
            SessionData.MoveSpeed = TempData.ChoosenCharacter.MoveSpeed;
            characterSelectPage.GetComponent<CharacterPage>().CloseAndOpenWeaponPage();
        }
        else
        {
            if (TempData.WeaponIsLocked || TempData.ChoosenWeapon == null){ return; }
            TempData.WeaponIsPicked = true;
            SetChosenWeaponAndDefaultData();
            SceneManager.LoadScene("SampleScene");
            
        }
        
    }
    public void SetChosenWeaponAndDefaultData(){
        SessionData.Damage = TempData.ChoosenWeapon.Damage;
        SessionData.AttackSpeedMelee = TempData.ChoosenWeapon.AnimationSpeed;
        SessionData.CdBetweenFire = TempData.ChoosenWeapon.GunAttackSpeed;
        SessionData.CdBetweenMagazine = TempData.ChoosenWeapon.GunMagazineReloadTime;
        SessionData.SprintMultiplier = 1.5f;
        SessionData.StartSpeedMultiplier = TempData.ChoosenWeapon.PlayerSpeedMultiplier;
        SessionData.MagazineCapacity = TempData.ChoosenWeapon.GunMagazineSize;
        SessionData.OneShootChance = 0;
        SessionData.CritChance = 0;
        SessionData.BulletSpeed = TempData.ChoosenWeapon.GunBulletSpeed;
        SessionData.BulletLifeTime = TempData.ChoosenWeapon.GunBulletLifeTime;
        SessionData.MeleeSize = TempData.ChoosenWeapon.WeaponPrefab.transform.localScale;
        SessionData.BulletSize = new Vector3(1,1,1);
    }
    void Update()
    {
        if (TempData.CharacterIsLocked || TempData.WeaponIsLocked)
        {
            transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "Купить";
        }
        else
        {
            transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "Выбрать";
        }
    }
}
