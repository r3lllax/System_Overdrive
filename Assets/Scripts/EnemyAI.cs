using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
    [SerializeField] private GameObject BulletPrefab;
    public Transform Player;
    
    public enum State{
        Roaming,
        FollowPlayer,
        RangedAttack
    }
    public State GetState(){
        return state;
    }
    [SerializeField]private State state;
    private float attackTimer =2f;
    private bool canAttack = true;
    private Enemy enemyComponent;
    private EnemyPathfinder enemyPathfinder;
    public void SetPlayerOBJ(GameObject PlayerLink){
        Player = PlayerLink.GetComponent<Transform>();
    }

    private void Awake()
    {
        enemyPathfinder = GetComponent<EnemyPathfinder>();
        state = State.FollowPlayer;
        enemyComponent = GetComponent<Enemy>();
    }
    [ContextMenu("Roaming")]
    public void SetRoaming(){
        if(!gameObject.activeInHierarchy){return;}
        StopCoroutine(RoamingRoutline());
        state = State.Roaming;
        StartCoroutine(RoamingRoutline());
    }
    [ContextMenu("FollowPlayer")]
    public void SetFollowPlayer(){
        if(!gameObject.activeInHierarchy){return;}
        StopCoroutine(RoamingRoutline());
        state = State.FollowPlayer;
        StartCoroutine(RoamingRoutline());
    }
    [ContextMenu("LogState")]
    public void LogState(){
        Debug.Log(state);
    }
    public void SetRangedAttack(){
        if(!gameObject.activeInHierarchy){return;}
        StopCoroutine(RoamingRoutline());
        state = State.RangedAttack;
        StartCoroutine(RoamingRoutline());
    }
    private int EnableCount = 1;
    private void Start()
    {
        StartCoroutine(RoamingRoutline());
    }


    void OnEnable()
    {
        if(EnableCount >1){
            SetFollowPlayer();
        }
        else{
            EnableCount++;
        }
        
    }
    void OnDisable()
    {
        StopCoroutine(RoamingRoutline());
    }
    private IEnumerator RoamingRoutline(){
        switch(state){
            case State.Roaming:
                while (state == State.Roaming){
                    Vector2 roamPosition = GetRoamingPosition();
                    enemyPathfinder.MoveTo(roamPosition);
                    yield return new WaitForSeconds(2f);
                }
                break;
            case State.FollowPlayer:
                while (state == State.FollowPlayer){
                    Vector2 playerDirection = GetPlayerDirection();
                    Vector2 roamPosition = new Vector2(playerDirection.x,playerDirection.y);
                    enemyPathfinder.MoveTo(roamPosition);
                    yield return new WaitForSeconds(0.3f);
                }
                break;
            case State.RangedAttack:
                while (state == State.RangedAttack){

                    GetComponent<EnemyPathfinder>().SetMoveSpeed(0f);
                    if(canAttack){
                        RangedAttack(1,5,3);
                        canAttack=false;
                    }
                    yield return new WaitForSeconds(0.5f);
                    GetComponent<EnemyPathfinder>().SetMoveSpeed(2f);
                }
                break;  
        }
    }

    private void Update()
    {
        if(!canAttack && attackTimer>0){
            attackTimer-=Time.deltaTime;
        }
        else if(!canAttack && attackTimer <=0){
            attackTimer = 3f;
            canAttack=true;
        }
        
    }
    private Vector2 calculateBulletTargetPos(Vector2 position){
        if(position.x>=0){
            position.x*=-2;
        }
        else if(position.x<0){
            position.x*=2;
        }
        if(position.y>=0){
            position.y*=-2;
        }
        else if(position.y<0){
            position.y*=2;
        }
        return position;
    }
    private void RangedAttack(int Damage,float BulletLifeTime,float BulletSpeed){
        GameObject bullet = Instantiate(BulletPrefab);
        bullet.GetComponent<Bullet>().SetDamage(Damage);
        bullet.GetComponent<Bullet>().SetTargetLayer(6);
        bullet.GetComponent<Bullet>().SetBulletLifeTime(BulletLifeTime);
        Vector2 pos = transform.position;
        pos.y += 0.1f;
        bullet.transform.position = pos;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<EnemyBullet>().CanBeMove = true;
        bullet.GetComponent<EnemyBullet>().SetTargetPos(PlayerController.Instance.transform.position);
        bullet.GetComponent<EnemyBullet>().SetSpeed(BulletSpeed);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        // rb.linearVelocity = GetPlayerDirection() * BulletSpeed;
        bullet.GetComponent<EnemyBullet>().StartCoroutine(bullet.GetComponent<EnemyBullet>().ReturnBulletAfterTimeEnemyBullet(bullet,BulletLifeTime));

    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemyComponent.GetEnemyType() != "Ranged"){return;}

        if(collision.name == "shadow"){
            SetRangedAttack();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(enemyComponent.GetEnemyType() != "Ranged"){return;}
        if(collision.name == "shadow"){
            SetFollowPlayer();
        }
    }

    private Vector2 GetPlayerDirection(){
        Vector2 ourPos = new Vector2(GetComponent<Transform>().position.x,GetComponent<Transform>().position.y);
        return new Vector2(Player.position.x-ourPos.x<0?-1:1,Player.position.y-ourPos.y<0?-1:1);
    }

    private Vector2 GetRoamingPosition(){
        return new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;
    }
}
