using System.Collections.Generic;
using UnityEngine;

public class LightningVFX : MonoBehaviour
{
    public static void Create(Vector3 start, Vector3 end,bool useCollider = false,GameObject owner = null)
    {
        GameObject lightning = new GameObject("LightningVFX");
        LineRenderer lr = lightning.AddComponent<LineRenderer>();
        if (useCollider)
        {
            // lr.startColor = new Color(Random.Range(0F,1F), Random.Range(0, 1F), Random.Range(0, 1F));
            // lr.endColor = new Color(Random.Range(0F,1F), Random.Range(0, 1F), Random.Range(0, 1F));
            
            List<Vector2> Points = new List<Vector2>() { start, end};
            EdgeCollider2D ec = lightning.AddComponent<EdgeCollider2D>();
            Malevolent_Shrine_line ml = lightning.AddComponent<Malevolent_Shrine_line>();
            if (owner)
            {
                ml.owner = owner;
                if (owner.tag == "Player")
                {
                    lightning.layer = 11;
                    lr.startColor = new Color32(0, 0, 0, 1);
                    lr.endColor = new Color32(0, 0, 0, 1);
                }
                else
                {
                    lightning.layer = 14;
                    lr.startColor = new Color32(0, 0, 255, 1);
                    lr.endColor = new Color32(0, 0, 255, 1);
                }
                if (owner.tag == "Player" && Random.Range(0, SessionData.ProcenteScaleMax + 1) <= SessionData.CritChance)
                {
                    ml.Type = "Crit";
                    lr.startColor = new Color32(255, 0, 0, 1);
                    lr.endColor = new Color32(255, 0, 0, 1);
                }
                 
                
            }

            
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
