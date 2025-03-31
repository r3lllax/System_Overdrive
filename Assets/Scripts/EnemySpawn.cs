using UnityEngine;
using System.Linq;

public class EnemySpawn : MonoBehaviour
{
     private new Camera camera;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject VirusPrefab;
    
    [SerializeField] private float spawnDistance = 10f;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Start()
    {
        for (short i = 0; i < 4; i++)
        {
            SpawnVirus(PointOutOfScreen());
        }
    }

    private Vector3 PointOutOfScreen()
    {
        float X = Random.Range(-0.5f, 1.5f);
        float Y = Random.Range(-0.5f, 1.5f);
        if (Random.value > 0.5f) X = X < 0.5f ? -0.5f : 1.5f;
        else Y = Y < 0.5f ? -0.5f : 1.5f;
        return new Vector3(X, Y, spawnDistance);
    }

    private void SpawnVirus(Vector3 viewportPoint)
    {
    
        Vector3 posInWorldSpace = camera.ViewportToWorldPoint(viewportPoint);
        var newVirus = Instantiate(VirusPrefab, posInWorldSpace, Quaternion.identity);
        newVirus.GetComponent<EnemyAI>().Player = Player.transform;
    }
    
}
