using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponCard : MonoBehaviour
{
    private TextMeshProUGUI WeaponName;
    private TextMeshProUGUI AttackSpeed;
    private TextMeshProUGUI PlayerSpeedMultiplier;
    private TextMeshProUGUI Damage;
    private TextMeshProUGUI RangeStats;
    private TextMeshProUGUI Bullets;
    [SerializeField] private Sprite NoChoosenImage;
    private Image choosenWeapon;
    private bool RenderAnimation = false;
    private string BulletType = "";
    void Awake()
    {
        choosenWeapon = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        WeaponName = transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        AttackSpeed = transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        PlayerSpeedMultiplier = transform.GetChild(1).transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        Damage = transform.GetChild(1).transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        RangeStats = transform.GetChild(1).transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
        Bullets = transform.GetChild(1).transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void RenderCurrentWeapon()
    {

        if (RenderAnimation)
        {
            return;
        }
        RenderAnimation = true;
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(0, 0.25f).From(1).OnComplete(()=>UpdateData()))
        // .Join(transform.DORotate(new Vector3(0,0,360), 0.25f, RotateMode.FastBeyond360)).SetRelative(true)
        .Append(transform.DOScale(1, 0.25f).From(0).OnComplete(() => RenderAnimation = false))
        .Play();

    }

    void UpdateData()
    {
        TempData.updateUI = false;
        if (TempData.ChoosenWeapon)
        {
            choosenWeapon.sprite = TempData.ChoosenWeapon.WeaponPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            WeaponName.text = $"{TempData.ChoosenWeapon.WeaponName}";
            string AS = TempData.ChoosenWeapon.BulletPrefab ? ((int)1 / TempData.ChoosenWeapon.GunAttackSpeed).ToString() : TempData.ChoosenWeapon.AnimationSpeed.ToString();
            string type = TempData.ChoosenWeapon.BulletPrefab ? "Скорость атаки" : "Скорость взмахов";
            AttackSpeed.text = $"{type}:{AS}";
            PlayerSpeedMultiplier.text = $"Множитель начальной скорости игрока: {TempData.ChoosenWeapon.PlayerSpeedMultiplier}x";
            Damage.text = $"Урон: {TempData.ChoosenWeapon.Damage}";
            if (TempData.ChoosenWeapon.BulletPrefab)
            {
                RangeStats.text = $"Размер магазина:  {TempData.ChoosenWeapon.GunMagazineSize}\nСкорость пуль: {TempData.ChoosenWeapon.GunBulletSpeed}\nВремя полета пуль: {TempData.ChoosenWeapon.GunBulletLifeTime}\nПерезарядка: {TempData.ChoosenWeapon.GunMagazineReloadTime}\n";
                string BulletDescription = "";
                TempData.ChoosenWeapon.BulletPrefab.GetComponent<PistolBullet>();
                if (TempData.ChoosenWeapon.BulletPrefab.name == "Bullet") {
                    BulletType = "Прошивающие";
                }
                else if(TempData.ChoosenWeapon.BulletPrefab.name == "BulletRevoler") {
                    BulletType = "Рикошетные";
                }
                
                
                switch (BulletType)
                {
                    case "Прошивающие":
                        BulletDescription = "Пули пролетают врагов на сквозь, все задетые враги получают урон";
                        break;
                    case "Рикошетные":
                        BulletDescription = "Пули рикошетят при взаимодействии с окружением";
                        break;

                }
                Bullets.text = $"Тип пуль:\n{BulletType}\n{BulletDescription}";
            }
            else
            {
                RangeStats.text = "";
                Bullets.text = "";
            }
        }
        else
        {
            choosenWeapon.sprite = NoChoosenImage;
            WeaponName.text = $"Выберите оружие";
            AttackSpeed.text = PlayerSpeedMultiplier.text = Damage.text = RangeStats.text = Bullets.text = "";
        }
        TempData.updateUI = false;
    }

    void Update()
    {
        if (TempData.updateUI)
        {
            RenderCurrentWeapon();

        }


    }
}
