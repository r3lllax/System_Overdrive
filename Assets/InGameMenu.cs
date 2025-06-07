using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private bool isOpen = false;
    private bool isProcessing = false;
    private Vector2 outOfScreen;
    private RectTransform rectTransform;
    private float TimeBeforeOpen;
    [SerializeField] settingsObj settings;
    [SerializeField] GameObject backdrop;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        outOfScreen = new Vector2(rectTransform.sizeDelta.x, GetComponent<RectTransform>().anchoredPosition.y);
        rectTransform.anchoredPosition = outOfScreen;
    }

    [ContextMenu("Toggle")]
    public void TogglePanel()
    {
        if (isProcessing) { return; }
        if (isOpen)
        {
            if (settings.isOpen)
            {
                settings.ToggleSettings();
            }
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Open()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(rectTransform.DOAnchorPos(Vector2.zero, 1f)
        .SetEase(Ease.InOutBack)
        .OnPlay(() => {ShowBackDrop(); isProcessing = true;  TimeBeforeOpen = Time.timeScale; Time.timeScale = 0; }))
        .SetUpdate(true)
        .Play().OnComplete(() => { isProcessing = false; isOpen = true; });
    }
    public void Exit()
    {
        Time.timeScale = 1f;
        TempData.ChoosenCharacter = null;
        TempData.ChoosenWeapon = null;
        TempData.ExpPrefab = null;
        TempData.ActivePage = 0;
        TempData.CharacterIsLocked = false;
        TempData.WeaponIsLocked = false;
        TempData.updateUI = false;
        TempData.CharIsPicked = false;
        TempData.WeaponIsPicked = false;
        TempData.needRefreshData = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(null, new Vector2(Screen.width/2,Screen.height/2), CursorMode.Auto);
        TheRaceStatistics.Reset();
        SceneManager.LoadScene("ChooserCharacterScene");
    }
    [ContextMenu("Statistics")]
    public void GetStatistics()
    {
        TheRaceStatistics.GetScore();
    }
    public void Close()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(rectTransform.DOAnchorPos(outOfScreen, 1f)
        .SetEase(Ease.InOutBack)
        .OnPlay(() => { isProcessing = true; Time.timeScale = TimeBeforeOpen; }))
        .SetUpdate(true)
        .Play().OnComplete(() => { HideBackDrop(); isProcessing = false; isOpen = false; });
    }
    public void ShowBackDrop()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(backdrop.transform.DOScale(1f, 0)
        .From(0))
        .SetEase(Ease.InOutCubic)
        .Play()
        .SetUpdate(true);
    }
    public void HideBackDrop()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(backdrop.transform.DOScale(0f, 0)
        .From(1))
        .SetEase(Ease.InOutCubic)
        .Play()
        .SetUpdate(true);
    }

    void Update()
    {
        if (PlayerController.Instance.playerControls.UI.ToggleGameMenu.IsPressed())
        {
            TogglePanel();
        }
    }
}
