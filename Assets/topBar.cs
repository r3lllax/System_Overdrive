using DG.Tweening;
using TMPro;
using UnityEngine;

public class topBar : MonoBehaviour
{
    void Awake()
    {
        LoadData();
    }
    public void LoadData()
    {
        DataManager.LoadUserProfile();
        try
        {
            transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DataManager.CurrentUser.ProfileName;
            transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Денег:\n{DataManager.CurrentUser.Coins}";
        }
        catch { }
        
    }
    void Update()
    {
        if (TempData.needRefreshData)
        {
            LoadData();
        }   
    }
    void OnEnable()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(1f, 0.5f).From(0)).SetEase(Ease.OutBounce).Play();   
    }
    
}
