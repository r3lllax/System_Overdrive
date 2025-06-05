using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseButton : MonoBehaviour
{
    private ScenesController controller;
    [SerializeField] private GameObject weaponSelectPage;
    [SerializeField] private GameObject characterSelectPage;
    private bool needBuy = false;
    void Awake()
    {
        controller = GetComponent<ScenesController>();
    }

    public void tryPickCharacter()
    {

        if (needBuy)
        {
            if (TempData.ActivePage == 0)
            {
                bool OperationStatus = DataManager.TryBuyCharacter(TempData.ChoosenCharacter);
                if(OperationStatus){
                    needBuy = OperationStatus;
                    TempData.CharacterIsLocked = !OperationStatus;
                };
            }
            else
            {
                bool OperationStatus = DataManager.TryBuyWeapon(TempData.ChoosenWeapon);
                if(OperationStatus){
                    needBuy = OperationStatus;
                    TempData.WeaponIsLocked = !OperationStatus;
                };
            }
            return;
            
        }
        
        if (TempData.ChoosenCharacter && !TempData.CharIsPicked)
        {
            TempData.CharIsPicked = true;
            SessionData.Health = TempData.ChoosenCharacter.Health;
            SessionData.MoveSpeed = TempData.ChoosenCharacter.MoveSpeed;
            characterSelectPage.GetComponent<CharacterPage>().CloseAndOpenWeaponPage();
        }
        else
        {
            if (TempData.WeaponIsLocked || TempData.ChoosenWeapon == null) { return; }
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
            int price = TempData.WeaponIsLocked ? TempData.ChoosenWeapon.Price : TempData.ChoosenCharacter.Price;
            string priceColor = price <= DataManager.CurrentUser.Coins ? "<color=#00ff44>" : "<color=#ff0000>";
            transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = $"{priceColor}{price}";
            needBuy = true;
        }
        else
        {
            needBuy = false;
            transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "Выбрать";
        }
    }
}
