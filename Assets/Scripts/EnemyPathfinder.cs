using UnityEngine;

public class EnemyPathfinder : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Enemy enemy;
    private Vector2 moveDir;
    private Knockback knockback;
    private EnemyAI enemyAI;
    private Transform playerTransform;

    private bool isFreezed = false;

    public void SetMoveSpeed(float Num) {
        moveSpeed = Num;
    }
    public void SetFreezed(bool newState)
    {
        isFreezed = newState;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
        enemy = GetComponent<Enemy>();
        enemyAI = GetComponent<EnemyAI>();

        if(PlayerController.Instance != null)
        {
            playerTransform = PlayerController.Instance.transform;
        }
    }
    private void FixedUpdate()
    {
        bool isRangedAttacking = enemyAI.GetState() == EnemyAI.State.RangedAttack;
        if (knockback.KnockedBack || isRangedAttacking || isFreezed || playerTransform == null) { return; }
        
        float speed = enemy.speed*SessionData.EnemySpeedMultiplier;
        transform.position = Vector2.MoveTowards(transform.position,PlayerController.Instance.transform.position, speed * Time.deltaTime);
    }
    public void MoveTo(Vector2 targetPos){
        moveDir = targetPos;
    }
}
