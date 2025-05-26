using System.Collections.Generic;
using UnityEngine;

public class AbilitiesController : MonoBehaviour
{
   [SerializeField] private Ability[] abilities;
    private Dictionary<KeyCode, Ability> _abilityKeyMap;

    private void Awake()
    {
        _abilityKeyMap = new Dictionary<KeyCode, Ability>();
        foreach (Ability ability in abilities)
        {
            if (ability.GetComponent<Ability>().GetHotkey() != KeyCode.None)
            {
                _abilityKeyMap[ability.GetComponent<Ability>().GetHotkey()] = ability;
            }
        }
    }

    private void Update()
    {
        foreach (var key in _abilityKeyMap.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                _abilityKeyMap[key].TryActivate();
                break; 
            }
        }
    }
}
