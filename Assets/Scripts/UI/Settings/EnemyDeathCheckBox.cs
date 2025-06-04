using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDeathCheckBox : MonoBehaviour
{
    public TextMeshProUGUI label;
    private GameObject mark;

    void Awake()
    {
        mark = transform.GetChild(0).transform.GetChild(0).gameObject;
    }

    public void ChangeStatus()
    {
        DataManager.CurrentUser.Settings.EnableEnemyDeathEffect = !DataManager.CurrentUser.Settings.EnableEnemyDeathEffect;
        DataManager.SaveUserProfile();

    }
    void Update()
    {
        if (mark.activeSelf != DataManager.CurrentUser.Settings.EnableEnemyDeathEffect)
        {
            mark.SetActive(DataManager.CurrentUser.Settings.EnableEnemyDeathEffect);
        }
        if (DataManager.CurrentUser.Settings.EnableEnemyDeathEffect)
        {
            label.text = "Вкл";
        }
        else
        {
            label.text = "Выкл";
        }
    }

}
