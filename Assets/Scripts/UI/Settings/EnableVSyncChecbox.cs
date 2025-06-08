using System;
using TMPro;
using UnityEngine;

public class EnableVSyncChecbox : MonoBehaviour
{
    public TextMeshProUGUI label;
    private GameObject mark;

    void Awake()
    {
        mark = transform.GetChild(0).transform.GetChild(0).gameObject;
    }

    public void ChangeStatus()
    {
        DataManager.CurrentUser.Settings.Vsync = Convert.ToInt32(!Convert.ToBoolean(DataManager.CurrentUser.Settings.Vsync));
        DataManager.SaveUserProfile();

    }
    void Update()
    {
        if (mark.activeSelf != Convert.ToBoolean(DataManager.CurrentUser.Settings.Vsync))
        {
            mark.SetActive( Convert.ToBoolean(DataManager.CurrentUser.Settings.Vsync));
        }
        if ( Convert.ToBoolean(DataManager.CurrentUser.Settings.Vsync))
        {
            label.text = "Вкл";
        }
        else
        {
            label.text = "Выкл";
        }
    }
}
