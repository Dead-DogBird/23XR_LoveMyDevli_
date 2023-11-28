using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
using FMOD.Studio;
using FMODUnity;
using Spine.Unity;

public class GhostMonster : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string SFXCtrl;

    private FMOD.Studio.EventInstance SFXInstance;

    [SerializeField] private GameObject _body;
    [SerializeField] private ColliderCallbackController _checkCollider;

    [Header("속도")] [SerializeField] private float _speed;
    [Header("넉백 강도")] [SerializeField] private float _knockbackFos;
    private Rigidbody2D _bodyRigid;
    private bool isDie;
    private State ghostState;
    private Vector3 SpawnPos;
    
    private GameObject player;
    private Vector3 toPos;
    private int nextMove = 1;
    [SerializeField] private SkeletonAnimation _animation;
    enum State
    {
        NonTargeted,
        Targeted,
        BackSpawn
    }
    private void OnDestroy()
    {
        isDie = true;
    }

    void Start()
    {
        _checkCollider.onColliderEnter += OnCheckTriggerEnter;
        _checkCollider.onColliderExit += OnCheckTriggerExit;
        
        _body.GetComponent<ColliderCallbackController>().onColliderEnter += OnBodyTriggerEnter;
        _body.GetComponent<ColliderCallbackController>().onColliderExit += OnBodyTriggerExit;
        _body.GetComponent<ColliderCallbackController>().onCollisionEnter += OnBodyCollisionEnter;

        _bodyRigid = _body.GetComponent<Rigidbody2D>();
        SpawnPos = _body.transform.position;
        MoveSelect().Forget();

        SFXInstance = FMODUnity.RuntimeManager.CreateInstance(SFXCtrl);
        SFXInstance.setVolume(2.0f);
    }

    void Update()
    {
        switch (ghostState)
        {
            case State.NonTargeted:
                NonTargetedMove();
                break;
            case State.Targeted:
                TargetedMove();
                break;
            
            case State.BackSpawn:
                BackSpawn();
                break;
        }
    }

    void NonTargetedMove()
    {
        _bodyRigid.velocity = new Vector2(nextMove, 0);
    }
    void TargetedMove()
    {
        if (Mathf.Abs(_body.transform.position.x - player.transform.position.x) >= 0)
        {
            toPos = (player.transform.position - _body.transform.position).normalized;
            _body.transform.Translate(toPos * (_speed * Time.deltaTime)); 
        }
        if (player.transform.position.x < _body.transform.position.x && _body.transform.localScale.x < 0)
        {
            _body.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (player.transform.position.x > _body.transform.position.x && _body.transform.localScale.x > 0)
        {
            _body.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void BackSpawn()
    {
        toPos = (SpawnPos - _body.transform.position).normalized;
        _body.transform.Translate(toPos * (_speed * Time.deltaTime));
        if (Vector2.Distance(_body.transform.position,SpawnPos) <= 0.1f)
        {
            ghostState = State.NonTargeted;
        }
    }
    async UniTaskVoid MoveSelect()
    {
        while (!isDie)
        {
            nextMove = Random.Range(-1, 2);
            _body.transform.localScale = nextMove > 0 ? new Vector3(1, 1) : new Vector3(-1, 1);
            await UniTask.Delay(TimeSpan.FromSeconds(5), ignoreTimeScale: false);
            await UniTask.WaitUntil(() => ghostState==State.NonTargeted);
        }
    }
    
    //body콜라이더의 충돌 처리
    void OnBodyTriggerEnter(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerMove>().GetKnockBack(_body.transform.position,_knockbackFos);
        }
    }

    void OnBodyCollisionEnter(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerMove>().GetKnockBack(_body.transform.position,_knockbackFos);
        }
    }
    void OnBodyTriggerExit(Collider2D other)
    {
        if (other.CompareTag("GhostCheckCollider"))
        {
            ghostState = State.BackSpawn;
        }
    }
    //check콜라이더의 충돌처리
    void OnCheckTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SFXInstance.start();
            ghostState = State.Targeted;
            player = other.gameObject;
            _animation.AnimationName = "animation2";
            _animation.loop = true;
        }
        if (other.CompareTag("3"))
        {
            Destroy(gameObject);
        }
    }
    void OnCheckTriggerExit(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animation.AnimationName = "animation1";
            _animation.loop = true;
            SFXInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
    
    
}
