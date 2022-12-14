using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFlakPool : MonoBehaviour
{
    public static ItemFlakPool Instance;
    public GameObject ItemFlakPrefab;
    public int initActivationCount;

    private Queue<ItemFlak> Q = new Queue<ItemFlak>();

    private void Awake()
    {
        Instance = this;
        Initilize(initActivationCount);
    }

    private ItemFlak CreateNewObject()
    {
        var newObj = Instantiate(ItemFlakPrefab, transform).GetComponent<ItemFlak>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    private void Initilize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Q.Enqueue(CreateNewObject());
        }
    }

    public static ItemFlak GetObject()
    {
        if (Instance.Q.Count > 0)
        {
            var obj = Instance.Q.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public static void ReturnObject(ItemFlak obj)
    {
        obj.gameObject.SetActive(false);
        Instance.Q.Enqueue(obj);
    }
}
