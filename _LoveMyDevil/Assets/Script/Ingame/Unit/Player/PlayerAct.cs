using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class PlayerAct : MonoBehaviour
{
    [SerializeField] private GameObject spray;
    [SerializeField] private Transform mousePointer;

    private PlayerContrl _playerContrl;
    
    private float _sprayGauge = 100;
    private float sprayGauge
    {
        get => _sprayGauge;
        set
        {
            _sprayGauge = value;
            UImanager.Instance.SetSprayGauge(_sprayGauge);
        }
    }
    private bool fillGauge;
    private bool isWaitForfillGauge;
    private CircleCollider2D _mouseCollider;
    private bool isFirst;
    private GameObject nowSpray;
    void Start()
    {
        _playerContrl = GetComponent<PlayerContrl>();
        _mouseCollider = mousePointer.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (_playerContrl.Userinput.LeftMouseState)
            Spray();
        else if (!isWaitForfillGauge)
        {
            FillGaugeTask().Forget();
        }
        if(Input.GetMouseButtonUp(0))
        {
            isFirst = true;
            isWaitForfillGauge = false;
            if(nowSpray)
            {
                nowSpray.transform.parent = null;
                nowSpray = null; 
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
                _mouseCollider.enabled = true;
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
            }
        }
    }
    async UniTaskVoid FillGaugeTask()
    {
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
        while (sprayGauge < 100)
        {
            if (_playerContrl.Userinput.LeftMouseState)
            {
                isWaitForfillGauge = false;
                return;
            }
            sprayGauge += 0.5f;
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
        sprayGauge = 100;
    }

}
