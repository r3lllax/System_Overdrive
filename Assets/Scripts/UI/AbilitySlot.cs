using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    private Ability myAbility;
    [SerializeField] private GameObject cdImage;
    [SerializeField] private GameObject abilitySprite;
    [SerializeField] private GameObject buttonText;
    [SerializeField] private GameObject cdText;
    [SerializeField] private GameObject notReadyPanel;
    private float fillAmount = 0f;
    private Image cdImg;

    private TextMeshProUGUI Button;

    private TextMeshProUGUI cd;

    private void Start()
    {
        cdImg = cdImage.GetComponent<Image>();
        Button = buttonText.GetComponent<TextMeshProUGUI>();
        cd = cdText.GetComponent<TextMeshProUGUI>();
    }

    public void SetAbility(Ability ability)
    {
        myAbility = ability;
    }
    private void Update()
    {
        if (myAbility)
        {
            abilitySprite.GetComponent<Image>().sprite = myAbility.GetSprite();
            if (myAbility.GetReady())
            {
                notReadyPanel.SetActive(false);
            }
            else
            {
                notReadyPanel.SetActive(true);
            }
            fillAmount = myAbility.GetCurrentCooldown() / myAbility.GetCooldown();
            cdImg.fillAmount = fillAmount;
            Button.text = KeyCodeToChar.KeyCodeToCharCalc(myAbility.GetHotkey()).ToString();
            cd.text = ((int)myAbility.GetCurrentCooldown()).ToString();

            if (fillAmount == 0f)
            {
                cd.text = "";
            }
            if (myAbility.GetActive())
            {
                if ((int)myAbility.GetActiveTimer() + 1 > 0)
                {
                    cd.text = ((int)myAbility.GetActiveTimer() + 1).ToString();
                }
                else
                {
                    cd.text = "";
                }

            }
            if (myAbility.GetActive())
            {
                cd.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                cd.color = new Color32(255, 0, 0, 255);
            }
            
            
        }
    }
}
