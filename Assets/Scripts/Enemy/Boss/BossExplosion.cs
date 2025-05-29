using UnityEngine;

public class BossExplosion : MonoBehaviour
{
    private float timer;
    private float lifetime = 3f;
    [SerializeField] private GameObject ExplosionDeadPrefab;
    private GameObject owner;
    private SpriteRenderer sp;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public void SetOnwer(GameObject newOwner)
    {
        owner = newOwner;
    }

    private void Update()
    {

        if (owner.tag == "Player")
        {
            sp.color = new Color32(0, 255, 0, 255);
        }

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Instantiate(ExplosionDeadPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && owner.tag != "Player")
        {
            collision.GetComponent<PlayerController>().isFreezed = true;
            collision.GetComponent<PlayerController>().SetMoveSpeed(0);
            collision.GetComponent<PlayerController>().NotTakeSpeed = true;
        }
        if (collision.tag == "Enemy" && owner.tag=="Player")
        {
            collision.GetComponent<EnemyPathfinder>().SetFreezed(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && owner.tag != "Player")
        {
            collision.GetComponent<PlayerController>().isFreezed = false;
            collision.GetComponent<PlayerController>().NotTakeSpeed = false;
        }
        if (collision.tag == "Enemy" && owner.tag=="Player")
        {
            collision.GetComponent<EnemyPathfinder>().SetFreezed(false);
        }
    }
}
