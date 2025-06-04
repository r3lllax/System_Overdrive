using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Characters", menuName = "ScriptableObjects/AllCharacters")]
public class AllCharacters : ScriptableObject
{
    public List<GameCharacter> CharacterList = new List<GameCharacter>();
}
