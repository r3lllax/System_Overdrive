
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenesController : MonoBehaviour
{   
    
    public void LoadGameScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void TryLoadWeaponScene(string name = "ChooseEquipmentScene"){
        if(TempData.ChoosenCharacter == null ){
            StartCoroutine(BadChoice());
        }
        else{
            gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);
            LoadGameScene(name);
        }
    }
    public void TryLoadGameScene(string name = "SampleScene")
    {
        if (TempData.ChoosenCharacter == null || TempData.ChoosenWeapon == null)
        {
            StartCoroutine(BadChoice());
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            LoadGameScene(name);
        }
    }
    private IEnumerator BadChoice(){
        gameObject.GetComponent<Image>().color = new Color32(255,129,129,255);
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);

    }
}
