using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillMagazineButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(1.01f, 0.2f).SetEase(Ease.InBack))
        .Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(1f, 0.1f).SetEase(Ease.OutBack))
        .Play();
    }

    
}
