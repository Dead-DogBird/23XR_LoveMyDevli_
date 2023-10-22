using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class openanimctrl : MonoBehaviour
{
    public Transform sprite1; // 첫 번째 스프라이트의 Transform 컴포넌트
    public Transform sprite2; // 두 번째 스프라이트의 Transform 컴포넌트
    public float moveDuration = 2.0f; // 이동에 걸리는 시간 (초)

    private Vector3 targetPosition1 = new Vector3(-20f, 0f, -9f);
    private Vector3 targetPosition2 = new Vector3(20f, 0f, -9f);
    private Vector3 initialPosition1;
    private Vector3 initialPosition2;

    private void Start()
    {
        // 스프라이트 초기 위치 설정
        initialPosition1 = sprite1.position;
        initialPosition2 = sprite2.position;

        // 씬 시작 시 스프라이트를 서서히 이동시킵니다.
        StartCoroutine(MoveSpritesToTargets());
    }

    private IEnumerator MoveSpritesToTargets()
    {
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            sprite1.position = Vector3.Lerp(initialPosition1, targetPosition1, t);
            sprite2.position = Vector3.Lerp(initialPosition2, targetPosition2, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 목표 위치에 도달한 후 코루틴 종료
        sprite1.position = targetPosition1;
        sprite2.position = targetPosition2;
    }
}
