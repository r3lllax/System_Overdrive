using System.Collections;
using UnityEngine;

public class Sandevistan : Ability
{
    private float PlayerSpeedBefore = 0;
    private float PlayerSpeedAfter = 0;
    private float PlayerCDBefore = 0;
    private float PlayerCDdAfter = 0;
    private float PlayerMLSBefore = 0;
    private float PlayerMLSAfter = 0;
    private GunWeapon weapon;

    void Start()
    {
        activeTime = cooldown / 2;
        try
        {
            weapon = GameObject.FindWithTag("ActiveWeapon").GetComponent<GunWeapon>();
        }
        catch{}
        
    }
    protected override void ExecuteAbility()
    {

        PlayerSpeedBefore = SessionData.MoveSpeed;
        PlayerSpeedAfter = PlayerSpeedBefore * 3;

        PlayerCDBefore = SessionData.CdBetweenFire;
        PlayerCDdAfter = PlayerCDBefore / 3;

        PlayerMLSBefore = SessionData.AttackSpeedMelee;
        PlayerMLSAfter = PlayerMLSBefore * 3;
        StartCoroutine(WaitEndOfAbility());
    }
    private IEnumerator WaitEndOfAbility()
    {
        Time.timeScale = 0.5f;
        if (weapon)
        {
            weapon.SetUnlimitedAmmo(true);
        }
        
        SessionData.SetValueFloat(ref SessionData.MoveSpeed, PlayerSpeedAfter);
        SessionData.SetValueFloat(ref SessionData.AttackSpeedMelee, PlayerMLSAfter);
        SessionData.SetValueFloat(ref SessionData.CdBetweenFire, PlayerCDdAfter);
        yield return new WaitForSeconds(activeTime);
        if (weapon)
        {
            weapon.SetUnlimitedAmmo(false);
        }

        float tempSpeed = SessionData.MoveSpeed - (PlayerSpeedAfter - PlayerSpeedBefore);
        float tempCD = SessionData.CdBetweenFire - (PlayerCDdAfter - PlayerCDBefore);
        float tempMLS = SessionData.AttackSpeedMelee - (PlayerMLSAfter - PlayerMLSBefore);

        SessionData.SetValueFloat(ref SessionData.MoveSpeed, tempSpeed);
        SessionData.SetValueFloat(ref SessionData.AttackSpeedMelee, tempMLS);
        SessionData.SetValueFloat(ref SessionData.CdBetweenFire, tempCD);
        GameObject.FindWithTag("Player").gameObject.GetComponentInChildren<PlayerController>().NotTakeSpeed = false;
        Time.timeScale = 1f;
    }
}
