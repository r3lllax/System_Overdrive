using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    public Transform player;
    public float distanceFromPlayer;
    
    private Camera mainCamera;
    private SpriteRenderer sp;
    private Animator Anim;
    public static bool AnimEnd = true;
    [SerializeField]private float AnSpeed;

    private void Start() {
        mainCamera = Camera.main;
        sp = GetComponentInChildren<Transform>().GetChild(0).GetComponent<SpriteRenderer>();
        Anim = GetComponentInChildren<Transform>().GetChild(0).GetComponent<Animator>();
        AnSpeed = 1f;
    }

    private void Update() {
        Anim.speed = AnSpeed;
        
        if(AnimEnd){
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            

            Vector3 direction = mousePosition - player.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            transform.position = player.position + direction * distanceFromPlayer;
        }

        // if(mousePosition.x < GetComponent<Transform>().position.x){
        //     sp.flipY = true;
        // }
        // else{
        //     sp.flipY = false;
        // }

        
        if(Input.GetMouseButton(0) && AnimEnd){
            AnimEnd = false;
            Anim.SetBool("Flip",sp.flipY);
            Anim.SetBool("Attack",true);
            
        }
    }
    
}
