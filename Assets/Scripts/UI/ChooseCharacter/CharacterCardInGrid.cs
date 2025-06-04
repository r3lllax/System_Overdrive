using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCardInGrid : MonoBehaviour
{
    public GameCharacter character;
    public bool isLocked;
    private Image img;
    void Awake()
    {
        img = transform.GetChild(0).gameObject.GetComponent<Image>();
        
    }
    void Start()
    {
        img.sprite = character.CharacterPrefab.GetComponent<SpriteRenderer>().sprite;
    }
    [ContextMenu("PickChar")]
    public void SetChosenCharacter()
    {
        TempData.ChoosenCharacter = character;
        TempData.isLocked = isLocked;
        SessionData.Health = character.Health;
        SessionData.MoveSpeed = character.MoveSpeed;

    }
    [ContextMenu("LoadWeapon")]
    public void changeScene()
    {
        SceneManager.LoadScene("ChooseEquipmentScene");
    }
}
