using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public int LevelToLoad;
    public Slider loadingSlider;
    public GameObject loadingScreen;
    public GameObject fadeScreen;

    void OnEnable()
    {
        FadeOutAnimation();
    }

    public void StartLoadScene()
    {
        FadeInAnimation();
    }
    private void FadeInAnimation()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(fadeScreen.transform.GetChild(0).GetComponent<Image>().DOFade(1f, 1f).From(0f))
        .OnComplete(() => { loadingScreen.SetActive(true);  StartCoroutine(LoadingSlider()); })
        .Play();
    }
    private void FadeOutAnimation()
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(fadeScreen.transform.GetChild(0).GetComponent<Image>().DOFade(0, 1f).From(0f))
        .Play();
    }
    private IEnumerator LoadingSlider()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(LevelToLoad);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }
}
