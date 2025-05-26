using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected float cooldown = 0f;
    [SerializeField] protected float castTime = 0f;
    [SerializeField] protected KeyCode hotkey = KeyCode.None;

    protected bool isReady = true;
    protected GameObject owner;

    private void Awake()
    {
        //Скорее всего придется искать через родителя или на подобии
        owner = gameObject;
    }

    public KeyCode GetHotkey()
    {
        return hotkey;
    }
     public bool TryActivate()
    {
        if (!isReady || !CanActivate()) return false;

        StartCoroutine(CastingRoutine());
        return true;
    }

    protected virtual IEnumerator CastingRoutine()
    {
        isReady = false;
        yield return new WaitForSeconds(castTime);
        
        ExecuteAbility();
        StartCoroutine(CooldownRoutine());
    }

    protected abstract void ExecuteAbility();
    
    protected virtual bool CanActivate() => true;
    
    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }

    // Для босса
    public virtual bool ShouldAIUse() => Random.value > 0.5f; 
}
