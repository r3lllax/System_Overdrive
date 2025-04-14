using Unity.Cinemachine;
using UnityEngine;

public class CinemachinePlayerSetter : MonoBehaviour
{
    [SerializeField] private CinemachineCamera Camera;
    void Start()
    {
        GameObject target = GameObject.FindWithTag("Player").transform.GetChild(0).gameObject;
        if(Camera != null)
        {
            Camera.LookAt = target.transform;
            Camera.Follow = target.transform;
        }
    }
}
