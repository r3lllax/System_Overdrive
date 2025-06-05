using System.Collections.Generic;
using UnityEngine;

public class SkillsPanel : MonoBehaviour
{
    private List<EthernalUpgrade> ethernalUpgrades;
    [SerializeField] private GameObject SkillCardPrefab;
    void Awake()
    {
        DataManager.LoadUserProfile();

        ethernalUpgrades = DataManager.CurrentUser.EthernalUpdates;
        Debug.Log(ethernalUpgrades.Count);
        DrawUpgrades();
    }
    private void DrawUpgrades()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < ethernalUpgrades.Count; i++)
        {
            GameObject card = Instantiate(SkillCardPrefab, transform);
            card.GetComponent<SkillItemUI>().currentUpgrade = ethernalUpgrades[i];
            card.GetComponent<SkillItemUI>().SetData();
        }
        CalculateHeight(transform.parent.gameObject.GetComponent<RectTransform>());
    }
    public void CalculateHeight(RectTransform rectTransform)
    {
        rectTransform.sizeDelta = new Vector2(0,225 * ethernalUpgrades.Count);
    }
}
