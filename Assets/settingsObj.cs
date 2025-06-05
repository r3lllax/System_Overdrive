using DG.Tweening;
using UnityEngine;

public class settingsObj : MonoBehaviour
{

    void OnEnable()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(1f, 0.5f).From(0)).SetEase(Ease.InOutCubic).Play();        
    }

}
