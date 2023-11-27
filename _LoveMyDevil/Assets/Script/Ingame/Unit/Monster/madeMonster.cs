using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class madeMonster : MonoBehaviour
{
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

    //Rigidbody2D 
    void Start()
    {
        targetCallbackCollider.onColliderEnter += OnTriggerEnterTargetCollider;
        targetCallbackCollider.onColliderExit += OnTriggerExitTargetCollider;
        _body.GetComponent<ColliderCallbackController>().onColliderEnter += OnTriggerEnterBodyCollider;


        SFXInstance = FMODUnity.RuntimeManager.CreateInstance(SFXCtrl);
    }

    void Update()
    {
        switch (madeState)
        {
            case State.NonTargeting:
                NonTargetedMove();
                break;
            case State.Targeting:
                TargetedMove();
                break;
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

    //인식범위 객체의 충돌처리
    private void OnTriggerEnterTargetCollider(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SFXInstance.setParameterByName("Maid_pa", 2.0f);
            SFXInstance.start();
            madeState = State.Targeting;
            player = other.gameObject;
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
            //TODO : 다이얼로그 출력
        }
    }
}
