using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseButton : MonoBehaviour
{
    private ScenesController controller;
    [SerializeField] private GameObject weaponSelectPage;
    [SerializeField] private GameObject characterSelectPage;
    private bool needBuy = false;
    private bool ButtonIsShaking = false;
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
                if (OperationStatus)
                {
                    needBuy = OperationStatus;
                    TempData.CharacterIsLocked = !OperationStatus;
                }
                else
                {
                    if(ButtonIsShaking){ return; }
                    ShakeButton(gameObject);
                }
                
            }
            else
            {
                bool OperationStatus = DataManager.TryBuyWeapon(TempData.ChoosenWeapon);
                if (OperationStatus)
                {
                    needBuy = OperationStatus;
                    TempData.WeaponIsLocked = !OperationStatus;
                }
                else
                {
                    if(ButtonIsShaking){ return; }
                    ShakeButton(gameObject);
                }
            }
            return;
            
        }
        
        if (TempData.ChoosenCharacter && !TempData.CharIsPicked)
        {
            TempData.CharIsPicked = true;
            characterSelectPage.GetComponent<CharacterPage>().CloseAndOpenWeaponPage();
        }
        else
        {
            if (TempData.WeaponIsLocked || TempData.ChoosenWeapon == null) { return; }
            TempData.WeaponIsPicked = true;
            SessionData.Reset();
            SetChosenWeaponAndDefaultData();
            SceneManager.LoadScene("SampleScene");

        }
        
    }
    public void SetChosenWeaponAndDefaultData(){
        SessionData.Health = TempData.ChoosenCharacter.Health;
        SessionData.MoveSpeed = TempData.ChoosenCharacter.MoveSpeed;
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
        foreach (EthernalUpgrade upg in DataManager.CurrentUser.EthernalUpdates)
        {
            UpgradesController.DefineAndApplyEthernalUpgrade(upg,upg.Count*upg.AddStrength);
        }
    }
    private void ShakeButton(GameObject obj)
    {
        Vector2 objPos = obj.GetComponent<RectTransform>().anchoredPosition;
        Sequence sq = DOTween.Sequence();
        sq
        .Append(obj.GetComponent<RectTransform>().DOShakeAnchorPos(0.5f, 5).OnPlay(() => { ButtonIsShaking = true; }))
        .OnComplete(()=>{ obj.GetComponent<RectTransform>().anchoredPosition = objPos;ButtonIsShaking = false; })
        .Play();
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
