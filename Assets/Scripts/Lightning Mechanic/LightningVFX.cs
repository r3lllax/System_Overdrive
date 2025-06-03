using System.Collections.Generic;
using UnityEngine;

public class LightningVFX : MonoBehaviour
{
    public static void Create(Vector3 start, Vector3 end,bool useCollider = false)
    {
        GameObject lightning = new GameObject("LightningVFX");
        LineRenderer lr = lightning.AddComponent<LineRenderer>();
        if (useCollider)
        {
            lightning.layer = 11;
            List<Vector2> Points = new List<Vector2>() { start, end};
            EdgeCollider2D ec = lightning.AddComponent<EdgeCollider2D>();
            Malevolent_Shrine_line ml = lightning.AddComponent<Malevolent_Shrine_line>();
            ec.isTrigger = true;
            ec.SetPoints(Points);
        }
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startWidth = 0.1f;
        lr.endWidth = 0.05f;
        lr.material = Resources.Load<Material>("Materials/WhiteFlash");
        Destroy(lightning, 0.2f);
    }
}
