using UnityEngine;

public class EnemyPathfinder : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Enemy enemy;
    private Vector2 moveDir;
    private Knockback knockback;

    public void SetMoveSpeed(float Num){
        moveSpeed = Num;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
        enemy = GetComponent<Enemy>();
    }
    private void FixedUpdate()
    {
        if(knockback.KnockedBack || GetComponent<EnemyAI>().GetState()=="RangedAttack"){return;}
        //rb.MovePosition(rb.position+moveDir*(moveSpeed*Time.fixedDeltaTime));
        
        float speed = enemy.speed*SessionData.EnemySpeedMultiplier;
        transform.position = Vector2.MoveTowards(transform.position,PlayerController.Instance.transform.position, speed * Time.deltaTime);
    }
    public void MoveTo(Vector2 targetPos){
        moveDir = targetPos;
    }
}
