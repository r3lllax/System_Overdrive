using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponCardInGrid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Weapon weapon;
    public bool isLocked;
    private Image img;
    public string weaponName;
    [SerializeField] private TextMeshProUGUI WeaponName;
    [SerializeField] private Image Background;
    private Color32 lockedColor = new Color32(255, 0, 0, 94);
    private Color32 unlockedColor = new Color32(79, 255, 0, 94);
    void Awake()
    {
        img = transform.GetChild(1).gameObject.GetComponent<Image>();

    }
    void Start()
    {
        img.sprite = weapon.WeaponPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }
    [ContextMenu("PickChar")]
    public void SetChosenWeapon()
    {
        if (TempData.ChoosenWeapon == weapon) { return; }
        TempData.ChoosenWeapon = weapon;
        TempData.WeaponIsLocked = isLocked;
        TempData.updateUI = true;
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
    void Update()
    {
        WeaponName.text = weaponName;
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
