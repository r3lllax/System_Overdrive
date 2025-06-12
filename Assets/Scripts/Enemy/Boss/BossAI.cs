using UnityEngine;

public class BossAI : MonoBehaviour
{
    private AbilitiesController SpellController;
    private CircleCollider2D Trigger;
    private int Health;
    private bool SpellWasGivenToPlayer = false;
    private GameObject RewardSpell;
    private StatisticWindow sw;
    private void Awake()
    {
        try
        {
            Health = GetComponent<Enemy>().GetHealth();
            Trigger = GetComponentInChildren<CircleCollider2D>();
            SpellController = GetComponentInChildren<AbilitiesController>();
            sw = GameObject.FindWithTag("Statistics").GetComponent<StatisticWindow>();
        }
        catch { }
    }
    [ContextMenu("UseRandomSpell")]
    public void UseRandomSpell()
    {
        SpellController.UseRandomSpell();
    }

    private void Update()
    {
        Health = GetComponent<Enemy>().GetHealth();
        if (Health <= 0 && !SpellWasGivenToPlayer)
        {
            PlayerWin();
        }
    }
    private void PlayerWin(){

        GiveRandomAbilityToPlayer();
        sw.Title("Победа");
    }
    private void GiveRandomAbilityToPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        AbilitiesController playerSpellsController = player.GetComponentInChildren<AbilitiesController>();
        GameObject BossSpellController = transform.GetChild(0).gameObject;
        int childsCount = BossSpellController.transform.childCount;
        RewardSpell = BossSpellController.transform.GetChild(Random.Range(0, childsCount)).gameObject;
        var AbilityInPlayerContoller = Instantiate(RewardSpell, playerSpellsController.transform);
        playerSpellsController.abilities.Add(AbilityInPlayerContoller.GetComponent<Ability>());
        playerSpellsController.CalculateAbilities();
        AbiilityPanel.NeedRefreshAbilityPanel = true;
        SpellWasGivenToPlayer = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "RadiusToKnockBack")
        {
            SpellController.UseRandomSpell();
        }

    }
    

}
