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
    private GameObject Panel;
    private float value;
    public void SetCurrentUpgrade(Upgrade NEW){
        CurrentUpgrate = NEW;
    }
    void Start()
    {
        Panel = GameObject.FindWithTag("Panel");
        RenderUpgrate();
    }
    public void Choose(){
        if(Panel.GetComponent<UiUpgradePanel>().inAnim){return;}
        Panel.GetComponent<UiUpgradePanel>().inAnim = true;
        Panel.GetComponent<Animator>().SetTrigger("toggle");
        UpgradesController.DefineAndApplyVariable(CurrentUpgrate,value);
        UpgradesController.PlayerUpgrades.Add(CurrentUpgrate);
        Time.timeScale = 1;
        
    }
    private void RenderUpgrate(){
        Title.text = CurrentUpgrate.Name;
        image.sprite = CurrentUpgrate.Image;
        value = UpgradesController.CalculateUpgradeValue(CurrentUpgrate);
        string ProcenteDigit = CurrentUpgrate.Procente?"%":"";
        string Debuff = CurrentUpgrate.HasDebuff?$@"-{CurrentUpgrate.DebufSize}{ProcenteDigit} у {CurrentUpgrate.debuffStat}":"";
        string body = 
        $@"{Round(Convert.ToDecimal(value),2)}{ProcenteDigit} к {CurrentUpgrate.targerStat}\n{Debuff}";
        Body.text = body;
    }

    public static decimal Round(decimal number, int digits)
    {
        var factor = Convert.ToDecimal(Math.Pow(10, digits));
        return Math.Ceiling(number * factor) / factor;
    }
}
