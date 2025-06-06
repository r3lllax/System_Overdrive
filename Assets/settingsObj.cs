using DG.Tweening;
using UnityEngine;

public class settingsObj : MonoBehaviour
{
    public bool isOpen = false;
    public bool isProcessing = false;
    public bool MainMenu = true;
    void OnEnable()
    {
        if(!MainMenu){ return; }
        Open();
    }
    public void ToggleSettings()
    {
        if(isProcessing){ return; }
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
    public void Open()
    {
        
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(1f, 0.5f)
        .From(0)
        .OnPlay(() => isProcessing = true))
        .SetEase(Ease.InOutCubic)
        .Play()
        .OnComplete(()=>{ isProcessing = false; isOpen = true; })
        .SetUpdate(true);
    }
    public void Close()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transform.DOScale(0f, 0.5f)
        .From(1)
        .OnPlay(()=>isProcessing=true))
        .SetEase(Ease.InOutCubic)
        .Play()
        .OnComplete(()=>{ isProcessing = false; isOpen = false; })
        .SetUpdate(true);  
    }
}
