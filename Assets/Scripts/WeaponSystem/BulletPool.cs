using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    public GameObject bulletPrefab;
    public int poolSize = 500;

    private Queue<GameObject> bullets = new Queue<GameObject>();
    

    void Awake()
    {
        Instance = this;
        bulletPrefab = TempData.ChoosenWeapon.BulletPrefab;
        UpgradesController.BulletType = bulletPrefab.name;
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullets.Enqueue(bullet);
        }
    }

    void Update()
    {
        Debug.Log($"BulletPoolSize - {bullets.Count}");
    }

    public GameObject GetBullet()
    {
        
        // if (bullets.Count > 0)
        // {
        //     Debug.Log("Пуля из пула");
        //     GameObject bullet = bullets.Dequeue();
        //     bullet.SetActive(true);
        //     return bullet;
        // }
        // else
        // {
            GameObject bullet = Instantiate(bulletPrefab);
            //bullets.Enqueue(bullet);
            bullet.SetActive(true);
            return bullet;
        // }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullets.Enqueue(bullet);
    }
}
