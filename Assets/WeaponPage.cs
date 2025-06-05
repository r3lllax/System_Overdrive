using DG.Tweening;
using UnityEngine;

public class WeaponPage : MonoBehaviour
{
    [SerializeField] private GameObject CharacterPage;
    public void Open()
    {
        GetComponentInChildren<CurrentWeaponCard>().RenderCurrentWeapon();
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(1f, 0.5f).From(0)).SetEase(Ease.InOutCubic).Play();
    }
    public void Close()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).Play();
    }
    public void CloseAndOpenCharacterPage()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).OnComplete(()=>CharacterPage.GetComponent<CharacterPage>().Open())
        .Play();
    }
}
