using System.Collections.Generic;
using UnityEngine;

public class ExpPool : MonoBehaviour
{
    public static ExpPool Instance;
    public GameObject ExpPrefab;
    public int poolSize = 300;

    private Queue<GameObject> Exps = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
        ExpPrefab = TempData.ExpPrefab;
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject Exp = Instantiate(ExpPrefab);
            Exp.SetActive(false);
            Exps.Enqueue(Exp);
        }
    }

    public GameObject GetExp()
    {
        if (Exps.Count > 0)
        {
            GameObject Exp = Exps.Dequeue();
            Exp.SetActive(true);
            Exp.GetComponent<Exp>().MoveToPlayerTransform = false;
            return Exp;
        }
        else
        {
            GameObject Exp = Instantiate(ExpPrefab);
            Exp.GetComponent<Exp>().MoveToPlayerTransform = false;
            return Exp;
        }
    }

    public void ReturnExp(GameObject Exp)
    {
        Exp.SetActive(false);
        Exps.Enqueue(Exp);
    }
}
