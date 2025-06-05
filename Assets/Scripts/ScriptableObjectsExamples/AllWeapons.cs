using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AllWeapons", menuName = "ScriptableObjects/WeaponsList")]

public class AllWeapons : ScriptableObject
{
    public List<Weapon> WeaponsList = new List<Weapon>();
}
