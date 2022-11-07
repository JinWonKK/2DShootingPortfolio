using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.TryGetComponent<PoolLabel>(out PoolLabel pl))
            pl.Push();
    }
}
