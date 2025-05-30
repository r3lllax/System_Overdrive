using UnityEngine;

public class AbiilityPanel : MonoBehaviour
{
    public static bool NeedRefreshAbilityPanel = true;
    private GameObject Player;
    private AbilitiesController PlayerAbilityController;
    [SerializeField] private GameObject AbilitySlotPrefab;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform.GetChild(0).gameObject;
        PlayerAbilityController = Player.GetComponentInChildren<AbilitiesController>();

    }
    private void Update()
    {
        if (NeedRefreshAbilityPanel)
        {
            ClearAbilityPanel();
            DrawAbilities();
            NeedRefreshAbilityPanel = false;
        }
    }
    private void ClearAbilityPanel()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
    private void DrawAbilities()
    {
        for (int i = 0; i < PlayerAbilityController.transform.childCount; i++)
        {
            var AbilitySlot = Instantiate(AbilitySlotPrefab, gameObject.transform);
            AbilitySlot.GetComponent<AbilitySlot>().SetAbility(PlayerAbilityController.transform.GetChild(i).GetComponent<Ability>());
        }
    }

}
