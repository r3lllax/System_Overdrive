using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject ProfileExistsBlock;
    [SerializeField] private GameObject NoProfileBlock;
    [SerializeField] private InputField Field;
    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(null, new Vector2(Screen.width/2,Screen.height/2), CursorMode.Auto);
        Render();
    }

    void OnEnable()
    {
        Field.text = "Default";
    }
    [ContextMenu("CreateUser")]
    public void CreateUser()
    {
        DataManager.CreateNewProfile(Field.text.Trim().Length < 1?"Default":Field.text);
        DataManager.LoadUserProfile();
        Render();
    }
    public void OpenChildById(int id)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (i == id)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }

    public void AnimateLogo()
    {
        Sequence sq = DOTween.Sequence();
    }

    public void OpenSettings(Transform transformObj)
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transformObj.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).Play().OnComplete(() => OpenChildById(1));
    }
    public void CloseSettings(Transform transformObj)
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transformObj.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).Play().OnComplete(() => OpenChildById(0));
    }
    public void OpenCreateUser(Transform transformObj)
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transformObj.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).Play().OnComplete(() => OpenChildById(2));
    }
    public void CloseCreateUser(Transform transformObj)
    {
        Sequence sq = DOTween.Sequence();
        sq
        .Append(transformObj.DOScale(0f, 0.5f).From(1)).SetEase(Ease.InOutCubic).Play().OnComplete(() => OpenChildById(0));
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    private void Render()
    {
        if (DataManager.SaveFileExists())
        {
            ProfileExistsBlock.SetActive(true);
            NoProfileBlock.SetActive(false);
            DataManager.LoadUserProfile();

        }
        else
        {
            ProfileExistsBlock.SetActive(false);
            NoProfileBlock.SetActive(true);
        }
    }
}
