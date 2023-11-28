using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class XcameraCtrl : MonoSingleton<XcameraCtrl>
{
    public  bool PlayerDeath = false;
    public bool cameraishere = false;

    [Header("카메라 이동 딜레이")]
    public float Timeformove = 0.5f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    private float minY = -0.1f;

    [SerializeField] private CinemachineDollyCart Cart;

    private void Start()
    {
        GameManager.Instance.OnMakeOverGraffiti += (o, args) => OnGraffitiDone();
    }

    void Update()
    {
        // 카메라 무브먼트 죽을 때 멈춤
        if (cameraishere == false)
        {
            Vector3 playerPosition = new Vector3(transform.position.x, Mathf.Max(player.position.y, minY), transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, Timeformove);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("camerastop")))
        {
            StartCoroutine(Death());
        }
        /*
        if ((collision.gameObject.CompareTag("camerastop2")))
        {
            StartCoroutine(Death());
        }
        */
    }

    IEnumerator Death()
    {
        cameraishere = true;
        PlayerDeath = true;

        for (int i = 0; i < 10; i++)
        {
            CameraEffect();
            yield return new WaitForSeconds(0.05f);
        }

        cameraishere = false;
        PlayerDeath = false; 


    }


    void CameraEffect()
    {
        transform.localPosition = transform.localPosition + Random.insideUnitSphere * 0.3f;
    }
    void OnGraffitiDone()
    {
        GetComponent<CinemachineBrain>().enabled = true;
        Cart.m_Speed = 2.5f;
    }
}
