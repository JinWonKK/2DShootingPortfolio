using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteManager : MonoBehaviour
{
    [SerializeField]
    private float meteoSpawnRate;

    GameObject obj;
    private Vector3 pos;

    private void Awake()
    {
        StartCoroutine("SpawnAlertLine");
    }

    IEnumerator SpawnAlertLine()
    {
        while(true)
        {
            yield return YieldInstructionCache.WaitForSeconds(meteoSpawnRate);
            obj = ObjectPoolManager.Instance.pools[(int)ObjectType.Objt_AlertLine_01].Pop();
            pos = Vector3.zero;
            pos.x = Random.Range(-2.5f, 2.5f);
            obj.transform.position = pos;
        }
        
    }
}
