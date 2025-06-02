using UnityEngine;
using UnityEngine.UI;


public class LifeStealHeartUI : MonoBehaviour
{
    private Image image;
    void Awake()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        if (SessionData.CanLifeSteal)
        {
            image.fillAmount = SessionData.LifeStealCurrentValue / 1f;
        }
    }
}
