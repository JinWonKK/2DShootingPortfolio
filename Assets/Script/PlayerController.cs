using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector2 clampMin;
    [SerializeField]
    private Vector2 clampMax;
    [SerializeField]
    private float attackRate;

    private bool isMove;
    public bool ISMOVE
    {
        set
        {
            isMove = value;
            if (isMove)
                GetComponent<Weapon>().IsFireing = true;
            else
                GetComponent<Weapon>().IsFireing = false;
        }
    }

    [SerializeField]
    private GameObject pre;

    private void Awake()
    {
        Invoke("InitPlayer", 1.0f);
    }

    void InitPlayer()
    {
        isMove = false;
        GetComponent<Weapon>().Init(pre, attackRate);
        GetComponent<PlayerState>().InitChar();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.x = Mathf.Clamp(pos.x, clampMin.x, clampMax.x); // pos.x가 Min.x보다 작다면 Min.x값을 리턴, Max.x보다 크다면 Max.x값을 리턴
            pos.y = Mathf.Clamp(pos.y, clampMin.y, clampMax.y);
            pos.z = transform.position.z;

            transform.position = pos;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Weapon>().LunchBoom();
        }
    }

}
