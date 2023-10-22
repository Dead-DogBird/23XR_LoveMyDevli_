using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XcameraCtrl : MonoBehaviour
{
    public static bool PlayerDeath = false;
    public bool cameraishere = false;

    [Header("Ä«¸Þ¶ó ÀÌµ¿ µô·¹ÀÌ")]
    public float Timeformove = 0.5f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    private float minY = 0;

    

    void Update()
    {
        // Ä«¸Þ¶ó ¹«ºê¸ÕÆ® Á×À» ¶§ ¸ØÃã
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
        transform.localPosition = transform.localPosition + Random.insideUnitSphere * 0.6f;
    }

}
