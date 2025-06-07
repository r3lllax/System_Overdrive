using TMPro;
using UnityEngine;

public class UiUpgradePanel : MonoBehaviour
{
    private float timeBefore = 1;
    private GameObject UpgradeCounterUI;
    private LevelSystem playerLevelSystem;
    private GameObject Upgrades;
    private bool panelIsOpen = false;
    public bool inAnim = false;
    private bool aimStatus;
    
    public void DeleteCards()
    {
        foreach (Transform child in Upgrades.transform)
        {
            if (child != null && child.tag != "UpgradeCountUI")
            {
                Destroy(child.gameObject);
            }
        }
    }
    private void Awake()
    {
        Upgrades = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        UpgradeCounterUI = GameObject.FindWithTag("UpgradeCountUI");
    }
    private void Start()
    {
        playerLevelSystem = GameObject.FindWithTag("Player").transform.GetChild(0).gameObject.transform.GetComponentInChildren<LevelSystem>();

    }
    private void Update()
    {
        UpgradeCounterUI.GetComponent<TextMeshProUGUI>().text = playerLevelSystem.GetLevelUpsCount()>0?$"Доступно улучшений: {SessionData.ChooseUpgradesCount- UIUpgradeCard.CurrentChoosenUpgradesCount}":"";
    }
    public void CreateCard(Vector3 position, GameObject Card, Upgrade upgr)
    {
        var card = Instantiate(Card, Upgrades.transform);
        card.GetComponent<UIUpgradeCard>().SetCurrentUpgrade(upgr);
    }
    [ContextMenu("TogglePanel")]
    public void TogglePanel()
    {
        panelIsOpen = !panelIsOpen;
        GetComponent<Animator>().SetTrigger("toggle");
    }
    public void StopTime()
    {
        aimStatus = playerLevelSystem.transform.parent.GetChild(0).GetComponentInChildren<AutoAim>().AutoAimStatus;
        playerLevelSystem.transform.parent.GetChild(0).GetComponentInChildren<AutoAim>().AutoAimStatus = false;

        timeBefore = Time.timeScale;
        Time.timeScale = 0;
        playerLevelSystem.transform.parent.GetChild(0).GetComponentInChildren<AutoAim>().CheckVisual();
    }
    public void Knocback()
    {
        if (!panelIsOpen)
        {
            playerLevelSystem.transform.parent.gameObject.GetComponentInChildren<KnockBackWPlayerDamage>().KnockBackClosestEnemy(15f);
        }
    }
    public void StartTime()
    {
        if (SessionData.ChooseUpgradesCount == UIUpgradeCard.CurrentChoosenUpgradesCount)
        {
            UIUpgradeCard.CurrentChoosenUpgradesCount = 0;
        }
        playerLevelSystem.transform.parent.GetChild(0).GetComponentInChildren<AutoAim>().AutoAimStatus = aimStatus;
        Time.timeScale = timeBefore;
    }
    public void AnimStart()
    {
        inAnim = true;
        
    }
    public void AnimEnd(){
        inAnim=false;
    }
}
