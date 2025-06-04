using TMPro;
using UnityEngine;

public class ChooseCharacterButton : MonoBehaviour
{
    private ScenesController controller;
    void Awake()
    {
        controller = GetComponent<ScenesController>();
    }
    public void tryPickCharacter()
    {
        if (TempData.isLocked) { return; }
        controller.TryLoadWeaponScene();
    }
    void Update()
    {
        if (TempData.isLocked)
        {
            transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "Купить";
        }
        else
        {
            transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "Выбрать";
        }
    }
}
