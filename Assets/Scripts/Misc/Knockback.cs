using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool KnockedBack {get; private set;}
    [SerializeField] private float knockBackTime = 0.2f;
    private Rigidbody2D rb;
    public void Refresh(){
        rb.linearVelocity = Vector2.zero;
        KnockedBack = false;
        
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    } 
    public void GetKnockBack(Transform damageSource,float knockBackThrust){
        KnockedBack = true;
        Vector2 diff = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(diff,ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }
    
    private IEnumerator KnockRoutine(){
        yield return new WaitForSeconds(knockBackTime);
        rb.linearVelocity = Vector2.zero;
        KnockedBack = false;
    }
}
