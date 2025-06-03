using System;
using UnityEngine;

public class Malevolent_Shrine_line : MonoBehaviour
{
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.GetComponent<Enemy>().TakeDamage((int)((float)SessionData.Damage*0.1f)==0?1:(int)((float)SessionData.Damage*0.1f), 1);
        }
    }
    
   
}
