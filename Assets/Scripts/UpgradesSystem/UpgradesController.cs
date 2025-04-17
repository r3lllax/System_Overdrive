using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class UpgradesController : MonoBehaviour
{
    [SerializeField]private AllUpgrades AllUpgrades;
    private List<Upgrade> AvailableUpgrades;
    private List<Upgrade> NowSuggestedUpgrades;
    private List<Upgrade> PlayerUpgrades;
    public static string PlayerType;
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

    public static float CalculateUpgradeValue(Upgrade upg){
        return UnityEngine.Random.Range(upg.minVal,upg.maxVal);
    }

    private static readonly Dictionary<string, Action<bool, float>> _fieldHandlers = new()
    {
        // Float
        { "Health", (isPercent, val) => ApplyToFloatField(ref SessionData.Health, isPercent, val) },
        { "MoveSpeed", (isPercent, val) => ApplyToFloatField(ref SessionData.MoveSpeed, isPercent, val) },
        { "ExpFinderRadius", (isPercent, val) => ApplyToFloatField(ref SessionData.ExpFinderRadius, isPercent, val) },
        { "AttackSpeedMelee", (isPercent, val) => ApplyToFloatField(ref SessionData.AttackSpeedMelee, isPercent, val) },
        { "CdBetweenFire", (isPercent, val) => ApplyToFloatField(ref SessionData.CdBetweenFire, isPercent, val) },
        { "CdBetweenMagazine", (isPercent, val) => ApplyToFloatField(ref SessionData.CdBetweenMagazine, isPercent, val) },
        { "StartSpeedMultiplier", (isPercent, val) => ApplyToFloatField(ref SessionData.StartSpeedMultiplier, isPercent, val) },
        { "SprintMultiplier", (isPercent, val) => ApplyToFloatField(ref SessionData.SprintMultiplier, isPercent, val) },
        { "MagazineCapacity", (isPercent, val) => ApplyToFloatField(ref SessionData.MagazineCapacity, isPercent, Convert.ToInt32(Math.Round(val))) },
        { "BulletSpeed", (isPercent, val) => ApplyToFloatField(ref SessionData.BulletSpeed, isPercent, val) },
        { "BulletLifeTime", (isPercent, val) => ApplyToFloatField(ref SessionData.BulletLifeTime, isPercent, val) },
 
        //Chances
        { "OneShootChance", (isPercent, val) => ApplyToChanceField(ref SessionData.OneShootChance, isPercent, val) },

        // Int 
        { "Damage", (isPercent, val) => ApplyToIntField(ref SessionData.Damage, isPercent, Mathf.RoundToInt(val)) },
        { "BulletBypassCount", (isPercent, val) => ApplyToIntField(ref SessionData.BulletBypassCount, isPercent, Mathf.RoundToInt(val)) },
        { "BulletRebonceCount", (isPercent, val) => ApplyToIntField(ref SessionData.BulletRebonceCount, isPercent, Mathf.RoundToInt(val)) },
        
        // V3
        { "MeleeSize", (isPercent, val) => ApplyToVector3Field(ref SessionData.MeleeSize, isPercent, val) },
        { "BulletSize", (isPercent, val) => ApplyToVector3Field(ref SessionData.BulletSize, isPercent, val) },
        
    };

 
    public static void DefineAndApplyVariable(Upgrade upg, float value)
    {
        string stat = upg.targerStat.ToString();
        
        if (_fieldHandlers.TryGetValue(stat, out var handler))
        {
            
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

    private static void ApplyToChanceField(ref float field, bool isPercent, float value)
    {
        if (isPercent)
            SessionData.AddProcentesChance(ref field, value);
        else
            SessionData.AddValueChance(ref field, value);
    }
    

    private static void ApplyToIntField(ref int field, bool isPercent, int value)
    {
        if (isPercent)
            SessionData.AddProcentesInt(ref field, value);
        else
            SessionData.AddValueInt(ref field, value);
    }

    private static void ApplyToFloatField(ref float field, bool isPercent, float value)
    {
        if (isPercent)
            SessionData.AddProcentesFloat(ref field, value);
        else
            SessionData.AddValueFloat(ref field, value);
    }

    private static void ApplyToVector3Field(ref Vector3 field, bool isPercent, float value)
    {
        if (isPercent)
            SessionData.AddProcentesV3(ref field, value);
        else
            SessionData.AddValueV3(ref field, value);
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
        for(int i=0;i<AllUpgrades.UpgradesList.Count;i++){
            if((AllUpgrades.UpgradesList[i].Melee && PlayerType=="Melee")||(AllUpgrades.UpgradesList[i].Range && PlayerType=="Range")){
                AvailableUpgrades.Add(AllUpgrades.UpgradesList[i]);
            }
        }
        Debug.Log($"{AvailableUpgrades.Count} - AVCOUNT");
    }
}
