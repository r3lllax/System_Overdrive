using UnityEngine;

public class FollowGun : MonoBehaviour
{
    [SerializeField]public Weapon currentWeapon;
    public Transform player;
    [SerializeField]private float distanceFromPlayer;
    
    private Camera mainCamera;
    private SpriteRenderer sp;
    public float Reload;
    private GunWeapon Gun;
    private GameObject PlayerCursor;

    private void Start() {
        mainCamera = Camera.main;
        mainCamera.gameObject.AddComponent<BulletPool>();
        player = PlayerController.Instance.transform;
        sp = GetComponentInChildren<Transform>().GetChild(0).GetComponent<SpriteRenderer>();
        distanceFromPlayer = currentWeapon.distanceFromPlayer;
        // Instantiate(currentWeapon.AttackParticles,GetComponentInChildren<Transform>().GetChild(0).transform.position,Quaternion.identity);
        PlayerCursor = AutoAim.PlayerCursor;
    }
    private void Awake()
    {

        Gun = transform.GetChild(0).GetComponent<GunWeapon>();
        
    }

    private void Update()
    {
        Reload = Gun.ReloadTime;
        if (Time.timeScale == 0) { return; }
        Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(PlayerCursor.GetComponent<RectTransform>().position);
        if (cursorPosition.x < player.position.x)
        {
            sp.flipY = true;
        }
        else
        {
            sp.flipY = false;
        }
        cursorPosition.z = 0f;
        
        Vector3 direction = cursorPosition - player.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.position = player.position + direction * distanceFromPlayer;

        if (PlayerController.Instance.playerControls.Battle.Attack.IsPressed()  && Reload <= 0)
        {
            Gun.TryFire();

        }
        if (PlayerController.Instance.playerControls.Battle.Reload.IsPressed())
        {
            Gun.ForcedReload();
        }
    }
}
