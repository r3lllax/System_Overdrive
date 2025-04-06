using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            moveSpeed*=1.5f;
            transform.GetChild(2).gameObject.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            if(moveSpeed!=0){
                moveSpeed/=1.5f;
            }
            transform.GetChild(2).gameObject.SetActive(false);
        }
        PlayerInput();
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
