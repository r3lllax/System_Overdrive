using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    [SerializeField]public Weapon currentWeapon;
    public Transform player;
    [SerializeField]private float distanceFromPlayer;
    
    private Camera mainCamera;
    private SpriteRenderer sp;
    private Animator Anim;
    private GameObject PlayerCursor;
    public static bool AnimEnd = true;

    void OnEnable()
    {
        AnimEnd = true;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        player = PlayerController.Instance.transform;
        sp = GetComponentInChildren<Transform>().GetChild(0).GetComponent<SpriteRenderer>();
        Anim = GetComponentInChildren<Transform>().GetChild(0).GetComponent<Animator>();
        distanceFromPlayer = currentWeapon.distanceFromPlayer;
        Instantiate(currentWeapon.AttackParticles, GetComponentInChildren<Transform>().GetChild(0).transform.position, Quaternion.identity);
        PlayerCursor = AutoAim.PlayerCursor;

    }
    private void Awake()
    {
        currentWeapon = TempData.ChoosenWeapon;

    }

    private void Update() {
        
        gameObject.transform.localScale = SessionData.MeleeSize;
        
        if(AnimEnd && Time.timeScale>0){
            Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(PlayerCursor.GetComponent<RectTransform>().position);
            cursorPosition.z = 0f;

            Vector3 direction = cursorPosition - player.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            transform.position = player.position + direction * distanceFromPlayer;
        }

        if(PlayerController.Instance.playerControls.Battle.Attack.IsPressed() && AnimEnd && Time.timeScale>0){
            AnimEnd = false;
            Anim.SetBool("Attack",true);
            
            
        }
    }
    
}
