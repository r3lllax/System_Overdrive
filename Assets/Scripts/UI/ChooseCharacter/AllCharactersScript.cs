using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class AllCharactersScropt : MonoBehaviour
{
    [SerializeField] private AllCharacters chars;
    [SerializeField] private GameObject characterCardPrefab;

    void Awake()
    {
        DataManager.LoadUserProfile();
        foreach (var character in chars.CharacterList)
        {
            Debug.Log(character.name);

            GameObject card = Instantiate(characterCardPrefab, transform);
            if (DataManager.CurrentUser.UnlockedCharacters.Contains(character.name))
            {
                card.GetComponent<CharacterCardInGrid>().isLocked = false;
            }
            else
            {
                card.GetComponent<CharacterCardInGrid>().isLocked = true;
            }
            card.GetComponent<CharacterCardInGrid>().character = character;
        }
    }
}
