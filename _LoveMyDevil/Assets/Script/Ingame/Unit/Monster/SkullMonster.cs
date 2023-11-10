using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;


public class SkullMonster : MonoBehaviour
{
    [SerializeField] ColliderCallbackController colliderCallbackController;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private float _speed = 3;
    [SerializeField] private float knockbackForce = 5f; // ³Ë¹é Èû ¼³Á¤

    private Rigidbody2D _rigid;
    
    
    private bool _targetedPlayer;
    private Transform player;
    int nextMove;
    private bool isDie = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        MoveSelect().Forget();
    }

    private void OnEnable()
    {
    }

    private void OnDestroy()
    {
        isDie = true;
    }

    // Update is called once per frame
    void Update()
    {
       NontargetPlayerMove();
    }

    void NontargetPlayerMove()
    {
        _rigid.velocity = new Vector2(nextMove, _rigid.velocity.y);
        Vector3 frontVec = new Vector3(_rigid.position.x + nextMove*0.7f,_rigid.position.y);
        Debug.DrawLine(frontVec,frontVec+Vector3.down*1,Color.blue);
        if (!Physics2D.Raycast(frontVec, Vector3.down, 1))
        {
         
            nextMove *= -1;
            transform.localScale = nextMove > 0 ? new Vector3(-1,1) : new Vector3(1,1);
        }

        frontVec = new Vector2(_rigid.position.x + nextMove*0.2f,_rigid.position.y+0.5f);
        // if (Physics2D.Raycast(frontVec, Vector3.up, 1, LayerMask.GetMask("Platform")))
        // {
        //     nextMove *= -1;
        //     transform.localScale = nextMove > 0 ? new Vector3(-1,1) : new Vector3(1,1);
        // }
    }
    async UniTaskVoid MoveSelect()
    {
        while (!isDie)
        {
            nextMove =  Random.Range(-1, 2);
            transform.localScale = nextMove > 0 ? new Vector3(-1,1) : new Vector3(1,1);
            await UniTask.Delay(TimeSpan.FromSeconds(5), ignoreTimeScale: false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

}
