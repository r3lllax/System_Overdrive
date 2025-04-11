using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExpFinder : MonoBehaviour
{
    [SerializeField] private float FinderRadius;
    //Убрать
    [SerializeField] private GameObject ExpPrefab;
    private CircleCollider2D collider;

    private Light2D Light;
    private LevelSystem LevelSystem;

    public void SetFinderRadius(float Num){
        FinderRadius = Num;
    }

    private void Awake()
    {
        TempData.ExpPrefab = ExpPrefab;
        LevelSystem = GetComponent<LevelSystem>();
        collider = GetComponent<CircleCollider2D>();
        Light = GetComponent<Light2D>();
        FinderRadius = 3;
    }

    private void Update()
    {
        collider.radius = FinderRadius;
        Light.pointLightOuterRadius = FinderRadius;
        Light.pointLightInnerRadius = FinderRadius>0?FinderRadius/2:0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Expirience"){
            // GainExpirience(collision.GetComponent<Exp>().GetExpCount());
            collision.GetComponent<Exp>().MoveToPlayerTransform = true;
            
        }
    }
    private void GainExpirience(float Num){
        LevelSystem.AddCurrentExp(Num);
    }
}
