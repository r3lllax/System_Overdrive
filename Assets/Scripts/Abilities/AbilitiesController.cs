using System.Collections.Generic;
using UnityEngine;

public class AbilitiesController : MonoBehaviour
{
   [SerializeField] private Ability[] abilities;
    private Dictionary<KeyCode, Ability> abilityKeyMap;

    private void Awake()
    {
        abilityKeyMap = new Dictionary<KeyCode, Ability>();
        foreach (Ability ability in abilities)
        {
            if (ability.GetComponent<Ability>().GetHotkey() != KeyCode.None)
            {
                abilityKeyMap[ability.GetComponent<Ability>().GetHotkey()] = ability;
            }

        }
    

    }

    private void Update()
    {
        foreach (var key in abilityKeyMap.Keys)
        {
             
            if (Input.GetKey(key))
            {
                abilityKeyMap[key].TryActivate();
            }
        }
    }
}
