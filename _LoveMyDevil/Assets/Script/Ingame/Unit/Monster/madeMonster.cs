using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.SceneManagement;


public class madeMonster : MonoBehaviour
{
    Transform transform;


    string mapname;

    [FMODUnity.EventRef]
    public string SFXCtrl;

    private FMOD.Studio.EventInstance SFXInstance;

    enum State
    {
        NonTargeting,
        Targeting,
        Finale
    }

    private State madeState = State.NonTargeting;
    [Header("인식범위 객체")] [SerializeField] ColliderCallbackController targetCallbackCollider;
    [Header(" ")] [SerializeField] private GameObject _body;
    [Header("속도")] [SerializeField] private float speed;
    int nextMove = 1;

    private GameObject player;

    private MaidAnimation _animation;

    private bool isDial=false;
    //Rigidbody2D 
    void Start()
    {
        targetCallbackCollider.onColliderEnter += OnTriggerEnterTargetCollider;
        targetCallbackCollider.onColliderExit += OnTriggerExitTargetCollider;
        _body.GetComponent<ColliderCallbackController>().onColliderEnter += OnTriggerEnterBodyCollider;
       // _body.GetComponent<ColliderCallbackController>().onCollisionEnter += OnCollisionEnterBodyCollider;
        _animation = GetComponent<MaidAnimation>();
        _animation.SetAnimation(MaidAnimation.States.idle,true);

        SFXInstance = FMODUnity.RuntimeManager.CreateInstance(SFXCtrl);
       
        transform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        mapname = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        switch (madeState)
        {
            case State.NonTargeting:
                NonTargetedMove();
                break;
            case State.Targeting:
                if(isTargeted)TargetedMove();
                break;
        }

        if (isDial && _animation.NowStates == MaidAnimation.States.cry&&TypingManager.instance.inputcount==11)
        {
            _animation.SetAnimation(MaidAnimation.States.siittingCry,true);
        }
    }

    void NonTargetedMove()
    {
        // bool isCollider = false;
        // _body.transform.position += new Vector3((speed * Time.deltaTime) * nextMove, 0);
        // var hits = Physics2D.RaycastAll(_body.transform.position + new Vector3(0, 0.5f), new Vector2(nextMove, 0),
        //     nextMove * Time.deltaTime);
        // foreach (var hit in hits)
        // {
        //     Debug.Log(hit.transform.tag);
        //     if (hit.transform.CompareTag("MadeTargetCollider"))
        //         isCollider = true;
        // }
        //
        // if (!isCollider)
        // {
        //     nextMove *= -1;
        //     _body.transform.localScale = nextMove > 0 ? new Vector3(-1, 1) : new Vector3(1, 1);
        // }

    }

    void TargetedMove()
    {
        bool isCollider = false;
        if (Mathf.Abs(_body.transform.position.x - player.transform.position.x) >= 0)
        {
            _body.transform.Translate(
                new Vector3(speed * (player.transform.position.x > _body.transform.position.x ? 1 : -1), 0) *
                Time.deltaTime);
        }

        if (player.transform.position.x < _body.transform.position.x && _body.transform.localScale.x < 0)
        {
            _body.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (player.transform.position.x > _body.transform.position.x && _body.transform.localScale.x > 0)
        {
            _body.transform.localScale = new Vector3(-1, 1, 1);
        }

        var hits = Physics2D.RaycastAll(_body.transform.position + new Vector3(0, 0.5f), new Vector2(nextMove, 0),
            nextMove * Time.deltaTime);
        foreach (var hit in hits)
        {
            if (hit.transform.CompareTag("MadeTargetCollider"))
                isCollider = true;
        }

        if (!isCollider)
        {
            madeState = State.NonTargeting;
        }
    }

    private bool isTargeted = false;
    //인식범위 객체의 충돌처리
    private void OnTriggerEnterTargetCollider(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SFXInstance.setParameterByName("Maid_pa", 2.0f);
            if(!isTargeted)
                TargetedAnimationTask().Forget();
            SFXInstance.start();
            madeState = State.Targeting;
            player = other.gameObject;
        }

        if (other.CompareTag("3"))
        {
            transform.position = new Vector3(0, 100, 0);
        }
    }


private void OnTriggerExitTargetCollider(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(madeState == State.Targeting)
                madeState = State.NonTargeting;
            SFXInstance.setParameterByName("Maid_pa", 1.0f);
            SFXInstance.start();
        }
    }
    //body의 충돌처리
    private void OnTriggerEnterBodyCollider(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (mapname == "Stage2")
            {
                texter2.Instance.maidcol = true;
                speed = 0;
                if(_animation)
                    _animation.SetAnimation(MaidAnimation.States.cry,true);
                else
                {
                    Debug.Log("_animation 없음");
                }
                isDial = true;
            }

            if (mapname == "Stage3")
            {
                texter3.Instance.maidcol = true;
                speed = 0;
            }
            _body.transform.localScale = new Vector3(1, 1, 1);
        }
        
    }

    async UniTaskVoid TargetedAnimationTask()
    {
        _animation.SetAnimation(MaidAnimation.States.targeted);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        _animation.SetAnimation(MaidAnimation.States.run,true);
        isTargeted = true;
    }


    
}
