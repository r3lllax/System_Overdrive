using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponCardInGrid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Weapon weapon;
    public bool isLocked;
    private Image img;
    void Awake()
    {
        img = transform.GetChild(0).gameObject.GetComponent<Image>();
        
    }
    void Start()
    {
        img.sprite = weapon.WeaponPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }
    [ContextMenu("PickChar")]
    public void SetChosenWeapon()
    {
        if(TempData.ChoosenWeapon == weapon){ return; }
        TempData.ChoosenWeapon = weapon;
        TempData.WeaponIsLocked = isLocked;
        TempData.updateUI = true;
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
