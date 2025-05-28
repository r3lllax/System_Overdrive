using UnityEngine;

public class BossExplosion : MonoBehaviour
{
    private float timer;
    private float lifetime = 1.5f;
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
}
