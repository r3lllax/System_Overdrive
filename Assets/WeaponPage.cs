using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponPage : MonoBehaviour
{
    [SerializeField] private GameObject CharacterPage;
    private PlayerControls playerControls;
    public void Open()
    {
        GetComponentInChildren<CurrentWeaponCard>().RenderCurrentWeapon();
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(1f, 0.5f).From(0)).SetEase(Ease.InOutCubic).Play().OnComplete(() => TempData.ActivePage = 1); ;
    }
    public void Close()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).Play().OnComplete(() => { gameObject.SetActive(!gameObject.activeSelf); TempData.ActivePage = 0; });
    }
    public void CloseAndOpenCharacterPage()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).OnComplete(() => { CharacterPage.GetComponent<CharacterPage>().Open(); })
        .Play();
    }
    void Awake()
    {
        playerControls = new PlayerControls();
    }
    void OnEnable()
    {
        playerControls.Enable();
    }
    void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        if (playerControls.UI.ToggleGameMenu.IsPressed())
        {
            TempData.ChoosenCharacter = null;
            TempData.ChoosenWeapon = null;
            TempData.ActivePage = 0;
            TempData.CharacterIsLocked = false;
            TempData.WeaponIsLocked = false;
            TempData.updateUI = false;
            TempData.CharIsPicked = false;
            TempData.WeaponIsPicked = false;
            TempData.needRefreshData = false;
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
