using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject ProfileExistsBlock;
    [SerializeField] private GameObject NoProfileBlock;
    void Awake()
    {
        Render();
    }
    [ContextMenu("CreateUser")]
    public void CreateUser()
    {
        DataManager.CreateNewProfile();
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
