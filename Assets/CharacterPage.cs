using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterPage : MonoBehaviour
{
    [SerializeField] private GameObject WeaponPage;
    private PlayerControls playerControls;
    public void Open()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(1f, 0.5f).From(0)).SetEase(Ease.InOutCubic).Play().OnPlay(()=>gameObject.SetActive(!gameObject.activeSelf)).OnComplete(() => TempData.ActivePage = 0);
    }
    public void Close()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).Play().OnComplete(() => TempData.ActivePage = 1);
    }
    public void CloseAndOpenWeaponPage()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).OnPlay(() => { WeaponPage.GetComponent<WeaponPage>().Close(); })
        .OnComplete(() => {gameObject.SetActive(!gameObject.activeSelf); WeaponPage.GetComponent<WeaponPage>().Open(); })
        .Play();
    }
    void Awake()
    {

        playerControls = new PlayerControls();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(null, new Vector2(Screen.width / 2, Screen.height / 2), CursorMode.Auto);
    }
    void OnEnable()
    {
        playerControls.Enable();
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
    void OnDisable()
    {
        playerControls.Disable();
    }

}
