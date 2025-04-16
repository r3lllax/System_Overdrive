using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/UpgradeMenu")]
public class AllUpgrades : ScriptableObject
{
    public List<Upgrade> UpgradesList = new List<Upgrade>();
}
