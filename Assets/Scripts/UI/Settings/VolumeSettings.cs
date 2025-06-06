using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public string Param = "";

    void Start()
    {
        try
        {
            if (Param == "MusicVolume")
            {
                GetComponent<Slider>().value = DataManager.CurrentUser.Settings.MusicVolume;
            }
            else
            {
                GetComponent<Slider>().value = DataManager.CurrentUser.Settings.EffectsVolume;
            }
        }
        catch{}
        

        
    }

    public void OnChange()
    {
        try
        {
            if (Param == "MusicVolume")
            {
                DataManager.CurrentUser.Settings.MusicVolume = GetComponent<Slider>().value;
            }
            else
            {
                DataManager.CurrentUser.Settings.EffectsVolume = GetComponent<Slider>().value;
            }
            DataManager.SaveUserProfile();
        }
        catch{}

    }
    
}
