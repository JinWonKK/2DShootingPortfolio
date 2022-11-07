using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item_Type
{
    IT_Power,
    IT_Boom,
    IT_Heart,
}

public class FlyItem : PoolLabel
{
    [SerializeField]
    private Item_Type type;
    private Movement2D movement;

    private void Awake()
    {
        movement = GetComponent<Movement2D>();
    }

    public override void Init()
    {
        base.Init();
        StartCoroutine("MoveCoroutine");
    }

    private Vector3 dir;
    private Vector3 moveTarget;
    private void SetMoveTarget()
    {
        moveTarget.x = Random.Range(-2.6f, 2.6f);
        moveTarget.y = Random.Range(-3f, 0f);
        moveTarget.z = 0f;
        dir = moveTarget - transform.position;
        movement.MoveTo(dir.normalized);
    }

    IEnumerator MoveCoroutine()
    {
        yield return null;
        while(true)
        {
            SetMoveTarget();
            yield return YieldInstructionCache.WaitForSeconds(4f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            switch(type)
            {
                case Item_Type.IT_Power:
                    break;
                case Item_Type.IT_Boom:
                    if(collision.TryGetComponent<Weapon>(out Weapon weapon))
                    {
                        weapon.BoomCount++;
                    }
                    break;
                case Item_Type.IT_Heart:
                    if(collision.TryGetComponent<PlayerState>(out PlayerState ps))
                    {
                        ps.TakeHealing();
                    }
                    break;
            }
            Push();
        }
    }

}
