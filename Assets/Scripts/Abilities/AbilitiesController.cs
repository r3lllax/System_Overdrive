using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesController : MonoBehaviour
{
    [SerializeField] public List<Ability> abilities;
    public Dictionary<KeyCode, Ability> abilityKeyMap;
    [SerializeField] private bool isPlayerOwner = true;
    private bool NeedChooseAbility = false;
    private Color32 disabledColor = new Color32(255, 255, 255, 34);
    private Color32 enabledColor = new Color32(255, 255, 255, 255);
    private Image AutoUseIndicator;
    private float AutoToggleReload = 0f;
    private float AutoUseReload = 0.5f;


    private bool UseEveryCD = false;
    private List<Ability> AvailableSpells;

    private bool isProcessing = false;

    private void Awake()
    {
        var AbilityInPlayerContoller = Instantiate(TempData.ChoosenCharacter.AbilityPrefab, transform);
        AbilityInPlayerContoller.GetComponent<Ability>().SetActiveTime(TempData.ChoosenCharacter.ActiveTime);
        AbilityInPlayerContoller.GetComponent<Ability>().SetCooldown(TempData.ChoosenCharacter.CooldownTime);
        AbilityInPlayerContoller.GetComponent<Ability>().SetCastTime(TempData.ChoosenCharacter.CastTime);

        abilities.Add(AbilityInPlayerContoller.GetComponent<Ability>());
        AbiilityPanel.NeedRefreshAbilityPanel = true;
        AutoUseIndicator = GameObject.FindWithTag("AutoUseIndicator").GetComponent<Image>();
        CalculateAbilities();
    }

    public void CalculateAbilities()
    {
        AvailableSpells = new List<Ability>();
        abilityKeyMap = new Dictionary<KeyCode, Ability>();
        foreach (Ability ability in abilities)
        {
            if (ability.GetComponent<Ability>().GetHotkey() != KeyCode.None)
            {
                abilityKeyMap[ability.GetComponent<Ability>().GetHotkey()] = ability;
            }

        }
    }

    public void UseRandomSpell()
    {
        NeedChooseAbility = true;
    }

    public bool CanUseSpell()
    {
        return AvailableSpells.Count > 0 ? true : false;
    }
    [ContextMenu("UseEveryCD")]
    public void ToggleAutoUse()
    {
        if (!isProcessing)
        {
            StartCoroutine(ToggleUseByCD());
        }
        
    }

    private IEnumerator ToggleUseByCD()
    {
        isProcessing = true;
        UseEveryCD = !UseEveryCD;
        yield return new WaitForSeconds(AutoUseReload);
        isProcessing = false;
    }

    private void Update()
    {
        if (isProcessing)
        {
            AutoToggleReload += Time.deltaTime;
            AutoUseIndicator.fillAmount = AutoToggleReload / AutoUseReload;
        }
        else
        {
            if (AutoToggleReload != 0f)
            {
                AutoToggleReload = 0f;
            }
        }
        if (Input.GetKey(KeyCode.Z))
        {
            ToggleAutoUse();
        }
        if (isPlayerOwner)
        {
            if (UseEveryCD)
            {

                AutoUseIndicator.color = enabledColor;
                foreach (var key in abilityKeyMap.Keys)
                {

                    abilityKeyMap[key].TryActivate();

                }
            }
            else
            {
                AutoUseIndicator.color = disabledColor;
                foreach (var key in abilityKeyMap.Keys)
                {

                    if (Input.GetKey(key))
                    {
                        abilityKeyMap[key].TryActivate();
                    }
                }
            }

        }
        else
        {
            if (NeedChooseAbility)
            {
                AvailableSpells.Clear();
                foreach (Ability spell in abilities)
                {
                    if (spell.GetReady())
                    {
                        AvailableSpells.Add(spell);
                    }
                }
                try
                {
                    Ability randomAbility = AvailableSpells[Random.Range(0, AvailableSpells.Count)];
                    randomAbility.TryActivate();
                }
                catch { }
                NeedChooseAbility = false;
            }

        }
    }
}
