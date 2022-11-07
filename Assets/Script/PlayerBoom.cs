using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : PoolLabel
{
    [SerializeField]
    private AnimationCurve curve;
    private float boomDelay = 1.5f;
    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    public override void Init()
    {
        base.Init();
        StartCoroutine("MoveToCenter");
    }

    private Vector3 startPos;
    private Vector3 endPos;
    private float current;
    private float percent;

    private IEnumerator MoveToCenter()
    {
        startPos = GameObject.FindObjectOfType<PlayerController>().transform.position;
        endPos = Vector3.zero;
        current = 0;
        percent = 0;
        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / boomDelay;
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(percent));
            yield return null;
        }
        // 목표지점에 도착
        ani.SetTrigger("OnBoom");
        Invoke("OnBoom", 0.3f);
    }

    private void OnBoom()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i].TryGetComponent<EnemyChar>(out EnemyChar enemyChar))
                enemyChar.OnDie();
            else if (enemys[i].TryGetComponent<PoolLabel>(out PoolLabel poolLabel))
                poolLabel.Push();
               
        }
        Push();
    }
}
