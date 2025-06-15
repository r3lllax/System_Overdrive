using TMPro;
using UnityEngine;

public class PostProcessingCheckbox : MonoBehaviour
{
    public TextMeshProUGUI label;
    private GameObject mark;

    void Awake()
    {
        mark = transform.GetChild(0).transform.GetChild(0).gameObject;
    }

    public void ChangeStatus()
    {
        DataManager.CurrentUser.Settings.EnablePostProcessing = !DataManager.CurrentUser.Settings.EnablePostProcessing;
        DataManager.SaveUserProfile();

    }
    void Update()
    {
        if (mark.activeSelf != DataManager.CurrentUser.Settings.EnablePostProcessing)
        {
            mark.SetActive(DataManager.CurrentUser.Settings.EnablePostProcessing);
        }
        if (DataManager.CurrentUser.Settings.EnablePostProcessing)
        {
            label.text = "Вкл";
        }
        else
        {
            label.text = "Выкл";
        }
    }
}
