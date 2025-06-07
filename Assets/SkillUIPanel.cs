using DG.Tweening;
using UnityEngine;

public class SkillUIPanel : MonoBehaviour
{
    public bool isOpen = false;
    private bool isProcessing = false; 
    private RectTransform rectTransform;
    private Vector2 outOfScreen;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        outOfScreen = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, Screen.height * 3);
        rectTransform.anchoredPosition = outOfScreen;
    }
    [ContextMenu("Toggle")]
    public void TogglePanel()
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
    private void Open()
    {
        Sequence sq = DOTween.Sequence();
        
        sq.Append(rectTransform.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.InOutBack).OnPlay(() => { isProcessing = true; }))
        .Play().OnComplete(() => { isProcessing = false; isOpen = true; });
    }
    private void Close()
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(rectTransform.DOAnchorPos(outOfScreen, 1f).SetEase(Ease.InOutBack).OnPlay(() => { isProcessing = true; }))
        .Play().OnComplete(() => { isProcessing = false; isOpen = false; });
    }
}
