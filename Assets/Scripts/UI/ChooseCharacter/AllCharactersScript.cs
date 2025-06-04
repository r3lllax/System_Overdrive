using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AllCharactersScropt : MonoBehaviour
{
    [SerializeField] private AllCharacters chars;
    [SerializeField] private GameObject characterCardPrefab;

    void Awake()
    {
        foreach (var character in chars.CharacterList)
        {
            GameObject card = Instantiate(characterCardPrefab, transform);
            card.GetComponent<CharacterCardInGrid>().character = character;
        }
    }
}
