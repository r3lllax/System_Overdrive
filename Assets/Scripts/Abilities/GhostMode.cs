using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GhostMode : Ability
{
    private SpriteRenderer sp;
    private CapsuleCollider2D PlayerColider;
    private Light2D playerLight;
    private Color32 oldColor = new Color32(255, 255, 255, 255);
    private Color32 newColor = new Color32(255, 255, 255, 60);
    private float PlayerSpeedBefore = 0;
    private float PlayerSpeedAfter = 0;
    private float AmmoPlusChance = 0.2f;
    void Start()
    {
        PlayerColider = owner.GetComponent<CapsuleCollider2D>();
        sp = owner.GetComponent<SpriteRenderer>();
        playerLight = owner.GetComponentInChildren<Light2D>();
    }
    protected override IEnumerator CastingRoutine()
    {
        isReady = false;
        sp.color = new Color32(127, 147, 255, 255);
        yield return new WaitForSeconds(castTime);

        ExecuteAbility();
        ActiveTimer = activeTime;
        ActiveNow = true;
        yield return new WaitForSeconds(activeTime);
        ActiveNow = false;
        StartCoroutine(CooldownRoutine());
        
    }
    protected override void ExecuteAbility()
    {
        if (UpgradesController.BulletType == "None")
        {
            if(Random.value <= AmmoPlusChance)
            {
                SessionData.AddValueV3(ref SessionData.MeleeSize,0.2f);
            }
        }
        else
        {
            if(Random.value <= AmmoPlusChance)
            {
                SessionData.AddValueInt(ref SessionData.MagazineCapacity, 1);
            }
        }
        PlayerSpeedBefore = SessionData.MoveSpeed;
        PlayerSpeedAfter = PlayerSpeedBefore * 1.5f;
        StartCoroutine(AbilityRoutine());

    }
    
    private IEnumerator AbilityRoutine()
    {
        DamageUI.Instance.AddText(1, owner.transform.position, "GhostMode");
        SessionData.SetValueFloat(ref SessionData.MoveSpeed, PlayerSpeedAfter);
        PlayerColider.enabled = false;
        sp.color = newColor;
        yield return new WaitForSeconds(activeTime / 3 * 2);
        for (int i = 0; i < 3; i++)
        {
            playerLight.enabled = false;
            yield return new WaitForSeconds(((activeTime / 3 * 1) / 3) / 2);
            playerLight.enabled = true;
            yield return new WaitForSeconds(((activeTime / 3 * 1) / 3) / 2);
        }
        SessionData.SetValueFloat(ref SessionData.MoveSpeed, PlayerSpeedBefore);
        sp.color = oldColor;
        PlayerColider.enabled = true;
    }
}
