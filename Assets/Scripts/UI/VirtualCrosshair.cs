using UnityEngine;
using UnityEngine.UI;

public class VirtualCrosshair : MonoBehaviour
{
    private Camera camera;

    void Awake()
    {
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        if (!GetComponent<Image>().enabled)
        {
            if (camera)
            {
                GetComponent<RectTransform>().position = Input.mousePosition;
            }
        }
    }
}
