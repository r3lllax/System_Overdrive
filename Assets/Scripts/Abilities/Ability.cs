using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected float cooldown = 0f;
    [SerializeField] protected Sprite Image;
    [SerializeField] protected float activeTime = 0f;
    [SerializeField] protected float castTime = 0f;
    [SerializeField]protected float currentCooldown = 0f;
    protected bool ActiveNow = false;
    protected float ActiveTimer = 0f;
    protected bool PlayerIsOwner = true;
    [SerializeField] protected KeyCode hotkey = KeyCode.None;

    protected bool Reload = false;

    protected bool isReady = true;
    protected GameObject owner;

    private void Awake()
    {
        //Скорее всего придется искать через родителя или на подобии
        owner = transform.parent.transform.parent.gameObject;
        if (PlayerIsOwner)
        {
            SessionData.SetValueFloat(ref SessionData.AbilityCooldown, cooldown);
            SessionData.SetValueFloat(ref SessionData.AbilityActiveTime, activeTime);
        }
        
    }
    private void Start()
    {
        try
        {
            owner = transform.parent.transform.parent.gameObject;
        }
        catch{}
    }

    private void Update()
    {

        if (PlayerIsOwner)
        {
            cooldown = SessionData.AbilityCooldown;
            activeTime = SessionData.AbilityActiveTime;
        }
        if (ActiveNow)
        {
            ActiveTimer -= Time.deltaTime;
        }
        else
        {
            ActiveTimer = 0f;
        }
        if (Reload)
        {
            currentCooldown -= Time.deltaTime;
        }
        else
        {
            currentCooldown = 0f;
        }

    }

    public KeyCode GetHotkey()
    {
        return hotkey;
    }
    public Sprite GetSprite()
    {
        return Image;
    }
    public float GetCooldown()
    {
        return cooldown;
    }
    public float GetCurrentCooldown()
    {
        return currentCooldown;
    }
    public bool GetReady()
    {
        return isReady;
    }
    public bool GetActive()
    {
        return ActiveNow;
    }
    public float GetActiveTimer()
    {
        return ActiveTimer;
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
        ActiveTimer = activeTime;
        ActiveNow = true;
        yield return new WaitForSeconds(activeTime);
        ActiveNow = false;
        StartCoroutine(CooldownRoutine());
    }

    protected abstract void ExecuteAbility();
    
    protected virtual bool CanActivate() => true;
    
    protected virtual IEnumerator CooldownRoutine()
    {
        currentCooldown = cooldown;
        Reload = true;
        yield return new WaitForSeconds(cooldown);
        Reload = false;
        isReady = true;
    }

    

    // Для босса
    public virtual bool ShouldAIUse() => Random.value > 0.5f; 
}
