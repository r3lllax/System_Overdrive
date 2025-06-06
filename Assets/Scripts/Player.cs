using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Обращаемся к классу TEMPDATA и в переменную SpeedMultiply получаем множитель скорости с оружием
    private Weapon Data;
    [SerializeField] private GameObject DamagePrefab;
    [SerializeField]private int Health;
    private float MoveSpeed;
    private float sprintMultiplier;
    private float PlayerSpeedMultiplier;
    private bool isBlockingNow = false;
    private bool isBlockingProcessing = false;
    private GameObject HPUI;
    [SerializeField] private GameObject HPUIprefab;
    [SerializeField] private GameObject LifeStealHPUIprefab;
    [SerializeField] private GameObject EvadeEffect;

    private bool DrawUIHPFlag = true;
    private bool LifeStealDrawed = false;
    private void SetPlayerMSWithMultiplier(float speed)
    {
        this.MoveSpeed *= speed;
    }

    private void UpdateData(){

        if (Health != SessionData.Health)
        {
            Health = SessionData.Health;
            DrawUIHPFlag = true;
        }
        sprintMultiplier = SessionData.SprintMultiplier;
        MoveSpeed = SessionData.MoveSpeed;
        MoveSpeed = MoveSpeed<0?0:MoveSpeed;
        PlayerSpeedMultiplier = SessionData.StartSpeedMultiplier;
        SetPlayerMSWithMultiplier(PlayerSpeedMultiplier);
    }
    public float GetSprintMultiplier(){
        return sprintMultiplier;
    }
    private void Awake()
    {
        HPUI = GameObject.FindWithTag("HPUI");
        UpdateData();
    }

    private IEnumerator blockingRoutine()
    {
        if (isBlockingProcessing)
        {
            yield return null;
        }
        isBlockingProcessing = true;
        isBlockingNow = true;
        yield return new WaitForSeconds(0.2f);
        isBlockingNow = false;
        isBlockingProcessing = false;
    }

    private void Update()
    {
        if (PlayerController.Instance.playerControls.Battle.Block.IsPressed())
        {
            if (!isBlockingProcessing)
            {
                StartCoroutine(blockingRoutine());
            }
        }
        if (SessionData.CanLifeSteal && LifeStealDrawed == false)
        {
            drawUIHP();
            LifeStealDrawed = true;
        }
        UpdateData();
        if (DrawUIHPFlag)
        {
            drawUIHP();
            DrawUIHPFlag = false;
        }

        // SessionData.NeedRefresh = false;


    }
    private void drawUIHP()
    {
        for (int i = 0; i < HPUI.transform.childCount; i++)
        {
            Destroy(HPUI.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < Health; i++)
        {
            Instantiate(HPUIprefab, HPUI.transform);
        }
        if (SessionData.CanLifeSteal)
        {
            Instantiate(LifeStealHPUIprefab, HPUI.transform);
        }
    }
    private void Start()
    {
        //GetComponent<PlayerController>().SetPlayerMSWithMultiplier(PlayerSpeedMultiplier);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 14)
        {
            TryTakeDamage(1);
        }
    }

    public bool TryTakeDamage(int Damage, bool knock = true)
    {
        if (Random.value <= SessionData.ScaleValueToProcente(SessionData.DamageEvadeChance)/100)
        {
            Instantiate(EvadeEffect, transform.position,Quaternion.identity);
            return false;
        }
        TakeDamage(Damage, knock);
        return true;
    }
    public bool TryShash(int Damage, bool knock = true)
    {
        if (Random.value <= SessionData.ScaleValueToProcente(SessionData.DamageEvadeChance)/100)
        {
            Instantiate(EvadeEffect, transform.position,Quaternion.identity);
            return false;
        }
        if (isBlockingNow)
        {
            DamageUI.Instance.AddText(1, transform.position,"Evade");
            Instantiate(EvadeEffect, transform.position,Quaternion.identity);
            return false;
        }
        TakeDamage(Damage, knock);
        return true;
    }

    private void TakeDamage(int Damage, bool knock = true)
    {
        SessionData.Health = Health - Damage < 0 ? 0 : Health - Damage;
        GetComponent<CinemachineImpulseSource>().GenerateImpulse(1);
        Instantiate(DamagePrefab, transform.position, Quaternion.identity);
        StartCoroutine(DamageRoutine());
        if (knock)
        {
            try
            {
                transform.GetChild(transform.childCount - 1).GetComponent<KnockBackWPlayerDamage>().KnockBackClosestEnemy(30f);
            }
            catch
            {
                transform.GetChild(transform.childCount - 2).GetComponent<KnockBackWPlayerDamage>().KnockBackClosestEnemy(30f);
            }

        }
        if (CheckDeath())
        {
            Death();
        }
    }
    private IEnumerator DamageRoutine(){
        GetComponent<SpriteRenderer>().color = new Color32(255,0,0,255);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
    }
    private bool CheckDeath(){
        return Health>0?false:true;
    }
    public void Death(){
        ///Парткиклы, выключение управления, скрипт запуска экрана результата
        
    }
    public float GetMoveSpeed(){
        return MoveSpeed;
    }
    public void SetMoveSpeed(float MS){
        this.MoveSpeed = MS;
    }
}
