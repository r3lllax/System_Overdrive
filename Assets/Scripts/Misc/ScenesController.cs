
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenesController : MonoBehaviour
{
    public void LoadGameScene(){
        SceneManager.LoadScene("SampleScene");
    }
    public void TryLoadGameScene(){
        if(TempData.ChoosenCharacter == null ||TempData.ChoosenWeapon == null){
            StartCoroutine(BadChoice());
        }
        else{
            gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);
            LoadGameScene();
        }
    }
    private IEnumerator BadChoice(){
        gameObject.GetComponent<Image>().color = new Color32(255,129,129,255);
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);

    }
}
