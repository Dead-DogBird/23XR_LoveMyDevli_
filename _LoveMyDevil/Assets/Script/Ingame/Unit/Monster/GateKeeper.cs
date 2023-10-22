using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class GateKeeper : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    Vector2 Attackarea;

    private float oridelay;
    private bool isPlayer = false;
    private bool isClimb = false;
    
    private int focus = -1;

    //�߷�
    [Header("���� �� ����Ǵ� �߷� ��(�⺻�� : 5)")]
    [SerializeField] private float _gravity = 5;

    //�ٴ� ����
    [Header("���� ����(�⺻�� : 45)")]
    [SerializeField] float Degree = 45;
    //�ٴ� ��
    [Header("�ٴ� ��(�⺻�� : 3000)")]
    [SerializeField] float JumpForce = 3000;
    
    
    public AnimationClip[] animationClips; // �ִϸ��̼� Ŭ�� �迭
    private Animator animator;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(readygo());
        animator = GetComponent<Animator>();
        Attackarea = new Vector2(10, 1.5f);
        oridelay = jumpDelay;
    }

    IEnumerator readygo()
    {
        yield return new WaitForSeconds(6);
        Invoke("Jump", 2);
        StopCoroutine(readygo());
    }



    void Update()
    {
       ColliderCheckCallback();
    }

    void Jump()
    {
        _rigidbody.gravityScale = _gravity;
        PlayAnimation((focus==1?2:0));
        _rigidbody.AddForce(D9Extension.DegreeToVector2(Degree + (focus==-1?90:0))*JumpForce);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            ClimbWall().Forget();
        }
    }

    [SerializeField] private float jumpDelay = 1.2f;
    private bool isDie = false;
    void OnDestroy()
    {
        isDie = true;
    }
    async UniTaskVoid ClimbWall()
    {
        PlayAnimation((focus == 1 ? 3 : 1));
        focus *= -1;
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = new Vector2(0, 0);

        jumpDelay = oridelay;

        while (jumpDelay >= 0f)
        {
            jumpDelay -= 0.1f;
            if (isDie) return;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
        Jump();
    }
    void ColliderCheckCallback()
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, Attackarea, 0);
        foreach(Collider2D i in hit) 
        {
            if (i.CompareTag("Platform") || i.CompareTag ("ColoredPlatform") || i.CompareTag("DropPlatform")
                
            ) 
            {
                Destroy(i.gameObject);
            }                   
        }
    }
    //0. LeftJump, 1. LeftLanding, 2. RightJump, 3. RightLanding
    void PlayAnimation(int index)
    {
        if (index >= 0 && index < animationClips.Length)
        {
            animator.Play(animationClips[index].name);
        }
        else
        {
            Debug.LogWarning("Invalid animation index.");
        }
    }
}