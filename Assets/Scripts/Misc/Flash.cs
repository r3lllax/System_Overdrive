using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float refreshDefaultMatTime = 0.2f;

    private Material defaulMat;
    private SpriteRenderer sp;
    private Enemy enemy;


    public void Refresh(){
        StopCoroutine(FlashRoutine());
        sp.material = defaulMat;
    }   
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        sp = GetComponent<SpriteRenderer>();
        defaulMat = sp.material;
    }
    public IEnumerator FlashRoutine(){
        sp.material = whiteFlashMat;
        yield return new WaitForSeconds(refreshDefaultMatTime);
        sp.material = defaulMat;
        if(enemy!=null){
            enemy.CheckDeath();
        }
    }
}
