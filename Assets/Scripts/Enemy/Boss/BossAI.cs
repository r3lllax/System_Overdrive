using UnityEngine;

public class BossAI : MonoBehaviour
{
    private AbilitiesController SpellController;
    private CircleCollider2D Trigger;

    private void Awake()
    {
        try
        {
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
