using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AutoAim : MonoBehaviour
{
    [SerializeField] private GameObject AimPrefab;
    public static GameObject PlayerCursor{ get => cursor; }
    private static GameObject cursor;
    private GameObject Canvas;
    public float detectionRadius = 20f;
    public LayerMask enemyLayer;
    private Transform currentTarget;
    private GameObject targetMarker;
    private Camera cam;
    public bool AutoAimStatus = false;
    private bool isProcessing = false;
    private float AutoAimReload = 1f;
    private float AutoAimTimer = 0f;
    private Image AutoAimIndicatorUI;
    private Color32 disabledColor = new Color32(255, 255, 255, 34);
    private Color32 enabledColor = new Color32(255, 255, 255, 255);
    private void Awake()
    {
        Canvas = GameObject.FindWithTag("Panel").transform.parent.transform.gameObject;
        cursor = Instantiate(AimPrefab, Canvas.transform);
        cursor.transform.SetAsFirstSibling();
    }
    private void Start()
    {
        
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        AutoAimIndicatorUI = GameObject.FindWithTag("AutoAimIndicatorUI").GetComponent<Image>();
    }

    public void CheckVisual()
    {
        if (AutoAimStatus)
        {
            AutoAimIndicatorUI.color = enabledColor;

        }
        else
        {
            Cursor.visible = true;
            AutoAimIndicatorUI.color = disabledColor;
        }
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            CheckVisual();
            if (isProcessing)
            {
                AutoAimTimer += Time.deltaTime;
                AutoAimIndicatorUI.fillAmount = AutoAimTimer / AutoAimReload;
            }
            else
            {
                if (AutoAimTimer != 0f)
                {
                    AutoAimTimer = 0f;
                }
            }
            if (PlayerController.Instance.playerControls.Battle.AutoAim.IsPressed())
            {
                ToggleAutoAim();
            }
            if (AutoAimStatus)
            {
                FindClosestTarget();
                UpdateAimDirection();
            }
        }
        
        
    }
    private void FindClosestTarget()
    {
        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        Transform closest = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider2D col in hittedEnemies)
        {
            Vector3 viewportPos = cam.WorldToViewportPoint(col.transform.position);
            
            if (viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1)
            {
                float dist = Vector2.Distance(transform.position, col.transform.position);
                if (dist < closestDistance)
                {
                    closest = col.transform;
                    closestDistance = dist;
                }
            }
            
        }
        if (currentTarget != closest)
        {
            currentTarget = closest;
        }
    }
    
    public void ToggleAutoAim()
    {
        if (!isProcessing && Time.timeScale>0)
        {
            StartCoroutine(ToggleUseByCD());
        }
        
    }

    private IEnumerator ToggleUseByCD()
    {
        isProcessing = true;
        if (!AutoAimStatus)
        {
            cursor.GetComponent<Image>().enabled = true;
        }
        else
        {
            cursor.GetComponent<Image>().enabled = false;
        }
        AutoAimStatus = !AutoAimStatus;
        yield return new WaitForSeconds(AutoAimReload);
        isProcessing = false;
    }

    public void UpdateAimDirection()
    {
        if (currentTarget != null)
        {
            cursor.GetComponent<Image>().enabled = true;
            cursor.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(currentTarget.position);
        }
        else
        {
            cursor.GetComponent<Image>().enabled = false;
        }

    }
}
