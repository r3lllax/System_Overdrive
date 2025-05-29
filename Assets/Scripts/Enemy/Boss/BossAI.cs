using UnityEngine;

public class BossAI : MonoBehaviour
{
    private AbilitiesController SpellController;
    private CircleCollider2D Trigger;
    private int Health;
    private bool SpellWasGivenToPlayer = false;
    [SerializeField]private GameObject RewardSpell;

    private void Awake()
    {
        try
        {
            Health = GetComponent<Enemy>().GetHealth();
            Trigger = GetComponentInChildren<CircleCollider2D>();
            SpellController = GetComponentInChildren<AbilitiesController>();
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
            GameObject player = GameObject.FindWithTag("Player");
            AbilitiesController playerSpellsController = player.GetComponentInChildren<AbilitiesController>();
            var AbilityInPlayerContoller = Instantiate(RewardSpell, playerSpellsController.transform);
            playerSpellsController.abilities.Add(AbilityInPlayerContoller.GetComponent<Ability>());
            playerSpellsController.CalculateAbilities();
            Debug.Log("Игрок получает способность босса");
            SpellWasGivenToPlayer = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "RadiusToKnockBack")
        {
            SpellController.UseRandomSpell();
            // if (SpellController.CanUseSpell())
            // {

            // }

        }

    }
    

}
