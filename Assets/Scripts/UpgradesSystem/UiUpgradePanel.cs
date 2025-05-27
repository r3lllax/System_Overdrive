using UnityEngine;

public class UiUpgradePanel : MonoBehaviour
{
    private float timeBefore = 1;
    public bool inAnim = false;
    [ContextMenu("DELETETEST")]
    public void DeleteCards(){
        foreach (Transform child in transform){
            if(child!=null){
                Destroy(child.gameObject);
            }
        }
    }
    public void CreateCard(Vector3 position,GameObject Card,Upgrade upgr){
        var card = Instantiate(Card,Vector3.zero,Quaternion.identity);
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
