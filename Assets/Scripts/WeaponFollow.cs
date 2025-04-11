using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    [SerializeField]public Weapon currentWeapon;
    public Transform player;
    [SerializeField]private float distanceFromPlayer;
    
    private Camera mainCamera;
    private SpriteRenderer sp;
    private Animator Anim;
    public static bool AnimEnd = true;

    private void Start() {
        mainCamera = Camera.main;
        player = PlayerController.Instance.transform;
        sp = GetComponentInChildren<Transform>().GetChild(0).GetComponent<SpriteRenderer>();
        Anim = GetComponentInChildren<Transform>().GetChild(0).GetComponent<Animator>();
        distanceFromPlayer = currentWeapon.distanceFromPlayer;
        Instantiate(currentWeapon.AttackParticles,GetComponentInChildren<Transform>().GetChild(0).transform.position,Quaternion.identity);

    }
    private void Awake()
    {
        currentWeapon = TempData.ChoosenWeapon;

    }

    private void Update() {
        if(AnimEnd){
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector3 direction = mousePosition - player.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            transform.position = player.position + direction * distanceFromPlayer;
        }

        //Переделать через инпут систем(Перенести в плеер контроллер)
        if(Input.GetMouseButton(0) && AnimEnd){
            AnimEnd = false;
            Anim.SetBool("Attack",true);
            
        }
    }
    
}
