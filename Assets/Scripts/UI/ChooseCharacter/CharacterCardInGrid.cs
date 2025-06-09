using DG.Tweening;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCardInGrid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameCharacter character;
    public bool isLocked;
    private Image img;
    public string characterName;
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private Image Background;
    private Color32 lockedColor = new Color32(255, 0, 0, 94);
    private Color32 unlockedColor = new Color32(79, 255, 0, 94);
    void Awake()
    {
        img = transform.GetChild(1).gameObject.GetComponent<Image>();
    }
    void Start()
    {
        img.sprite = character.CharacterPrefab.GetComponent<SpriteRenderer>().sprite;
    }
    [ContextMenu("PickChar")]
    public void SetChosenCharacter()
    {
        if (TempData.ChoosenCharacter == character) { return; }
        SoundManager.PlaySound(SoundType.Clicks,0, DataManager.CurrentUser.Settings.EffectsVolume);
        TempData.ChoosenCharacter = character;
        TempData.CharacterIsLocked = isLocked;
        TempData.updateUI = true;

    }
    [ContextMenu("LoadWeapon")]
    public void changeScene()
    {
        SceneManager.LoadScene("ChooseEquipmentScene");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).From(transform.localScale).SetEase(Ease.InBack))
        .Join(transform.DORotate(new Vector3(0, 0, Random.Range(-20, 20)), 0.2f))
        .Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).From(transform.localScale).SetEase(Ease.OutBack))
        .Join(transform.DORotate(new Vector3(0, 0, 0), 0.2f))
        .Play();
    }
    private void Update() {
        CharacterName.text = characterName;
        if (isLocked)
        {
            Background.color = lockedColor;
        }
        else
        {
            Background.color = unlockedColor;
        }
    }
}
