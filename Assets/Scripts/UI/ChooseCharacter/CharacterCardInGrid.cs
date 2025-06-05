using DG.Tweening;
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
    void Awake()
    {
        img = transform.GetChild(0).gameObject.GetComponent<Image>();
        
    }
    void Start()
    {
        img.sprite = character.CharacterPrefab.GetComponent<SpriteRenderer>().sprite;
    }
    [ContextMenu("PickChar")]
    public void SetChosenCharacter()
    {
        if(TempData.ChoosenCharacter == character){ return; }
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
        .Join(transform.DORotate(new Vector3(0, 0, Random.Range(-20,20)), 0.2f))
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
}
