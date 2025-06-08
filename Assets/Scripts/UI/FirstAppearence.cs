using System.Diagnostics.Tracing;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FirstAppearence : MonoBehaviour
{
    [SerializeField] private GameObject Logo;
    [SerializeField] private GameObject Light;
    [SerializeField] private GameObject Menu;
    private float intensity = 0;
    public void Appearence()
    {
        LogoAnimation();
        //Включение света
        //Появление меню
    }
    void Start()
    {
        if (TempData.FirstAppearence)
        {
            Appearence();
        }
        else
        {
            RectTransform rectTransform = Logo.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, 60);
            Light.GetComponent<Light2D>().intensity = 1f;
            Menu.SetActive(true);
        }
        
    }
    public void LogoAnimation()
    {
        RectTransform rectTransform = Logo.GetComponent<RectTransform>();
        Sequence sq = DOTween.Sequence();
        sq
        .Append(rectTransform.DOAnchorPos(Vector2.zero, 2f)
        .SetEase(Ease.InOutBack))
        .Append(rectTransform.DOScale(1.7f, 1.7f))
        .Append(rectTransform.DOScale(1, 0.5f))
        .Join(rectTransform.DOAnchorPos(new Vector2(0, 60), 0.5f))
        .SetUpdate(true)
        .Play()
        .OnComplete(() =>
        {
            LightAnimation();
        });
    }
    public void LightAnimation()
    {
        Sequence sq = DOTween.Sequence();
        Light2D light = Light.GetComponent<Light2D>();
        DOTween.To(() => intensity, x => intensity = x, 1, 1f)
            .OnUpdate(() =>
            {
                Light.GetComponent<Light2D>().intensity = intensity;
            })
            .Play()
            .OnComplete(() =>
            {
                Menu.SetActive(true);
                TempData.FirstAppearence = false;
            });
    }

}
