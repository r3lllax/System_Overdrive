using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DamageUI : MonoBehaviour
{
    public static DamageUI Instance {get; private set;}
 
    private class ActiveText{
        public TextMeshProUGUI UIText;
        public float maxTime;
        public float Timer;
        public Vector3 Position;

        public void MoveText(Camera camera){
            float delta = 1f - (Timer/maxTime);
            Vector3 pos = Position + new Vector3(delta,delta,0f);
            pos = camera.WorldToScreenPoint(pos);
            pos.z = 0f;

            UIText.transform.position = pos;
        }

    }
 
    public TextMeshProUGUI TextPrefab;
    const int PoolSize = 1000;

    Queue<TextMeshProUGUI> TextPool = new Queue<TextMeshProUGUI>();
    List<ActiveText> ActiveTexts = new List<ActiveText>(); 

    private void Awake()
    {
        Instance = this;
    }
    Camera camera;
    Transform m_transform;

    private void Start()
    {
        camera = Camera.main;
        m_transform = transform;
        for(int i=0;i<PoolSize;i++){
            TextMeshProUGUI temp = Instantiate(TextPrefab,m_transform);
            temp.gameObject.SetActive(false);
            TextPool.Enqueue(temp);
        }
    }

    private void Update()
    {
        for(int i =0;i<ActiveTexts.Count;i++){
            ActiveText at = ActiveTexts[i];
            at.Timer -=Time.deltaTime;

            if(at.Timer <=0.0f){
                at.UIText.gameObject.SetActive(false);
                TextPool.Enqueue(at.UIText);
                ActiveTexts.RemoveAt(i);
                i--;
            }
            else{
                var color = at.UIText.color;
                color.a = at.Timer/at.maxTime;
                at.UIText.color = color;

                at.MoveText(camera);
            }
        }
    }

    public void AddText(int amt, Vector3 Pos, string modifier = "default"){
        TextMeshProUGUI t;
        if (TextPool.Count > 0)
        {
            t = TextPool.Dequeue();
        }
        else
        {
            t = Instantiate(TextPrefab,m_transform);
        }
        
        if (modifier == "crit")
        {

            t.GetComponent<TextMeshProUGUI>().color = new Color32(255, 60, 60, 91);
            t.text = amt.ToString();
        }
        else if (modifier == "oneshoot")
        {
            t.GetComponent<TextMeshProUGUI>().color = new Color32(238, 130, 238, 91);
            t.text = "Execute!";
        }
        else if (modifier == "LevelUP")
        {
            t.GetComponent<TextMeshProUGUI>().color = new Color32(0, 255, 255, 255);
            t.text = "LEVEL UP!";
        }
        else if (modifier == "GhostMode")
        {
            t.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
            t.text = "GhostMode!";
        }
        else if (modifier == "Lightning")
        {
            t.GetComponent<TextMeshProUGUI>().color = new Color32(0, 255, 234, 255);
            t.text = amt.ToString();
        }
        else if (modifier == "Evade")
        {
            t.GetComponent<TextMeshProUGUI>().color = new Color32(145, 145, 145, 255);
            t.text = "Blocked!";
        }
        else
        {
            t.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
            t.text = amt.ToString();
        }
        
        t.gameObject.SetActive(true);

        ActiveText at = new ActiveText(){maxTime = 1f};
        at.Timer = at.maxTime;
        at.UIText = t;
        at.Position = Pos+ Vector3.up;

        at.MoveText(camera);
        ActiveTexts.Add(at);
    }
}
