using UnityEngine;

public class BossAbility2 : Ability
{
    void Awake()
    {
        PlayerIsOwner = false;
    }
    protected override void ExecuteAbility()
    {
        Debug.Log($"Босс использует способность {gameObject.name}");
    }
}
