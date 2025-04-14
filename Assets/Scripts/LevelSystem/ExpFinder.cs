using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExpFinder : MonoBehaviour
{
    [SerializeField] private float FinderRadius;
    //Убрать
    [SerializeField] private GameObject ExpPrefab;
    private CircleCollider2D ObjCollider;

    private Light2D Light;

    public void SetFinderRadius(float Num){
        FinderRadius = Num;
    }

    private void Awake()
    {
        
        ObjCollider = GetComponent<CircleCollider2D>();
        Light = GetComponent<Light2D>();
        FinderRadius = SessionData.ExpFinderRadius;
    }

    private void Update()
    {
        FinderRadius = SessionData.ExpFinderRadius;
        ObjCollider.radius = FinderRadius;
        Light.pointLightInnerRadius = FinderRadius;
        Light.pointLightOuterRadius = FinderRadius*2;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Expirience"){
            collision.GetComponent<Exp>().MoveToPlayerTransform = true;
            
        }
    }
   
}
