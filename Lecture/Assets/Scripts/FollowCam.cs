using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform targetTr;
    public float dist = 5f;          // 거리.
    public float height = 2.3f;
    public float dampTrace = 20.0f;     // 부드러운 추적을 위해 카메라의 회전에 딜레이를 주기 위한 값.
    private Transform tr;

    // Use this for initialization
    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        tr.position = Vector3.Slerp(tr.position, (targetTr.position - (targetTr.forward * dist) + (targetTr.up * height)), dampTrace * Time.deltaTime);
        //                                                              //       z축 -10                     y축 +3
        // 직선 보간법을 이용하여 부드럽게 이동하도록 만들어줌.
        tr.LookAt(targetTr.position);
    }
}