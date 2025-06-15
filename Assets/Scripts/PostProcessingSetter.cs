using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PostProcessingSetter : MonoBehaviour
{
    [SerializeField]private Camera cam;
    void Update()
    {
        cam.GetUniversalAdditionalCameraData().renderPostProcessing = DataManager.CurrentUser.Settings.EnablePostProcessing;
    }
}
