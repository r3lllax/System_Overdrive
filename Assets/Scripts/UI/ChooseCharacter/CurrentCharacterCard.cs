using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrentCharacterCard : MonoBehaviour
{
    private TextMeshProUGUI health;
    private TextMeshProUGUI moveSpeed;
    private TextMeshProUGUI AbilityTitle;
    private TextMeshProUGUI AbilityDescription;
    private TextMeshProUGUI AbilityStatsTitle;
    private TextMeshProUGUI AbilityStats;
    [SerializeField] private Sprite NoChoosenImage;
    private Image choosenChar;
    private bool RenderAnimation = false;
    void Awake()
    {
        choosenChar = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        health = transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        moveSpeed = transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        AbilityTitle = transform.GetChild(1).transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        AbilityDescription = transform.GetChild(1).transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        AbilityStatsTitle = transform.GetChild(1).transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
        AbilityStats = transform.GetChild(1).transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void RenderCurrentCharacter()
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
        if (TempData.ChoosenCharacter)
        {
            choosenChar.sprite = TempData.ChoosenCharacter.CharacterPrefab.GetComponent<SpriteRenderer>().sprite;
            health.text = $"Здоровье: {TempData.ChoosenCharacter.Health}";
            moveSpeed.text = $"Скорость передвижения: {TempData.ChoosenCharacter.MoveSpeed}";
            AbilityDescription.text = $"{TempData.ChoosenCharacter.AbilityDescription}";
            AbilityStatsTitle.text = "Характеристики способности";
            AbilityTitle.text = "Способность";
            AbilityStats.text = $"Время перезарядки: {TempData.ChoosenCharacter.CooldownTime} сек.\nВремя применения: {TempData.ChoosenCharacter.CastTime} сек.\nВремя действия: {TempData.ChoosenCharacter.ActiveTime} сек.";
        }
        else
        {
            choosenChar.sprite = NoChoosenImage;
            health.text = $"Выберите персонажа";
            moveSpeed.text = AbilityDescription.text = AbilityStats.text = AbilityStatsTitle.text = AbilityTitle.text = "";
        }
        TempData.updateUI = false;
    }

    void Update()
    {
        if (TempData.updateUI)
        {
            RenderCurrentCharacter();

        }


    }
    
}
