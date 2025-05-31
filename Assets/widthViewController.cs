using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class widthViewController : MonoBehaviour
{
    private int currentChildCount = 0;
    private bool needResize = false;
    
    private void Update()
    {
        if (currentChildCount != transform.childCount)
        {
            needResize = true;
            currentChildCount = transform.childCount;
        }
        if (transform.childCount > 4)
        {
            if (needResize)
            {
                float offset = 0f;
                for (int i = 0; i < transform.childCount - 4; i++)
                {
                    offset += 130f;
                }
                Vector2 rt = GetComponent<RectTransform>().offsetMax;
                rt.x = offset;
                GetComponent<RectTransform>().offsetMax = rt;
                needResize = false;
            }

        }
        
    }
}
