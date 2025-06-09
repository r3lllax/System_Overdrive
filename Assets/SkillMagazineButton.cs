using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillMagazineButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(1.01f, 0.2f).SetEase(Ease.InBack).OnPlay(()=>SoundManager.PlaySound(SoundType.UI,2, DataManager.CurrentUser != null ? DataManager.CurrentUser.Settings.EffectsVolume/2 : 1)))
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
