using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform Player;
    
    private enum State{
        Roaming,
        FollowPlayer
    }
    private State state;
    private EnemyPathfinder enemyPathfinder;
    public void SetPlayerOBJ(GameObject PlayerLink){
        Player = PlayerLink.GetComponent<Transform>();
    }

    private void Awake()
    {
        enemyPathfinder = GetComponent<EnemyPathfinder>();
        state = State.FollowPlayer;

    }
    [ContextMenu("Roaming")]
    public void SetRoaming(){
        StopCoroutine(RoamingRoutline());
        state = State.Roaming;
        StartCoroutine(RoamingRoutline());
    }
    [ContextMenu("FollowPlayer")]
    public void SetFollowPlayer(){
        StopCoroutine(RoamingRoutline());
        state = State.FollowPlayer;
        StartCoroutine(RoamingRoutline());
    }
    [ContextMenu("LogState")]
    public void LogState(){
        Debug.Log(state);
    }
    
    private int EnableCount = 1;
    private void Start()
    {
        StartCoroutine(RoamingRoutline());
    }

    // <!-Раскомментировать чтобы противник преследовал только в определенном радиусе-!>
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Debug.Log(collision.name);
    //     if(collision.name == "shadow"){
    //         SetFollowPlayer()
    //     }
    // }
    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if(collision.name == "shadow"){
    //         void SetRoaming()
    //     }
    // }
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
        if(state == State.Roaming){
            while (state == State.Roaming){
                Vector2 roamPosition = GetRoamingPosition();
                enemyPathfinder.MoveTo(roamPosition);
                yield return new WaitForSeconds(2f);
            }
        }
        else if(state == State.FollowPlayer){
            while (state == State.FollowPlayer){
                Vector2 playerDirection = GetPlayerDirection();
                Vector2 roamPosition = new Vector2(playerDirection.x,playerDirection.y);
                enemyPathfinder.MoveTo(roamPosition);
                yield return new WaitForSeconds(0.3f);
            }
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
