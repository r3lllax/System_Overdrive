using UnityEngine;

public class LightningVFX : MonoBehaviour
{
    public static void Create(Vector3 start, Vector3 end)
    {
        GameObject lightning = new GameObject("LightningVFX");
        LineRenderer lr = lightning.AddComponent<LineRenderer>();

        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startWidth = 0.1f;
        lr.endWidth = 0.05f;
        lr.material = Resources.Load<Material>("Materials/WhiteFlash");
        Destroy(lightning, 0.2f);
    }
}
