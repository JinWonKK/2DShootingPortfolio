using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController controller;

    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); 

        if (controller == null)
        {
            Debug.Log("캐릭터 컨트롤러를 찾지 못했습니다.");
        }
    }

    private void OnMouseDown() // 터치 이벤트
    {
        controller.ISMOVE = true;
    }

    private void OnMouseUp()
    {
        controller.ISMOVE = false;
    }
}
