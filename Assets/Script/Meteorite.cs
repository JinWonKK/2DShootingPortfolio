using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : PoolLabel
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerState>(out PlayerState ps))
            ps.TakeDamage(1);


    }
}
