using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    float offsetX;

    void Start()
    {
        /*플레이어와 카메라의 간격값
        카메라의 x축 포지션값 - 카메라의 x축 포지션값 = offsetX
        */
        if (target == null) return;
        offsetX = transform.position.x - target.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector3 CameraPos = transform.position;//카메라위치
        /*위에서 구한 간격값에 카메라위치 더하기
        
        */
        CameraPos.x = transform.position.x + offsetX;
        transform.position = CameraPos;
    }
}
