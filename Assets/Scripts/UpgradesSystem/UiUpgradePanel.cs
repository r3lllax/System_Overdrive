using TMPro;
using UnityEngine;

public class UiUpgradePanel : MonoBehaviour
{
    private float timeBefore = 1;
    private GameObject UpgradeCounterUI;
    private LevelSystem playerLevelSystem;
    public bool inAnim = false;
    [ContextMenu("DELETETEST")]
    public void DeleteCards(){
        foreach (Transform child in transform){
            if(child!=null && child.tag!="UpgradeCountUI"){
                Destroy(child.gameObject);
            }
        }
    }
    private void Awake()
    {
        UpgradeCounterUI = GameObject.FindWithTag("UpgradeCountUI");
    }
    private void Start()
    {
        playerLevelSystem = GameObject.FindWithTag("Player").transform.GetChild(0).gameObject.transform.GetComponentInChildren<LevelSystem>();

    }
    private void Update()
    {
        UpgradeCounterUI.GetComponent<TextMeshProUGUI>().text = playerLevelSystem.GetLevelUpsCount()>1?$"Улучшений:{playerLevelSystem.GetLevelUpsCount()}":"";
    }
    public void CreateCard(Vector3 position, GameObject Card, Upgrade upgr)
    {
        var card = Instantiate(Card, Vector3.zero, Quaternion.identity);
        card.transform.SetParent(transform);
        card.transform.localPosition = position;
        card.transform.localScale = Vector3.one;
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
