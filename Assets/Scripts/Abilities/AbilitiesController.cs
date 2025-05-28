using System.Collections.Generic;
using UnityEngine;

public class AbilitiesController : MonoBehaviour
{
   [SerializeField] private Ability[] abilities;
    private Dictionary<KeyCode, Ability> abilityKeyMap;
    [SerializeField]private bool isPlayerOwner = true;
    private bool NeedChooseAbility = false;
    private List<Ability> AvailableSpells;


    private void Awake()
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

    private void Update()
    {
        if (isPlayerOwner)
        {
            foreach (var key in abilityKeyMap.Keys)
            {

                if (Input.GetKey(key))
                {
                    abilityKeyMap[key].TryActivate();
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
