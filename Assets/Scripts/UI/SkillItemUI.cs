using System.Runtime.Serialization;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillItemUI : MonoBehaviour
{
    public EthernalUpgrade currentUpgrade;
    private TextMeshProUGUI Title;
    private TextMeshProUGUI CurrentCount;
    private TextMeshProUGUI Description;
    private TextMeshProUGUI Price;
    private bool ButtonIsShaking = false;
    void Awake()
    {
        Title = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        CurrentCount = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        Description = transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        Price = transform.GetChild(3).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }
    public void SetData()
    {
        currentUpgrade = DataManager.CurrentUser.EthernalUpdates.Find(obj => obj.targetStat == currentUpgrade.targetStat);
        Title.text = $"{currentUpgrade.Title}";
        CurrentCount.text = $"Текущее {currentUpgrade.Count} шт.";
        string procente = currentUpgrade.isPercent ? "%" : "";
        Description.text = $"{currentUpgrade.Description} {UpgradesController.GetReadableString(currentUpgrade.targetStat.ToString())} <color=#FFFFFF>на {currentUpgrade.AddStrength}{procente} за 1 шт.";
        int price = currentUpgrade.CalculateCurrentPrice();
        string priceColor = price <= DataManager.CurrentUser.Coins ? "<color=#00ff44>" : "<color=#ff0000>";
        Price.text = $"{priceColor}{currentUpgrade.CalculateCurrentPrice()}";
    }
    void Update()
    {
        if (TempData.needRefreshData)
        {
            SetData();
        }
    }
    public void buy(GameObject obj)
    {
        if(ButtonIsShaking){ return; }
        bool operationStatus = DataManager.TryBuyEthernalSkill(currentUpgrade);
        if (operationStatus)
        {
            Debug.Log("Вечная способность куплена");
        }
        else
        {
            ShakeButton(obj);
            Debug.Log("Ошибка покупки вечной способности");
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
}
