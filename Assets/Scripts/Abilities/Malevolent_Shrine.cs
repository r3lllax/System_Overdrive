using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class Malevolent_Shrine : Ability
{
    private Vector3 start;
    private Vector3 end;
    private CinemachineCamera cam;
    private float targetOrt = 12f;
    private float transitionSpeed = 1f;

    private float initialOrt;
    private float currentOrt;
    
    private bool isTransitioning;
    void Awake()
    {
        start = PlayerController.Instance.transform.position;
        cam = GameObject.FindWithTag("CinemachineVirtualCamera").GetComponent<CinemachineCamera>();
    }
    protected override void ExecuteAbility()
    {
        StartCoroutine(Slashes());
    }
    private Vector2 GenerateAbilityPoint()
    {
        Vector2 localPoint = Random.insideUnitCircle * 100;
        Vector3 globalPoint = transform.position + new Vector3(localPoint.x, localPoint.y, 0);
        return globalPoint;
    }
    

    void Start()
    {
        owner = transform.parent.transform.parent.gameObject;
        initialOrt = cam.Lens.OrthographicSize;
        currentOrt = initialOrt;
    }

    void Update()
    {
        if (PlayerIsOwner)
        {
            if (TakeStatFromSessionData)
            {
                cooldown = SessionData.AbilityCooldown;
                activeTime = SessionData.AbilityActiveTime;
            }
           
            
        }
        if (ActiveNow)
        {
            ActiveTimer -= Time.deltaTime;
        }
        else
        {
            ActiveTimer = 0f;
        }
        if (Reload)
        {
            currentCooldown -= Time.deltaTime;
        }
        else
        {
            currentCooldown = 0f;
        }
        if (isTransitioning )
        {
            if (currentOrt < 8f)
            {
                targetOrt = 12f;
                transitionSpeed = 1f;
                cam.Lens.OrthographicSize = 8f;
                isTransitioning = false;
            }
            currentOrt = Mathf.Lerp(currentOrt, targetOrt, transitionSpeed * Time.deltaTime);
            LensSettings lens = cam.Lens;
            lens.OrthographicSize = currentOrt;
            cam.Lens = lens;
            if (Mathf.Abs(currentOrt - targetOrt) < 0.1f)
            {
                transitionSpeed = activeTime/50;
                targetOrt = 1f;
            }
        }
    }

    public void StartLensTransition()
    {
        isTransitioning = true;
    }
    private IEnumerator Slashes()
    {
        StartLensTransition();
        while (ActiveNow)
        {
            start = GenerateAbilityPoint();
            Vector3 diff = transform.position - start;
            end = start + diff * 2;
            start += new Vector3(Random.Range(-30, 30), Random.Range(-30, 30));
            end += new Vector3(Random.Range(-30, 30), Random.Range(-30, 30));
            CreateLightningEffect(start, end,owner);
            yield return new WaitForSeconds(0.005f);
        }
        targetOrt = 8f;
        StartLensTransition();
    }

    private void CreateLightningEffect(Vector3 start, Vector3 end,GameObject thisOwner)
    {
        Debug.Log(owner);
        LightningVFX.Create(start, end, true,thisOwner);
    }

    
}
