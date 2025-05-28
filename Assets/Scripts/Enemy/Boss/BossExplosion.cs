using UnityEngine;

public class BossExplosion : MonoBehaviour
{
    private float timer;
    private float lifetime = 3f;
    [SerializeField] private GameObject ExplosionDeadPrefab;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Instantiate(ExplosionDeadPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Explosion - {collision.tag}");
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().isFreezed = true;
            collision.GetComponent<PlayerController>().SetMoveSpeed(0);
            collision.GetComponent<PlayerController>().NotTakeSpeed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().isFreezed = false;
            collision.GetComponent<PlayerController>().NotTakeSpeed = false;
        }
    }
}
