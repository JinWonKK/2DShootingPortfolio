using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PoolLabel
{
    [SerializeField]
    private string TargetTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TargetTag) && collision.TryGetComponent<PlayerState>(out PlayerState pc))
        {
            pc.TakeDamage(1);
            Push();
        }
        if(collision.CompareTag(TargetTag) && collision.TryGetComponent<EnemyChar>(out EnemyChar ec))
        {
            ec.TakeDamage(1);
            Push();
        }
        if (collision.CompareTag(TargetTag) && collision.TryGetComponent<BossHP>(out BossHP boss))
        {
            boss.TakeDamage(1);
            Push();
        }



    }
}
