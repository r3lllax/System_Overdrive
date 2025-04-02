using UnityEngine;

public class EnemyPathfinder : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDir;

    public void SetMoveSpeed(float Num){
        moveSpeed = Num;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position+moveDir*(moveSpeed*Time.fixedDeltaTime));
    }
    public void MoveTo(Vector2 targetPos){
        moveDir = targetPos;
    }
}
