using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class UpgradesController : MonoBehaviour
{
    private static Dictionary<string, string> UpgradeName = new Dictionary<string, string>
    {
        {"Health","<color=#87d96f>Здоровье<#87d96f>"},
        {"MoveSpeed","<color=#62bcf5>Скорость передвижения<#ffd12b>"},
        {"Damage","<color=#ff4f4f>Урон<#ff4f4f>"},
        {"ExpFinderRadius","<color=#b196ff>Радиус поиска<#b196ff>"},
        {"AttackSpeedMelee","<color=#87d96f>Скорость атаки<#87d96f>"},
        {"CdBetweenFire","<color=#ffd12b>Скорострельность<#ffd12b>"},
        {"CdBetweenMagazine","<color=#9cb1f0>Перезарядка<#9cb1f0>"},
        {"StartSpeedMultiplier","Начальная скорость атаки"},
        {"SprintMultiplier","<color=#ffd12b>Множитель спринта<#ffd12b>"},
        {"MagazineCapacity","<color=#ffd12b>Размер магазина<#ffd12b>"},
        {"OneShootChance","<color=#ff29ad>Шанс ваншота<#ff29ad>"},
        {"CritChance","<color=#ff2929>Шанс крита<#ff2929>"},
        {"EnemySpeedMultiplier","<color=#ff2929>Множитель скорости врагов<#ff2929>"},
        {"MeleeSize","<color=#62bcf5>Размер оружия<#62bcf5>"},
        {"BulletSize","<color=#62bcf5>Размер пуль<#62bcf5>"},
        {"BulletSpeed","<color=#ffd12b>Скорость пуль<#ffd12b>"},
        {"BulletLifeTime","<color=#f57c20>Время полета пуль<#f57c20>"},
        {"BulletBypassCount","<color=#f57c20>Количество пробиваемых пулей целей<#f57c20>"},
        {"BulletRebonceCount","<color=#f57c20>Количество отскоков пули<#f57c20>"},
        {"AbilityActiveTime","<color=#62bcf5>Время действия способности<#62bcf5>"},
        {"AbilityCooldown","<color=#80a9d9>Время перезарядки способности<#80a9d9>"},
        {"BackFire","<color=#faf739>Доп. атака сзади<#faf739>"},
        {"LightningProcChance","<color=#2cf5ee>Шанс появления молнии<#2cf5ee>"},
        {"LightningMaxJumps","<color=#2cf5ee>Количество отскоков молнии<#2cf5ee>"},
        {"LightningJumpRadius","<color=#2cf5ee>Радиус отскока молнии<#2cf5ee>"},
        {"LightningDamageMultiplier","<color=#2cf5ee>Множитель урона за отскок молнии<#2cf5ee>"},
        {"LightningDelay","<color=#2cf5ee>Задержка меджу отскоками молнии<#2cf5ee>"},
        {"offersCount","<color=#ffd12b>Количество предлагаемых улучшений<#ffd12b>"},
        {"CritScale","<color=#ff4f4f>Урон критических попаданий<#ff4f4f>"},
        {"CanDestroyEnemyBullet","<color=#ff29ad>Теперь вы можете ломать пули врагов<#ff29ad>"},
        {"CanLifeSteal","<color=#ff29ad>Каждый убитый враг заполняет новое очко здоровья, при заполнении дается +1 здоровье<#ff29ad>"},
        {"LifeStealStrength","<color=#ff29ad>Восстанавливаемому здоровью<#ff29ad>"},
        {"DamageEvadeChance","<color=#ff29ad>Шанс избежать урон<#ff29ad>"},
        {"ChooseUpgradesCount","<color=#ffd12b>Количество выбираемых улучшений за уровень<#ffd12b>"},
        {"ExpMultiplier","<color=#31de5f>Множитель получаемого опыта<#31de5f>"},

    };
    
    [SerializeField] private AllUpgrades AllUpgrades;
    private List<Upgrade> AvailableUpgrades;
    private List<Upgrade> NowSuggestedUpgrades;
    public static List<Upgrade> PlayerUpgrades;
    public static string PlayerType;
    public static string BulletType = "None";
    [SerializeField]private GameObject CardPrefab;
    private GameObject Panel;
    private void Awake()
    {
        AvailableUpgrades = new List<Upgrade>();
        NowSuggestedUpgrades = new List<Upgrade>();
        PlayerUpgrades = new List<Upgrade>();
        Panel = GameObject.FindWithTag("Panel");
    }
    void Start()
    {
        FilterUpgrades();
        GenerateOfferUpdates(3);
    }
    [ContextMenu("PlayerUpgrades")]
    public void PlayerCollectedUpdates()
    {
        foreach (var obj in PlayerUpgrades)
        {
            Debug.Log($"Player upgrade - {obj.name}");
        }
    }

    public static string GetReadableString(string param)
    {
        try
        {
            return UpgradeName[param];
        }
        catch
        {
            return "NOT FOUND";
        }
    }
    public static float CalculateUpgradeValue(Upgrade upg)
    {
        switch (upg.targerStat.ToString())
        {
            case "Health":
                return UnityEngine.Random.Range((int)upg.minVal, (int)upg.maxVal);
                break;
            case "Damage":
                return UnityEngine.Random.Range((int)upg.minVal, (int)upg.maxVal);
                break;
            case "MagazineCapacity":
                return UnityEngine.Random.Range((int)upg.minVal, (int)upg.maxVal);
                break;
            case "BulletBypassCount":
                return UnityEngine.Random.Range((int)upg.minVal, (int)upg.maxVal);
                break;
            case "BulletRebonceCount":
                return UnityEngine.Random.Range((int)upg.minVal, (int)upg.maxVal);
                break;
            case "offersCount":
                return UnityEngine.Random.Range((int)upg.minVal, (int)upg.maxVal);
                break;
            case "LightningMaxJumps":
                return UnityEngine.Random.Range((int)upg.minVal, (int)upg.maxVal);
                break;
            case "BackFire":
                return UnityEngine.Random.Range((int)upg.minVal, (int)upg.maxVal);
                break;
             case "ChooseUpgradesCount":
                return UnityEngine.Random.Range((int)upg.minVal, (int)upg.maxVal);
                break;
        }
        return UnityEngine.Random.Range(upg.minVal, upg.maxVal);
    }

    private static readonly Dictionary<string, Action<bool, float>> _fieldHandlers = new()
    {
        // Float
        
        { "MoveSpeed", (isPercent, val) => ApplyToFloatField(ref SessionData.MoveSpeed, isPercent, val) },
        { "ExpFinderRadius", (isPercent, val) => ApplyToFloatField(ref SessionData.ExpFinderRadius, isPercent, val) },
        { "AttackSpeedMelee", (isPercent, val) => ApplyToFloatField(ref SessionData.AttackSpeedMelee, isPercent, val) },
        { "CdBetweenFire", (isPercent, val) => ApplyToFloatField(ref SessionData.CdBetweenFire, isPercent, val) },
        { "CdBetweenMagazine", (isPercent, val) => ApplyToFloatField(ref SessionData.CdBetweenMagazine, isPercent, val) },
        { "StartSpeedMultiplier", (isPercent, val) => ApplyToFloatField(ref SessionData.StartSpeedMultiplier, isPercent, val) },
        { "ExpMultiplier", (isPercent, val) => ApplyToFloatField(ref SessionData.ExpMultiplier, isPercent, val) },
        { "SprintMultiplier", (isPercent, val) => ApplyToFloatField(ref SessionData.SprintMultiplier, isPercent, val) },
        { "BulletSpeed", (isPercent, val) => ApplyToFloatField(ref SessionData.BulletSpeed, isPercent, val) },
        { "BulletLifeTime", (isPercent, val) => ApplyToFloatField(ref SessionData.BulletLifeTime, isPercent, val) },
        { "AbilityActiveTime", (isPercent, val) => ApplyToFloatField(ref SessionData.AbilityActiveTime, isPercent, val) },
        { "AbilityCooldown", (isPercent, val) => ApplyToFloatField(ref SessionData.AbilityCooldown, isPercent, val) },
        { "LightningJumpRadius", (isPercent, val) => ApplyToFloatField(ref SessionData.LightningJumpRadius, isPercent, val) },
        { "LightningDamageMultiplier", (isPercent, val) => ApplyToFloatField(ref SessionData.LightningDamageMultiplier, isPercent, val) },
        { "LightningDelay", (isPercent, val) => ApplyToFloatField(ref SessionData.LightningDelay, isPercent, val) },
        { "CritScale", (isPercent, val) => ApplyToFloatScaleField(ref SessionData.CritScale, isPercent, val) },
        { "LifeStealStrength", (isPercent, val) => ApplyToFloatScaleField(ref SessionData.CritScale, isPercent, val) },
 
        //Chances
        { "OneShootChance", (isPercent, val) => ApplyToChanceField(ref SessionData.OneShootChance, isPercent, val) },
        { "CritChance", (isPercent, val) => ApplyToChanceField(ref SessionData.CritChance, isPercent, val) },
        { "LightningProcChance", (isPercent, val) => ApplyToChanceField(ref SessionData.LightningProcChance, isPercent, val) },
        { "DamageEvadeChance", (isPercent, val) => ApplyToChanceField(ref SessionData.DamageEvadeChance, isPercent, val) },


        // Int 
        { "Damage", (isPercent, val) => ApplyToIntField(ref SessionData.Damage, isPercent, Mathf.RoundToInt(val)) },
        { "BulletBypassCount", (isPercent, val) => ApplyToIntField(ref SessionData.BulletBypassCount, isPercent, Mathf.RoundToInt(val)) },
        { "BulletRebonceCount", (isPercent, val) => ApplyToIntField(ref SessionData.BulletRebonceCount, isPercent, Mathf.RoundToInt(val)) },
        { "Health", (isPercent, val) => ApplyToIntField(ref SessionData.Health, isPercent, Mathf.RoundToInt(val)) },
        { "MagazineCapacity", (isPercent, val) => ApplyToIntField(ref SessionData.MagazineCapacity, isPercent, Mathf.RoundToInt(val)) },
        { "BackFire", (isPercent, val) => ApplyToIntField(ref SessionData.BackFire, isPercent, Mathf.RoundToInt(val)) },
        { "LightningMaxJumps", (isPercent, val) => ApplyToIntField(ref SessionData.LightningMaxJumps, isPercent, Mathf.RoundToInt(val)) },
        { "offersCount", (isPercent, val) => ApplyToIntField(ref SessionData.offersCount, isPercent, Mathf.RoundToInt(val)) },
        { "ChooseUpgradesCount", (isPercent, val) => ApplyToIntField(ref SessionData.ChooseUpgradesCount, isPercent, Mathf.RoundToInt(val)) },

        // V3
        { "MeleeSize", (isPercent, val) => ApplyToVector3Field(ref SessionData.MeleeSize, isPercent, val) },
        { "BulletSize", (isPercent, val) => ApplyToVector3Field(ref SessionData.BulletSize, isPercent, val) },
        //Bool
        // { "BackFire", (isPercent, val) => ApplyToBoolField(ref SessionData.BackFire, isPercent, Convert.ToBoolean(val)) },
        { "CanDestroyEnemyBullet", (isPercent, val) => ApplyToBoolField(ref SessionData.CanDestroyEnemyBullet, isPercent, Convert.ToBoolean(val)) },
        { "CanLifeSteal", (isPercent, val) => ApplyToBoolField(ref SessionData.CanLifeSteal, isPercent, Convert.ToBoolean(val)) }

        
    };

 
    public static void DefineAndApplyVariable(Upgrade upg, float value)
    {
        string stat = upg.targerStat.ToString();
        
        if (_fieldHandlers.TryGetValue(stat, out var handler))
        {
            Debug.Log($"value - {value}");
            handler(upg.Procente, value);
            
            
            if (!upg.HasDebuff) return;
    
            var debuffUpg = new Upgrade
            {
                targerStat = upg.debuffStat,
                Procente = upg.Procente,
                DebufSize = upg.DebufSize
            };
            
            DefineAndApplyVariable(debuffUpg, -upg.DebufSize);  
        }

    }
    public static void DefineAndApplyEthernalUpgrade(EthernalUpgrade upg, float value)
    {
        string stat = upg.targetStat.ToString();
        
        if (_fieldHandlers.TryGetValue(stat, out var handler))
        {
            handler(upg.isPercent, value);
            Debug.Log($"Стартовое улучшение {stat} на {value}(Procente - {upg.isPercent})");   
        }

    }

    private static void ApplyToChanceField(ref float field, bool isPercent, float value)
    {
        if (isPercent)
            SessionData.AddProcentesChance(ref field, value);
        else
            SessionData.AddValueChance(ref field, value);
            Debug.Log(field);

    }

    private static void ApplyToBoolField(ref bool field, bool isPercent, bool value)
    {

        SessionData.SetBool(ref field, value);
        Debug.Log(field);
       
    }


    private static void ApplyToFloatScaleField(ref float field, bool isPercent, float value)
    {
        if (isPercent)
            SessionData.AddProcentesFloatScale(ref field, value);
        else
            SessionData.AddValueFloat(ref field, value);
            Debug.Log(field);

    }

    private static void ApplyToIntField(ref int field, bool isPercent, int value)
    {
        if (isPercent)
            SessionData.AddProcentesInt(ref field, value);
        else
            SessionData.AddValueInt(ref field, value);
            Debug.Log(field);

    }

    private static void ApplyToFloatField(ref float field, bool isPercent, float value)
    {
        if (isPercent)
            SessionData.AddProcentesFloat(ref field, value);
        else
            SessionData.AddValueFloat(ref field, value);
            Debug.Log(field);

    }

    private static void ApplyToVector3Field(ref Vector3 field, bool isPercent, float value)
    {
        if (isPercent)
            SessionData.AddProcentesV3(ref field, value);
        else
            SessionData.AddValueV3(ref field, value);
            Debug.Log(field);

    }

    /// 

    public void GenerateOfferUpdates(int n){
        NowSuggestedUpgrades.Clear();
        FilterUpgrades();
        for(int i =0;i<n;i++){
            OfferUpdates();
        }
        UiUpgradePanel panelComponent = Panel.GetComponent<UiUpgradePanel>();
        Vector3 StartPos = new Vector3(-400,0,0); 
        
        for(int i=0;i<n;i++){
            StartPos.x+=200;
            panelComponent.CreateCard(StartPos,CardPrefab,NowSuggestedUpgrades[i]);
        }
    }
    private Upgrade PopUpgrade(int num){
        Upgrade poppedItem = AvailableUpgrades[num];
        AvailableUpgrades.RemoveAt(num);
        return poppedItem;
    }
    [ContextMenu("OfferUpdates")]
    public void OfferUpdates(){
        if(AvailableUpgrades.Count==0){return;}
        var NewUpdate = PopUpgrade(UnityEngine.Random.Range(0,AvailableUpgrades.Count));
        
        if(InSuggestedUpdates(NewUpdate.name)){
            OfferUpdates();
        }
        else{
            NowSuggestedUpgrades.Add(NewUpdate);
        }
        
    }

    private bool InSuggestedUpdates(string name){
        for(int i = 0;i<NowSuggestedUpgrades.Count;i++){
            if(NowSuggestedUpgrades[i].name == name){
                return true;
            }
        }
        return false;
    }

    [ContextMenu("ShowAvailables")]
    public void Show(){
        foreach(var item in AvailableUpgrades){
            Debug.Log($"AVITEM - {item.name}");
            Debug.Log($"stat - {item.targerStat}");
        }
    }
    public void FilterUpgrades(){
        AvailableUpgrades.Clear();
        for (int i = 0; i < AllUpgrades.UpgradesList.Count; i++)
        {
            if ((AllUpgrades.UpgradesList[i].Melee && PlayerType == "Melee") || (AllUpgrades.UpgradesList[i].Range && PlayerType == "Range"))
            {
                if (SessionData.CanLifeSteal==true && AllUpgrades.UpgradesList[i].targerStat.ToString() == "CanLifeSteal")
                {
                    continue;
                }
                if (SessionData.CanDestroyEnemyBullet == true && AllUpgrades.UpgradesList[i].targerStat.ToString() == "CanDestroyEnemyBullet")
                {
                    continue;
                }
                if (SessionData.CanLifeSteal == false && (AllUpgrades.UpgradesList[i].targerStat.ToString() == "LifeStealStrength"))
                {
                    continue;
                }
                if (SessionData.LightningProcChance == 0 && (AllUpgrades.UpgradesList[i].targerStat.ToString() == "LightningJumpRadius" || AllUpgrades.UpgradesList[i].targerStat.ToString() == "LightningDamageMultiplier" || AllUpgrades.UpgradesList[i].targerStat.ToString() == "LightningDelay" || AllUpgrades.UpgradesList[i].targerStat.ToString() == "LightningMaxJumps"))
                {
                    continue;
                }
                switch (BulletType)
                {
                    case "Bullet":
                        if (AllUpgrades.UpgradesList[i].targerStat.ToString() != "BulletRebonceCount")
                        {
                            AvailableUpgrades.Add(AllUpgrades.UpgradesList[i]);
                        }
                        break;
                    case "BulletRevoler":
                        if (AllUpgrades.UpgradesList[i].targerStat.ToString() != "BulletBypassCount")
                        {
                            AvailableUpgrades.Add(AllUpgrades.UpgradesList[i]);
                        }
                        break;
                    case "None":
                        AvailableUpgrades.Add(AllUpgrades.UpgradesList[i]);
                        break;
                }
            }
            
        }
    }
}
