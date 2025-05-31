using TMPro;
using UnityEngine;

public class UiUpgradePanel : MonoBehaviour
{
    private float timeBefore = 1;
    private GameObject UpgradeCounterUI;
    private LevelSystem playerLevelSystem;
    private GameObject Upgrades;
    public bool inAnim = false;
    [ContextMenu("DELETETEST")]
    public void DeleteCards(){
        foreach (Transform child in Upgrades.transform){
            if(child!=null && child.tag!="UpgradeCountUI"){
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
        UpgradeCounterUI.GetComponent<TextMeshProUGUI>().text = playerLevelSystem.GetLevelUpsCount()>0?$"Доступно улучшений: {playerLevelSystem.GetLevelUpsCount()}":"";
    }
    public void CreateCard(Vector3 position, GameObject Card, Upgrade upgr)
    {
        var card = Instantiate(Card, Upgrades.transform);
        card.GetComponent<UIUpgradeCard>().SetCurrentUpgrade(upgr);
    }
    [ContextMenu("TogglePanel")]
    public void TogglePanel(){
        GetComponent<Animator>().SetTrigger("toggle");
    }
    public void StopTime(){
        timeBefore = Time.timeScale;
        Time.timeScale = 0;
    }
    public void StartTime(){
        Time.timeScale = timeBefore;
    }
    public void AnimStart(){
        inAnim=true;
    }
    public void AnimEnd(){
        inAnim=false;
    }
}
