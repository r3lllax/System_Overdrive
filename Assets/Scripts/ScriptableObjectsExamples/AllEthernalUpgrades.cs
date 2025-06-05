using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AllEthernalUpgrades", menuName = "ScriptableObjects/AllEthernalUpgrades")]

public class AllEthernalUpgrades : ScriptableObject
{
    public List<EthernalUpgrade> EthernalUpgrades = new List<EthernalUpgrade>();
}
