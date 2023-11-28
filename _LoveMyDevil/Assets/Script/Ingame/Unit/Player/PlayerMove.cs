using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cinemachine;
using Cysharp.Threading.Tasks;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoSingleton<PlayerMove>
{
    

    //사망시 BGM 멈춤 
    public static bool soundstop = false; 


    [FMODUnity.EventRef]
    public string SFXCtrl;

    [FMODUnity.EventRef]
    public string DeathSFXCtrl;

    private FMOD.Studio.EventInstance SFXInstance;
    private FMOD.Studio.EventInstance WalkInstance;

    public FMOD.Studio.EventInstance DeathInstance;



    // 컴포넌트들
    public Rigidbody2D _playerRigidbody { get; private set; }
    private BoxCollider2D _boxCollider2D;
    private PlayerContrl _playerControll;
    private PlayerAnimation _playerAnimation;
    private int BossStageHp = 5;

    [Header("플레이어 스탯")]
     public static float speed = 5.3f;
    [Tooltip("플레이어 점프력 (보통 1000 해놓음)")]
    [SerializeField] private float jumpForce = 1000;
    [Header("플레이어 무적 시간")]
    [Tooltip("플레이어 무적 시간 (기본값 0.5초)")]
    [SerializeField] private float ignoreTime = 0.5f;

    [Header("플레이어 최대 점프 횟수")]
    [SerializeField] private int MAXJUMP = 2;
    [Header("스프레이 아이템 회복량(%)")]
    [SerializeField] private float Getspray = 30;
    [Header("플레이어 점멸 관련 변수들")]
    [Tooltip("점멸 시간")]
    [SerializeField] float blinkDuration = 0.5f;
    [Tooltip("점멸 딜레이")]
    [SerializeField] private float BlinkDelay = 1;
    
    [Header("플레이어 넉백 거리 (기본값 1)")]
    [SerializeField] private float CustomKnockBackPos = 1;

    [SerializeField] private ColliderCallbackController platformCollider;
    //기타 트리거들
    private bool _isjumping;
    private int jumpCount = 0;

    private bool isIgnore=false;

    //더블탭 대쉬
    public bool Blinkmode = true;
    private float FirstTimeChecker;
    private float Betweentime = 0.3f;
    private bool isTimeCheck = true;
    private int lclick = 0;
    private int rclick = 0;

    //프리징 상태
    
    public static bool freeze = true; 


    private float _playerOriSpeed;
    private float oriGravity;
    private bool isBlink;

    //임시 애니메이션 구현 
    private SpriteRenderer _spriteRenderer;

    private bool isKnockBack;
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
        platformCollider.onColliderEnter += PlatformEnter;
        platformCollider.onColliderExit += PlatformExit;
        Blinkmode = true;
        SFXInstance = FMODUnity.RuntimeManager.CreateInstance(SFXCtrl);
        DeathInstance = FMODUnity.RuntimeManager.CreateInstance(DeathSFXCtrl);

        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadedsceneEvent;
    }


    void Update()
    {

        if (freeze == false)
        {
            if (isKnockBack) return;
            Jump();
            if (Blinkmode)
            {
                if (_playerControll.Userinput.SkillKey && !isBlink)
                {
                    Blink(_playerControll.Userinput.AxisState).Forget();
                }
            }
            // 키보드 A,D 더블탭 대쉬
            if (Blinkmode == false && !isBlink)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    rclick += 1;
                }
                if (rclick == 1 && isTimeCheck)
                {
                    FirstTimeChecker = Time.time;
                    StartCoroutine(DoubleClicked());
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        rclick -= 1;
                    }

                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    lclick += 1;
                }
                if (lclick == 1 && isTimeCheck)
                {
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        lclick -= 1;
                    }
                    FirstTimeChecker = Time.time;
                    StartCoroutine(DoubleClicked());
                }
            }

            

           
        }

        


        if (XcameraCtrl.Instance.PlayerDeath == true)
        {
            deathfromkeeper();
        }
    }
    // 더블탭 대쉬 
    private IEnumerator DoubleClicked()
    {
        isTimeCheck = false;
        while (Time.time < FirstTimeChecker + Betweentime)
        {
            if (rclick == 2)
            {
                Blink(_playerControll.Userinput.AxisState).Forget();
                break;
            }
            if(lclick == 2)
            {
                Blink(_playerControll.Userinput.AxisState).Forget();
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        rclick = 0;
        lclick = 0;
        isTimeCheck = true;
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
        if (isKnockBack) return;
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
        SFXInstance.setParameterByName("PlayerState", 0.0f);
        SFXInstance.setVolume(0.2f);
        SFXInstance.start();
        jumpCount++;
        _playerAnimation.SetAnimation((_playerControll.Userinput.AxisState>=0)
            ?PlayerAnimation.Animations.leftjump:PlayerAnimation.Animations.rightjump,false);
        if (jumpCount == 2)
        {
            SFXInstance.setParameterByName("PlayerState", 1.0f);
            SFXInstance.setVolume(0.3f);
            SFXInstance.start();
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
        SFXInstance.setVolume(0.6f);
        isBlink = true;
        _playerOriSpeed = speed;
        //GetComponent<Player_Effect>().getEffect(getAxis>0?Player_Effect.Effects.LeftDash:Player_Effect.Effects.RightDash);
        oriGravity = _playerRigidbody.gravityScale;
        _playerRigidbody.velocity = Vector2.zero;
        _playerRigidbody.gravityScale = 0;
        GetComponent<Player_Effect>().GetDash(blinkDuration,getAxis>0);
        speed = 0;

        SFXInstance.setParameterByName("PlayerState", 3.0f);
        SFXInstance.start();

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

     void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

     void OnCollisionExit2D(Collision2D collision)
    {
        
    }


   
    public void GetKnockBack(Vector3 pos, float knokbackfos,bool isDie = false)
    {
        if (isIgnore) return;
        IgnoreTask().Forget();

        SFXInstance.setParameterByName("PlayerState", 2.0f);
         SFXInstance.setVolume(0.6f);
        SFXInstance.start();
       
        

        Debug.Log("넉백");
       // _playerRigidbody.AddForce(CustomAngle.VectorRotation(CustomAngle.PointDirection(transform.position,pos))*knokbackfos,ForceMode2D.Impulse);
        _playerRigidbody.velocity = (CustomAngle.VectorRotation(CustomAngle.PointDirection(transform.position, new Vector2(pos.x,transform.position.y-0.5f))+180) * knokbackfos*CustomKnockBackPos);
        if(!isDie)_playerAnimation.SetAnimation(PlayerAnimation.Animations.hit);
        
        CinemachineImpulseSource? source = GetComponent<CinemachineImpulseSource>();
        if (source)
        {
            source.m_DefaultVelocity = ((CustomAngle.VectorRotation(CustomAngle.PointDirection(transform.position, new Vector2(pos.x,transform.position.y))+180) * knokbackfos))*0.07f;
            source.m_ImpulseDefinition.m_ImpulseDuration = 0.3f;
            //source.m_ImpulseDefinition.m_ImpulseChannel = 0;
            source.GenerateImpulse();
        }
        
        
        isKnockBack = true;
        KnockBackTask().Forget();
    }

    async UniTaskVoid KnockBackTask()
    {
        float timer = 0.67f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (!isKnockBack) break;
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        }
        isKnockBack = false;
    }

    async UniTaskVoid IgnoreTask()
    {
        float timer = ignoreTime;
        isIgnore = true;
        ColorTask().Forget();
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        }
        isIgnore = false;
    }
    async UniTaskVoid ColorTask()
    {
        int tick = 0;
        float timer = 0.5f;
        while (isIgnore)
        {
            tick++;
            timer = 0.1f;
            _playerAnimation.SetColor(tick%2==0?Color.white:new Color(1,0,0,0.8f));
            while (timer > 0)
            {
                if (!isIgnore) break;
                timer -= Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            }
        }
        _playerAnimation.SetColor(Color.white);
    }
    async UniTaskVoid GameOverTask() //게임오버 
    {
        GetComponent<BoxCollider2D>().enabled = false;
        float timer = 3;
        while (timer > 0)
        {
            timer -= 0.1f;
            transform.rotation = Quaternion.Euler(0, 0,transform.eulerAngles.z+6f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            soundstop = true; 
        }
        //TODO : 게임오버 연출
        SceneManager.LoadScene("stage4");
    }
    void PlatformEnter(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Ground") ||
             other.gameObject.CompareTag("ColoredPlatform") ||
             other.gameObject.CompareTag("DropPlatform") ||
             other.gameObject.CompareTag("Platform")))
        {
          
           

            _isjumping = false;
            jumpCount = 0;
            _playerAnimation.SetAnimation(PlayerAnimation.Animations.idle,true);
            isKnockBack = false;
        }
        if (other.transform.CompareTag("DropPlatform") && other.transform.position.y < transform.position.y)
        {
            other.transform.GetComponent<DroppedPlatform>().Dropped().Forget();
            _playerAnimation.SetAnimation(PlayerAnimation.Animations.idle,true);
            isKnockBack = false;
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
            GetComponent<PlayerAct>().GetSpray(Getspray);
            SFXInstance.setVolume(1);
            SFXInstance.setParameterByName("PlayerState", 4.0f);
            SFXInstance.start();
        }
        if (other.CompareTag("CameraCollider"))
        {
            Camera.main.GetComponent<CameraChanger>().UpdateCamera(other.GetComponent<CameraCollider>().colliderID);
        }

        if (other.CompareTag("BossAttack")&&!isIgnore)
        {
            BossStageHp--;
            var contactPos = other.ClosestPoint(transform.position);
            if (BossStageHp > 0)
            {
                BossStageCamera.Instance.Shake(0.04f,0.04f,0,1,100);
                GetKnockBack(contactPos, 10.79f);
            }
            else
            {
                BossStageCamera.Instance.Shake(0.12f,0.12f,0,1f,100);
                GetKnockBack(contactPos, 20,true);
                GameOverTask().Forget();
            }
        }
        if (other.CompareTag("FallDownCollider"))
        {
            BossStageCamera.Instance.Shake(0.12f,0.12f,0,1f,100);
            GetKnockBack(other.ClosestPoint(transform.position), 20,true);
            GameOverTask().Forget();
        }

        if (other.CompareTag("death"))
        {
            DeathInstance.setVolume(2.0f);
            DeathInstance.start();
            soundstop = true;
            DeathTask().Forget();
        }

    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "GateKepper (1)")
        {
            Debug.Log("시발");
            deathfromkeeper();
        }
    }
    */

    public static void unlockfreeze()
    {
        freeze = false; 
    }

   public async UniTaskVoid DeathTask()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        SceneManager.LoadScene("Stage3");
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        soundstop = false; // 사운드멈춤취소      
    }

}
