using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeCard : MonoBehaviour
{
    [SerializeField]private Upgrade CurrentUpgrate;
    [SerializeField]private TextMeshProUGUI Title;
    [SerializeField]private Image image;
    [SerializeField]private TextMeshProUGUI Body;//Тут инфа о том сколько наносит итд
    [SerializeField] private TextMeshProUGUI Now;
    private LevelSystem playerLevelSystem;
    private GameObject Panel;
    private float value;
    private static int CurrentChoosenUpgradesCount = 0;
    
    public void SetCurrentUpgrade(Upgrade NEW)
    {
        CurrentUpgrate = NEW;
    }
    void Start()
    {
        playerLevelSystem = GameObject.FindWithTag("Player").transform.GetChild(0).transform.GetComponentInChildren<LevelSystem>();
        Panel = GameObject.FindWithTag("Panel");
        RenderUpgrate();
    }
    public void Choose()
    {
        if (Panel.GetComponent<UiUpgradePanel>().inAnim) { return; }
        CurrentChoosenUpgradesCount++;
        UpgradesController.DefineAndApplyVariable(CurrentUpgrate, value);
        UpgradesController.PlayerUpgrades.Add(CurrentUpgrate);
        if (CurrentChoosenUpgradesCount < SessionData.ChooseUpgradesCount &&(CurrentChoosenUpgradesCount!=0) && (CurrentUpgrate.targerStat.ToString() !="ChooseUpgradesCount"
        && CurrentUpgrate.targerStat.ToString() !="CanLifeSteal" && CurrentUpgrate.targerStat.ToString() !="CanDestroyEnemyBullet"))
        {
            RenderUpgrate();
            return;
        }
        else
        {
            CurrentChoosenUpgradesCount = 0;
        }
        if (playerLevelSystem.GetLevelUpsCount() > 1)
        {
            playerLevelSystem.Continue(true);
        }
        else
        {
            Panel.GetComponent<UiUpgradePanel>().inAnim = true;
            Panel.GetComponent<Animator>().SetTrigger("toggle");
        }
        
        
    }
    private void RenderUpgrate(bool fullRerender = true){
        Title.text = CurrentUpgrate.Name;
        image.sprite = CurrentUpgrate.Image;
        if (fullRerender)
        {
            value = UpgradesController.CalculateUpgradeValue(CurrentUpgrate);
        }
        string ProcenteDigit = CurrentUpgrate.Procente?"%":"";
        string ReadbleStatVar = UpgradesController.GetReadableString(CurrentUpgrate.targerStat.ToString());
        string ReadbleDebuffVar = UpgradesController.GetReadableString(CurrentUpgrate.debuffStat.ToString());
        string Debuff = CurrentUpgrate.HasDebuff?$@"<color=#FFFFFF>-{CurrentUpgrate.DebufSize}{ProcenteDigit} у {ReadbleDebuffVar}":"";
        string NowCount = $"<color=#9e9e9e>сейчас:<color={ReadbleStatVar.Substring(7,7)}> {SessionData.GetValue(CurrentUpgrate.targerStat.ToString())}";
        
        Now.text = NowCount;
        string body = 
        $@"<color=#FFFFFF>{Round(Convert.ToDecimal(value),2)}{ProcenteDigit} к {ReadbleStatVar}, \n{Debuff}";
        if (CurrentUpgrate.targerStat.ToString() == "CanDestroyEnemyBullet")
        {
            Now.text = "";
            body = $@"<color=#FFFFFF>{ReadbleStatVar}{Debuff}";
        }
        if (CurrentUpgrate.targerStat.ToString() == "CanLifeSteal")
        {
            Now.text = "";
            body = $@"<color=#FFFFFF>{ReadbleStatVar}{Debuff}";
        }
        if (CurrentUpgrate.targerStat.ToString() == "DamageEvadeChance")
        {
            body += $"\n<color=#FFFFFF>Каждое уклонение расходует 2%";
        }
        Body.text = body;
    }

    public static decimal Round(decimal number, int digits)
    {
        var factor = Convert.ToDecimal(Math.Pow(10, digits));
        return Math.Ceiling(number * factor) / factor;
    }
}
