using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform target;
    private float offsetX;

    void Start()
    {
        target = GameManager.Instance.Player.transform;

        /*카메라의 간격값
        카메라의 x축 포지션값 - 플레이어의 x축 포지션값 = offsetX
        */
        if (target == null) return;
        offsetX = transform.position.x - target.position.x;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 cameraPos = transform.position;//카메라위치
        
        /*위에서 구한 간격값에 카메라위치 더하기
        플레이어 위치에 카메라 위치 x축 값을 더해서  CameraPos 변수에 지정
        */
        cameraPos.x = target.position.x + offsetX;
        transform.position = cameraPos;
    }
}
