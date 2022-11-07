using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject poolObj;
    [SerializeField]
    private int allocateCount;
    private Stack<PoolLabel> poolStack = new Stack<PoolLabel>();

    private int objMaxCount;
    private int objActiveCount;
    private int objCount;

    private void Awake()
    {
        objMaxCount = 0;
        objActiveCount = 0;
        objCount = 0;
        Allocate();
    }

    private GameObject allocateobj;

    private void Allocate()
    {
        for(int i = 0; i < allocateCount; i++)
        {
            objCount++;
            allocateobj = Instantiate(poolObj, this.transform);
            allocateobj.GetComponent<PoolLabel>().Create(this);
            allocateobj.gameObject.name = allocateobj.gameObject.name + objCount.ToString();
            poolStack.Push(allocateobj.GetComponent<PoolLabel>());
            objMaxCount++;
        }
    }

    public GameObject Pop()
    {
        if (objActiveCount >= objMaxCount)
            Allocate();
        PoolLabel label = poolStack.Pop();
        label.gameObject.SetActive(true);
        label.Init();
        objActiveCount++;
        return label.gameObject;
    }

    public void Push(PoolLabel obj)
    {
        if (obj.gameObject.activeSelf)
        {
            obj.gameObject.SetActive(false);
            poolStack.Push(obj);
            objActiveCount--;
        }   
    }
}
