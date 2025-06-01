using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AutoAim : MonoBehaviour
{
    [SerializeField] private Texture2D AimCursor;
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
    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        AutoAimIndicatorUI = GameObject.FindWithTag("AutoAimIndicatorUI").GetComponent<Image>();
    }



    private void Update()
    {
        if (AutoAimStatus)
        {
            AutoAimIndicatorUI.color = enabledColor;

        }
        else
        {
            AutoAimIndicatorUI.color = disabledColor;
        }
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
        if (Input.GetKey(KeyCode.Space))
        {
            ToggleAutoAim();
        }
        if (AutoAimStatus)
        {
            FindClosestTarget();
            UpdateAimDirection();
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
        if (!isProcessing)
        {
            StartCoroutine(ToggleUseByCD());
        }
        
    }

    private IEnumerator ToggleUseByCD()
    {
        isProcessing = true;
        if (!AutoAimStatus)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.SetCursor(AimCursor, new Vector2(AimCursor.width/2,AimCursor.height/2), CursorMode.Auto);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.SetCursor(null, new Vector2(AimCursor.width/2,AimCursor.height/2), CursorMode.Auto);
        }
        AutoAimStatus = !AutoAimStatus;
        yield return new WaitForSeconds(AutoAimReload);
        isProcessing = false;
    }

    public void UpdateAimDirection()
    {
        if (currentTarget != null)
        {
            Mouse.current.WarpCursorPosition(cam.WorldToScreenPoint(currentTarget.position));

            // Vector2 direction = (currentTarget.position - transform.position).normalized;
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

    }
}
