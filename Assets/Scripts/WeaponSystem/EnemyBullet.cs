using System;
using System.Collections;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private float speed=1;
    public bool CanBeMove = false;
    public GameObject DestroyPrefab;
    private Vector2 targetPos;
    private void Start()
    {
        targetPos.y-=0.5f;
        MaxBypassesCount = 0;
        BypassesCount = MaxBypassesCount;
    }

    public void SetTargetPos(Vector2 Target){
        targetPos = Target;
    }


    public void SetSpeed(float speedNew){
        speed = speedNew;
    }

    private void OnTriggerEnter2D(Collider2D other){
        DamageRegTrigger(other);
    }

    public override void IncreaseBypassCount(int Num){
        MaxBypassesCount+=Num;
    }
    public override void DecreaseBypassCount(int Num){
        MaxBypassesCount-=Num;
    }

    private void MoveToPlayer(){
        transform.position = Vector3.MoveTowards(transform.position,targetPos, speed * Time.deltaTime);
    }
    void Update()
    {
        if(!CanBeMove){return;}
        MoveToPlayer();
    }

    private void OnDisable()
    {
        try{
            StopAllCoroutines();
        }
        catch(Exception){
            Debug.Log("ошибка в при стопе куротины");
        }
    }
    public override void DamageRegTrigger(Collider2D collision)
    {
        if (collision.gameObject.layer == targetLayerNum && collision.gameObject.tag == "Player")
        {
            if (BypassesCount > 0)
            {
                if (collision.gameObject.GetComponent<Player>().TryTakeDamage(damage))
                {
                    BypassesCount--;
                }
                
            }
            else
            {
                if (collision.gameObject.GetComponent<Player>().TryTakeDamage(damage))
                {
                    Destroy(gameObject);
                }
                
            }
        }
        if ((collision.gameObject.tag == "ActiveWeapon") && SessionData.CanDestroyEnemyBullet)
        {
            ForceDestroy();
        }
        else if ((collision.gameObject.layer == 11 && collision.gameObject.tag != "EnemuBullet")  && SessionData.CanDestroyEnemyBullet)
        {
            ForceDestroy();
        }


    }
    public void ForceDestroy()
    {
        Destroy(gameObject);
        Instantiate(DestroyPrefab, transform.position, Quaternion.identity);
    }

    public IEnumerator ReturnBulletAfterTimeEnemyBullet(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
