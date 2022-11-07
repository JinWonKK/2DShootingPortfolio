using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    BS_MoveToAppear = 0,
    BS_Phase01,
    BS_Phase02,
}

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    public BossState bossState = BossState.BS_MoveToAppear;
    private Movement2D movement;
    private BossWeapon weapon;
    private BossHP bossHP;

    private Vector3 startPos = new Vector3(0f, 7f, 0f);

    private void Awake()
    {
        movement = GetComponent<Movement2D>();
        weapon = GetComponent<BossWeapon>();
        bossHP = GetComponent<BossHP>();
    }

    public void Init(string name, int newHP)
    {
        transform.position = startPos;
        gameObject.SetActive(true);
        ChangeState(BossState.BS_MoveToAppear);
        bossHP.InitState(name, newHP);
    }

    public void ChangeState(BossState newState)
    {
        StopCoroutine(bossState.ToString());
        bossState = newState;
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator BS_MoveToAppear()
    {
        movement.MoveTo(Vector3.down);
        while (true)
        {
            if (transform.position.y <= bossAppearPoint)
            {
                movement.MoveTo(Vector3.zero);
                ChangeState(BossState.BS_Phase01);
            }
            yield return null;
        }
    }

    public IEnumerator BS_Phase01()
    {
        weapon.StartFiring(AttackType.AT_CircleFire2);
        while (true)
        {
            yield return null;
        }
    }

    Vector2 dir;

    private IEnumerator BS_Phase02()
    {
        //weapon.StartFiring(AttackType.AT_SingleFireToCenterPosition);
        dir = Vector2.right;
        movement.MoveTo(dir);
        while (true)
        {
            if (transform.position.x <= -2.6f || transform.position.x >= 2.6)
            {
                dir *= -1;
                movement.MoveTo(dir);
            }
            yield return null;
        }
    }
}
