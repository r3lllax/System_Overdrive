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

    private void Start() {
        mainCamera = Camera.main;
        mainCamera.gameObject.AddComponent<BulletPool>();
        player = PlayerController.Instance.transform;
        sp = GetComponentInChildren<Transform>().GetChild(0).GetComponent<SpriteRenderer>();
        distanceFromPlayer = currentWeapon.distanceFromPlayer;
        // Instantiate(currentWeapon.AttackParticles,GetComponentInChildren<Transform>().GetChild(0).transform.position,Quaternion.identity);

    }
    private void Awake()
    {
        
        Gun = transform.GetChild(0).GetComponent<GunWeapon>();
    }

    private void Update()
    {
        Reload = Gun.ReloadTime;
        if (Time.timeScale == 0) { return; }
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x < player.position.x)
        {
            sp.flipY = true;
        }
        else
        {
            sp.flipY = false;
        }
        mousePosition.z = 0f;
        
        Vector3 direction = mousePosition - player.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.position = player.position + direction * distanceFromPlayer;

        //Переделать через инпут систем(Перенести в плеер контроллер)
        if (Input.GetMouseButton(0) && Reload <= 0)
        {
            //Тут вызов функции в которой проверяется количество патрон в магазине, все кд, и только после создания снаряда спавнится эффект
            Gun.TryFire();

        }
        if (Input.GetKey(KeyCode.R))
        {
            Gun.ForcedReload();
        }
    }
}
