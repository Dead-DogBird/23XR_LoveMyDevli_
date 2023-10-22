using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class openanimctrl : MonoBehaviour
{
    public Transform sprite1; // ù ��° ��������Ʈ�� Transform ������Ʈ
    public Transform sprite2; // �� ��° ��������Ʈ�� Transform ������Ʈ
    public float moveDuration = 2.0f; // �̵��� �ɸ��� �ð� (��)

    private Vector3 targetPosition1 = new Vector3(-20f, 0f, -9f);
    private Vector3 targetPosition2 = new Vector3(20f, 0f, -9f);
    private Vector3 initialPosition1;
    private Vector3 initialPosition2;

    private void Start()
    {
        // ��������Ʈ �ʱ� ��ġ ����
        initialPosition1 = sprite1.position;
        initialPosition2 = sprite2.position;

        // �� ���� �� ��������Ʈ�� ������ �̵���ŵ�ϴ�.
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

        // ��ǥ ��ġ�� ������ �� �ڷ�ƾ ����
        sprite1.position = targetPosition1;
        sprite2.position = targetPosition2;
    }
}
