using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 컴포넌트들
    public Rigidbody2D _playerRigidbody { get; private set; }
    private BoxCollider2D _boxCollider2D;
    private PlayerContrl _playerControll;
    private PlayerAnimation _playerAnimation;


    [Header("플레이어 스탯")]
    [SerializeField] private float speed = 5f;
    [Tooltip("플레이어 점프력 (보통 1000 해놓음)")]
    [SerializeField] private float jumpForce = 1000;

    [Header("플레이어 최대 점프 횟수")]
    [SerializeField] private int MAXJUMP = 2;

    [Header("플레이어 점멸 관련 변수들")]
    [Tooltip("점멸 시간")]
    [SerializeField] float blinkDuration = 0.5f;
    [Tooltip("점멸 딜레이")]
    [SerializeField] private float BlinkDelay = 1;


    [SerializeField] private ColliderCallbackController platformCollider;
    //기타 트리거들
    private bool _isjumping;
    private int jumpCount = 0;

    private float _playerOriSpeed;
    private float oriGravity;
    private bool isBlink;

    //임시 애니메이션 구현 
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerControll = GetComponent<PlayerContrl>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        
        _spriteRenderer.color = Color.white;
        _boxCollider2D.isTrigger = false;
        speed = 5.3f;
        GameManager.Instance.setPlayer(gameObject);
        platformCollider.onColiderEnter += PlatformEnter;
        platformCollider.onColiderExit += PlatformExit;
    }

    private void OnEnable()
    {
    }

    void Update()
    {
        Jump();
        if (_playerControll.Userinput.SkillKey && !isBlink)
        {
            Blink(_playerControll.Userinput.AxisState).Forget();
        }
        if (XcameraCtrl.PlayerDeath == true)
        {
            deathfromkeeper();
        }
    }

    //죽음
    void deathfromkeeper()
    {
        speed = 0;
        _boxCollider2D.isTrigger = true;
        _spriteRenderer.color = Color.red;
    }


    private void FixedUpdate()
    {
        if (!_isjumping && _playerControll.Userinput.AxisState != 0)
        {
            _playerAnimation.SetAnimation((_playerControll.Userinput.AxisState>0)
                ?PlayerAnimation.Animations.leftrun:PlayerAnimation.Animations.rightrun,true);
        }
        else if(!_isjumping &&_playerAnimation.nowAnimation!=PlayerAnimation.Animations.idle)
        {
            _playerAnimation.SetAnimation(PlayerAnimation.Animations.idle,true);
        }
        transform.Translate(new Vector3(speed * _playerControll.Userinput.AxisState, 0) * Time.deltaTime);
        
    }


    void Jump()
    {
        if (jumpCount >= MAXJUMP) return;
        if (!_playerControll.Userinput.SpaceState) return;
        _isjumping = true;
        jumpCount++;
        _playerAnimation.SetAnimation((_playerControll.Userinput.AxisState>=0)
            ?PlayerAnimation.Animations.leftjump:PlayerAnimation.Animations.rightjump,false);
        if (jumpCount == 2)
        {
            
            GetComponent<Player_Effect>().GetDash(0.7f,_playerControll.Userinput.AxisState>0);
            GetComponent<Player_Effect>().getEffect(Player_Effect.Effects.DoubleJump);
            _playerAnimation.SetAnimation(PlayerAnimation.Animations.doublejump,false);
            _playerRigidbody.velocity = Vector2.zero;
            _playerRigidbody.AddForce(new Vector2(0, jumpForce));
            return;
        }
        //GetComponent<Player_Effect>().getEffect(Player_Effect.Effects.Jump);
        //_playerAnimation.SetAnimation(PlayerAnimation.Animations.jump,false);
        _playerRigidbody.AddForce(new Vector2(0, jumpForce));
        _playerRigidbody.velocity = Vector2.zero;

    }

    private bool isCollisionWall;
    async UniTaskVoid Blink(float getAxis)
    {
        isBlink = true;
        _playerOriSpeed = speed;
        //GetComponent<Player_Effect>().getEffect(getAxis>0?Player_Effect.Effects.LeftDash:Player_Effect.Effects.RightDash);
        oriGravity = _playerRigidbody.gravityScale;
        _playerRigidbody.velocity = Vector2.zero;
        _playerRigidbody.gravityScale = 0;
        GetComponent<Player_Effect>().GetDash(blinkDuration,getAxis>0);
        speed = 0;

        float blinktimer = blinkDuration;
        float blinkDelay = BlinkDelay;
        float toPos =getAxis>0? 1f : -1f;
        while (blinktimer > 0)
        {
            blinktimer -= 0.1f;
            if (isCollisionWall)
            {
                isCollisionWall = false;
                break;
            }
            // _playerAnimation.SetAnimation(PlayerAnimation.Animations.doublejump,true);
            transform.Translate(new Vector3(_playerOriSpeed * 3 * toPos, 0) * Time.deltaTime);
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        }

        speed = _playerOriSpeed;
        _playerRigidbody.gravityScale = oriGravity;
        //_playerAnimation.SetAnimation(PlayerAnimation.Animations.jump,true);
        while (blinkDelay > 0)
        {
            blinkDelay -= 0.1f;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
        isBlink = false;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
       
    }

    private void OnCollisionExit2D(Collision2D other)
    {

    }

    void PlatformEnter(Collider2D other)
    {
        Debug.Log("바닥에 닿임");
        if ((other.gameObject.CompareTag("Ground") ||
             other.gameObject.CompareTag("ColoredPlatform") ||
             other.gameObject.CompareTag("DropPlatform") ||
             other.gameObject.CompareTag("Platform")))
        {
            _isjumping = false;
            jumpCount = 0;
            _playerAnimation.SetAnimation(PlayerAnimation.Animations.idle,true);
        }
        if (other.transform.CompareTag("DropPlatform") && other.transform.position.y < transform.position.y)
        {
            other.transform.GetComponent<DroppedPlatform>().Dropped().Forget();
            _playerAnimation.SetAnimation(PlayerAnimation.Animations.idle,true);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            isCollisionWall = true;
        }
    }

    void PlatformExit(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Ground") ||
             other.gameObject.CompareTag("ColoredPlatform") ||
             other.gameObject.CompareTag("DropPlatform")))
        {
            _isjumping = true;
            jumpCount = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SprayItem"))
        {
            Destroy(other.gameObject);
            GetComponent<PlayerAct>().GetSpray(20);
        }

        if (other.CompareTag("CameraCollider"))
        {
            Camera.main.GetComponent<CameraChanger>().UpdateCamera(other.GetComponent<CameraCollider>().colliderID);
        }
    }
}
