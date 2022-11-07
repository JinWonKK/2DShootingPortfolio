using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChar : PoolLabel
{
    [SerializeField]
    private int currentHP;
    [SerializeField]
    private int MaxHP;
    [SerializeField]
    private int returnScore;

    private bool isAlive;
    private SpriteRenderer sr;

    private GameObject obj;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            currentHP -= damage;
            StopCoroutine("HitColor");
            StartCoroutine("HitColor");
            if (currentHP <= 0)
                OnDie();
        }
    }

    public override void Init()
    {
        base.Init();
        currentHP = MaxHP;
        sr.color = Color.white;
        isAlive = true;
    }

    public void SetEnemyLevel(int newMaxHp, int newScore)
    {
        currentHP = MaxHP = newMaxHp;
        returnScore = newScore;
    }

    GameObject effectObj;
    public int DieCount = 0;
    public void OnDie()
    {
        effectObj = ObjectPoolManager.Instance.pools[(int)ObjectType.ObjT_Effect_01].Pop();
        effectObj.transform.position = transform.position;
        DropItem();
        GameManager.Inst.AddScore(5);
        isAlive = false;
        SoundManager.Inst.PlaySFX(sfx_Type.sfx_EnemyDie);
        DieCount += 1;
        Push();
    }

    private IEnumerator HitColor()
    {
        sr.color = Color.red;
        yield return YieldInstructionCache.WaitForSeconds(0.05f);
        if (gameObject.activeSelf)
            sr.color = Color.white;
    }

    private void DropItem()
    {
        for (int i = 0; i < 5; i++)
        {
            obj = ObjectPoolManager.Instance.pools[(int)ObjectType.Objt_Item_01].Pop();
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.identity;
        }

        int randValue = Random.Range(1, 100);

        if (randValue < 2) // 1%
        {
            obj = ObjectPoolManager.Instance.pools[(int)ObjectType.Objt_Item_02B].Pop();
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.identity;
        }
        else if (randValue < 4) // 2%
        {
            obj = ObjectPoolManager.Instance.pools[(int)ObjectType.Objt_Item_03H].Pop();
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.identity;
        }

        /*obj = ObjectPoolManager.Instance.pools[(int)ObjectType.Objt_Item_04P].Pop();
        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.identity;*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerState>(out PlayerState ps))
            ps.TakeDamage(1);
    }
}
