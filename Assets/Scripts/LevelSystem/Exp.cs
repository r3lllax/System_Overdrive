using UnityEngine;

public class Exp : MonoBehaviour
{
    [SerializeField]private float ExpirienceCount;
    [SerializeField]private Transform target;
    private Vector3 pos;
    [SerializeField]private float speed;
    [SerializeField]public bool MoveToPlayerTransform = false;

    public float GetExpCount(){
        return ExpirienceCount;
    }
    public void SetExpCount(float Num){
        ExpirienceCount = Num;
    }
    private void Awake()
    {
        ExpirienceCount = Random.Range(0,5);
        speed = 15f;
        
    }
    void Update()
    {
        if(MoveToPlayerTransform){
            MoveToPlayer();
        }
    }
    private void Start()
    {
        target = PlayerController.Instance.transform;  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            ExpPool.Instance.ReturnExp(gameObject);
            LevelSystem.Instance.AddCurrentExp(ExpirienceCount);
            //Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            ExpPool.Instance.ReturnExp(gameObject);
            LevelSystem.Instance.AddCurrentExp(ExpirienceCount);
            //Destroy(gameObject);
        }
    }
    private void MoveToPlayer(){
        transform.position = Vector3.MoveTowards(transform.position,target.position, speed * Time.deltaTime);
    }
}
