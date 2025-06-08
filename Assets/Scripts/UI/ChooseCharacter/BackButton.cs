using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    [SerializeField] private GameObject weaponSelectPage;
    [SerializeField] private GameObject characterSelectPage;
    public void AddBackButton()
    {
        
    }
    public void Back()
    {
        if (TempData.CharIsPicked)
        {
            TempData.CharIsPicked = false;
            TempData.ChoosenWeapon = null;
            TempData.WeaponIsLocked = false;
            weaponSelectPage.GetComponent<WeaponPage>().Close();
            weaponSelectPage.GetComponent<WeaponPage>().CloseAndOpenCharacterPage();
            
        }
        else
        {
            TempData.ChoosenCharacter = null;
            TempData.ChoosenWeapon = null;
            TempData.CharacterIsLocked = false;
            TempData.WeaponIsLocked = false;
            SceneManager.LoadScene("MainMenuScene");
        }
        
    }
}
