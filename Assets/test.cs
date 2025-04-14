using UnityEngine;

public class test : MonoBehaviour
{
    public Vector3 testVar;
    void Start()
    {
        testVar = SessionData.MeleeSize;
        Debug.Log($"Шанс ваншота: {SessionData.ScaleValueToProcente(SessionData.OneShootChance)}%");
        SessionData.SetValueChance(ref SessionData.OneShootChance,1);

    }
    void Update()
    {
        testVar = SessionData.MeleeSize;
        Debug.Log($"Шанс ваншота: {SessionData.ScaleValueToProcente(SessionData.OneShootChance)}%");
        
    }
}
