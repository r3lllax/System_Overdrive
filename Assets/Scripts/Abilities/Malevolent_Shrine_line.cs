using System;
using UnityEngine;

public class Malevolent_Shrine_line : MonoBehaviour
{
    public GameObject owner;
    public string Type = "Default";
    private float Damage = 0;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!owner)
        {
            return;
        }
        Debug.Log(owner.tag);
        if (collision.gameObject.layer == 7 && owner.gameObject.tag == "Player")
        {
            Damage = ((float)SessionData.Damage * 0.1f) == 0 ? 1 : (int)((float)SessionData.Damage * 0.1f);
            if (Type == "Crit")
            {
                collision.GetComponent<Enemy>().TakeDamage((int)(Damage * (SessionData.CritScale / 2)) <= 0 ? 1 : (int)(Damage * (SessionData.CritScale / 2)), 1, "crit");

            }
            else
            {
                collision.GetComponent<Enemy>().TakeDamage((int)((float)SessionData.Damage * 0.1f) == 0 ? 1 : (int)((float)SessionData.Damage * 0.1f), 1);
            }
        }
        else if(collision.gameObject.layer == 6 && owner.gameObject.tag == "Boss")
        {
            try
            {
                collision.GetComponent<Player>().TryTakeDamage(1);
            }
            catch
            {
                
            }
            
        }
    }
    
   
}
