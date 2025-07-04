using DG.Tweening;
using UnityEngine;



public class ProfileExists : MonoBehaviour
{
    [SerializeField] private Camera cam;
    void OnEnable()
    {
        SoundManager.PlaySound(SoundType.UI, 3, DataManager.CurrentUser != null ? DataManager.CurrentUser.Settings.EffectsVolume : 1);
        for (int i = 0; i < transform.childCount; i++)
        {
            Sequence sq = DOTween.Sequence();
            sq
            .Append(transform.DOScale(1f, 0.5f).From(0)).SetEase(Ease.InOutCubic).Play();
        }
    }
    void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Sequence sq = DOTween.Sequence();
            sq
            .Append(transform.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).Play();  
        }
    }
}
