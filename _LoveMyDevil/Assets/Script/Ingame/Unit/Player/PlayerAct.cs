using Cysharp.Threading.Tasks;
using Spine.Unity.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class PlayerAct : MonoBehaviour
{
    //ÇÁ¸®Â¡ 
    public static bool freeze = true; 


    [SerializeField] private GameObject spray;
    [SerializeField] private Transform mousePointer;
    [SerializeField] private bool isBossStage=false;
    private PlayerContrl _playerContrl;
    
    [SerializeField] private float _sprayGauge = 100;
    private float maxGauge;
    private float sprayGauge
    {
        get => _sprayGauge;
        set
        {
            _sprayGauge = value;
            UImanager.Instance.SetSprayGauge(_sprayGauge/maxGauge*100);
        }
    }
    private bool fillGauge;
    private bool isWaitForfillGauge;
    private CircleCollider2D _mouseCollider;
    private bool isFirst = true;
    private GameObject nowSpray;

    private Player_Effect _playerEffect;
    void Start()
    {
        _playerContrl = GetComponent<PlayerContrl>();
        _mouseCollider = mousePointer.GetComponent<CircleCollider2D>();
        _playerEffect = GetComponent<Player_Effect>();
        maxGauge = _sprayGauge;
        if (isBossStage)
            _sprayGauge = 0;
        _playerEffect.SprayEffect.transform.parent = mousePointer;
        _playerEffect.SprayEffect.transform.localPosition = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (freeze == false)
        {
            if (_playerContrl.Userinput.LeftMouseState)
                Spray();
            else if (!isWaitForfillGauge)
            {
                FillGaugeTask().Forget();
            }
            if (Input.GetMouseButtonUp(0))
            {
                isFirst = true;
                isWaitForfillGauge = false;
                if (nowSpray)
                {
                    nowSpray.transform.parent = null;
                    nowSpray = null;
                    _mouseCollider.enabled = false;
                }
            }

            if (nowSpray)
            {
                _playerEffect.SprayEffect.SetActive(true);
                _mouseCollider.enabled = true;
            }
            else
            {
                _playerEffect.SprayEffect.SetActive(false);
                _mouseCollider.enabled = false;
            }
        }
    }
    void Spray()
    {
        if (sprayGauge > 0)
        {
            if (isFirst)
            {
                isFirst = false;
                nowSpray = Instantiate(spray, mousePointer.transform, true);
                nowSpray.transform.position = mousePointer.transform.position;
                
            }

            isWaitForfillGauge = false;
            sprayGauge -= 0.2f;
        }
        else
        {
            isFirst = true;
            if(nowSpray)
            {
                nowSpray.transform.parent = null;
                nowSpray = null; 
                _mouseCollider.enabled = false;
                _playerEffect.SprayEffect.SetActive(false);
            }
        }
    }
    async UniTaskVoid FillGaugeTask()
    {
        if (isBossStage) return;
        isWaitForfillGauge = true;
        for (int i = 0; i < 50; i++)
        {
            if (_playerContrl.Userinput.LeftMouseState)
            {
                isWaitForfillGauge = false;
                return;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
        }
        while (sprayGauge < maxGauge)
        {
            if (_playerContrl.Userinput.LeftMouseState)
            {
                isWaitForfillGauge = false;
                return;
            }
            sprayGauge += 0.5f;
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
        sprayGauge = maxGauge;
    }

    public void GetSpray(float filled)
    {
        sprayGauge += maxGauge * (filled / 100);
        if (sprayGauge > maxGauge)
            sprayGauge = maxGauge;
    }

    public static void unlockspray()
    {
        freeze = false; 
    }
}
