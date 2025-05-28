using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public static PlayerController Instance;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Transform transform;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public bool NotTakeSpeed =false;
    public bool isFreezed = false;
    public void SetPlayerMSWithMultiplier(float speed)
    {
        this.moveSpeed *= speed;
    }
    private void Awake()
    {
        Instance = this;
        transform = GetComponent<Transform>();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void Start(){
        moveSpeed = GetComponent<Player>().GetMoveSpeed();
    }
    private void Update()
    {
       
        if(NotTakeSpeed!=true){
            moveSpeed = GetComponent<Player>().GetMoveSpeed();
        }
        
        
        //Переделать через инпут систем
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            if(isFreezed){ return; }
            NotTakeSpeed = true;
            moveSpeed*=GetComponent<Player>().GetSprintMultiplier();
            transform.GetChild(2).gameObject.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            if(isFreezed){ return; }
            if (moveSpeed != 0)
            {
                moveSpeed /= GetComponent<Player>().GetSprintMultiplier();
            }
            NotTakeSpeed = false;
            transform.GetChild(2).gameObject.SetActive(false);
        }
        PlayerInput();
    }
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
    private void FixedUpdate()
    {
        PlayerFaceToDirection();
        Move();
    }

    private void PlayerInput(){
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        animator.SetFloat("moveX",movement.x);
        animator.SetFloat("moveY",movement.y);
    }
    private void Move(){
        rb.MovePosition(rb.position + movement *(moveSpeed * Time.fixedDeltaTime));
    }
    private void PlayerFaceToDirection(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if(mousePos.x<playerScreenPoint.x){
            spriteRenderer.flipX = true;
        }
        else{
            spriteRenderer.flipX = false;
        }
    }
    
}
