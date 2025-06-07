using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatisticWindow : MonoBehaviour
{
    [SerializeField] private GameObject Backdrop;
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject StatusText;
    [SerializeField] private GameObject ContinueBtn;
    private EndGameStats panelStat;


    private float TimeBefore;
    private Vector2 outOfScreen;

    void Awake()
    {
        panelStat = Panel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<EndGameStats>();
        outOfScreen = new Vector2(0, -Screen.height * 2);
        Panel.GetComponent<RectTransform>().anchoredPosition = outOfScreen;
    }

    [ContextMenu("OpenPanel")]
    public void OpenPanel()
    {
        if (SessionData.Health > 0)
        {
            ContinueBtn.SetActive(true);
        }
        else
        {
            ContinueBtn.SetActive(false);
        }
        ;
        Sequence sq = DOTween.Sequence();
        sq
        .Append(Panel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 1).From(outOfScreen))
        .OnPlay(() => { panelStat.GetStatistics(); Backdrop.SetActive(true); })
        .SetUpdate(true)
        .Play();
    }
    [ContextMenu("ClosePanel")]
    public void ClosePanel()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(Panel.GetComponent<RectTransform>().DOAnchorPos(outOfScreen, 1).From(Vector2.zero))
        .OnPlay(() => { Backdrop.SetActive(false); Time.timeScale = TimeBefore; Time.timeScale = 1; })
        .SetUpdate(true)
        .Play();
    }
    [ContextMenu("Title")]
    public void TestTitle()
    {
        Title("Победа");
    }
    public void Title(string text = "Default")
    {
        var temp = text == "Победа" ? StatusText.GetComponent<TextMeshProUGUI>().color = new Color32(0, 255, 0, 255) : StatusText.GetComponent<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
        StatusText.GetComponent<TextMeshProUGUI>().text = text;
        Sequence sq = DOTween.Sequence();
        sq
        .Append(StatusText.transform.DOScale(1f, 1f).From(0f))
        .OnPlay(() => { TimeBefore = Time.timeScale; Time.timeScale = 0; })
        .Join(StatusText.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360))
        .AppendInterval(1f)
        .Append(StatusText.transform.DOScale(0f, 1f).From(1f))
        .Join(StatusText.transform.DORotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360))
        .Play()
        .SetUpdate(true)
        .OnComplete(() => { OpenPanel(); });
    }
    public void Continue()
    {
        ClosePanel();
    }
    public void PlayAgain()
    {
        panelStat.AddMoneyToPlayer();
        SetChosenWeaponAndDefaultData();
        DataManager.SaveUserProfile();
        Time.timeScale = 1f;
        SessionData.Reset();
        TempData.ActivePage = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(null, new Vector2(Screen.width / 2, Screen.height / 2), CursorMode.Auto);
        TheRaceStatistics.Reset();
        SceneManager.LoadScene("SampleScene");
    }
    public void Exit()
    {
        panelStat.AddMoneyToPlayer();
        DataManager.SaveUserProfile();
        Reset();
        SceneManager.LoadScene("ChooserCharacterScene");
    }
    public void SetChosenWeaponAndDefaultData(){
        SessionData.Health = TempData.ChoosenCharacter.Health;
        SessionData.MoveSpeed = TempData.ChoosenCharacter.MoveSpeed;
        SessionData.Damage = TempData.ChoosenWeapon.Damage;
        SessionData.AttackSpeedMelee = TempData.ChoosenWeapon.AnimationSpeed;
        SessionData.CdBetweenFire = TempData.ChoosenWeapon.GunAttackSpeed;
        SessionData.CdBetweenMagazine = TempData.ChoosenWeapon.GunMagazineReloadTime;
        SessionData.SprintMultiplier = 1.5f;
        SessionData.StartSpeedMultiplier = TempData.ChoosenWeapon.PlayerSpeedMultiplier;
        SessionData.MagazineCapacity = TempData.ChoosenWeapon.GunMagazineSize;
        SessionData.OneShootChance = 0;
        SessionData.CritChance = 0;
        SessionData.BulletSpeed = TempData.ChoosenWeapon.GunBulletSpeed;
        SessionData.BulletLifeTime = TempData.ChoosenWeapon.GunBulletLifeTime;
        SessionData.MeleeSize = TempData.ChoosenWeapon.WeaponPrefab.transform.localScale;
        SessionData.BulletSize = new Vector3(1,1,1);
        foreach (EthernalUpgrade upg in DataManager.CurrentUser.EthernalUpdates)
        {
            UpgradesController.DefineAndApplyEthernalUpgrade(upg,upg.Count*upg.AddStrength);
        }
    }
    public void Reset()
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
        SessionData.Reset();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(null, new Vector2(Screen.width / 2, Screen.height / 2), CursorMode.Auto);
        TheRaceStatistics.Reset();

    }

}
