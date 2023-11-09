using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defaultcamera : MonoBehaviour
{
    private Vector3 incase = new Vector3(0f, 0f, -10f);

    [Header("카메라 이동 딜레이")]
    public float Timeformove = 0.5f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    private float minY = 1.65f;

    // Update is called once per frame
    void Update()
    {

        float playerY = player.position.y;


        // if (playerY < minY)
        // {
        //     playerY = minY;
        // }

        Vector3 playerposition = new Vector3(player.position.x + incase.x, playerY + incase.y, player.position.z + incase.z);
        transform.position = Vector3.SmoothDamp(transform.position, playerposition, ref velocity, Timeformove);
    }
}
