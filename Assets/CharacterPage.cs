using DG.Tweening;
using UnityEngine;

public class CharacterPage : MonoBehaviour
{
    [SerializeField] private GameObject WeaponPage;
    public void Open()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(1f, 0.5f).From(0)).SetEase(Ease.InOutCubic).Play().OnComplete(()=>TempData.ActivePage=0);
    }
    public void Close()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).Play().OnComplete(()=>TempData.ActivePage=1);
    }
    public void CloseAndOpenWeaponPage()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).OnPlay(() => WeaponPage.GetComponent<WeaponPage>().Close())
        .OnComplete(() => WeaponPage.GetComponent<WeaponPage>().Open())
        .Play();
    }
    
}
