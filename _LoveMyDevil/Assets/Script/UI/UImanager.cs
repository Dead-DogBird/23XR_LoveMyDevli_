using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UImanager : MonoSingleton<UImanager>
{
    [SerializeField] private Image sprayGauge;
    [SerializeField] private Text sprayGaugeText;

    [SerializeField] private Text StageProgress;

    [SerializeField] private Image sprayGaugeBg;
    private Camera _camera;
    private bool isplayer;

    private bool isOnGaugeTask;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        GetPlayerTask().Forget();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isplayer)
        {
            sprayGaugeBg.transform.position =
                _camera.WorldToScreenPoint(GameManager.Instance._player.transform.position-new Vector3(0.5f,-0.5f));
        }
    }
    public void SetSprayGauge(float _gauge)
    {
        if (Math.Abs(_gauge - 100) <= 0.01f)
        {
            SprayGaugeTask().Forget();
        }
        else if (!sprayGaugeBg.gameObject.activeSelf)
        {
            sprayGaugeBg.gameObject.SetActive(true);
        }
        else
        {
            if (isOnGaugeTask)
                isOnGaugeTask = false;
        }
        sprayGaugeText.text = $"Spray : {_gauge:F0}%";
        sprayGauge.fillAmount = _gauge/100f*0.68f;
    }

    public void SetStageProgress(float progress)
    {
        StageProgress.text = $"맵 진행 : {progress*100:F1}";
    }

   
    async UniTaskVoid GetPlayerTask()
    {
        await UniTask.WaitUntil(() => GameManager.Instance._player);
        isplayer = true;
    }

    async UniTaskVoid SprayGaugeTask()
    {
        isOnGaugeTask = true;
        for (int i = 0; i < 10; i++)
        {
            if (!isOnGaugeTask)
            {
                return;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
        sprayGaugeBg.gameObject.SetActive(false);
    }
}
