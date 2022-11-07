using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : PoolLabel
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public override void Init()
    {
        base.Init();
        ps.Play();
    }

    private void Update()
    {
        if(ps.isPlaying == false)
        {
            Push();
        }
    }
}
