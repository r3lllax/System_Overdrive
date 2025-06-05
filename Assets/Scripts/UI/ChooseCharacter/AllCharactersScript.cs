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
        LoadCharacters();

    }
    public void LoadCharacters()
    {
        for (int i =0;i<transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        foreach (var character in chars.CharacterList)
        {
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
    void Update()
    {
        if (TempData.needRefreshData)
        {
            LoadCharacters();
        }
    }
}
