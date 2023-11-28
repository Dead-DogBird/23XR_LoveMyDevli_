using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using FMOD.Studio;
using FMODUnity;
using static UnityEngine.UI.Button;
using UnityEngine.SceneManagement;

public class UImanager : MonoSingleton<UImanager>
{




    [SerializeField] private Image sprayGauge;
    [SerializeField] private Text sprayGaugeText;

    [SerializeField] private Text StageProgress;
    [SerializeField] private Image StageProgressImg;
    [SerializeField] private Image sprayGaugeBg;
    [SerializeField] private Image FadeImg;
    private Camera _camera;
    private bool isplayer;
    public bool isBossStage;
    private bool isOnGaugeTask;
    // Start is called before the first frame update
    void Start()
    {

        FadeImg.color = Color.black;
        _camera = Camera.main;
        GetPlayerTask().Forget();
        if(isBossStage)
            SprayGaugeTask().Forget();
        Fade(true).Forget();
    }
    void Update()
    {      

    }

    private void FixedUpdate()
    {
        if (isplayer)
        {
            sprayGaugeBg.transform.position +=(
                _camera.WorldToScreenPoint(GameManager.Instance._player.transform.position-new Vector3(0.5f,-0.5f))-sprayGaugeBg.transform.position) * (Time.deltaTime * 20);
        }
    }

    public void SetSprayGauge(float _gauge)
    {
        if (Math.Abs(_gauge - 100) <= 0.01f&&!isBossStage)
        {
            SprayGaugeTask().Forget();
        }
        else if (!sprayGaugeBg.gameObject.activeSelf)
        {
            sprayGaugeBg.gameObject.SetActive(true);
        }
        if(isBossStage)
            SprayGaugeTask().Forget();
        else
        {
            if (isOnGaugeTask)
                isOnGaugeTask = false;
        }
        sprayGaugeText.text = $"{_gauge:F0}%";
        sprayGauge.fillAmount = _gauge/100f*0.68f;
    }

    public void SetStageProgress(float progress)
    {
        StageProgress.text = $"{progress*100:F1}";
        StageProgressImg.fillAmount = progress;
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

    public async UniTaskVoid Fade(bool isIn)
    {
        if (isIn)
        {
            while (FadeImg.color.a > 0.01f)
            {
                FadeImg.color += (Color.clear - FadeImg.color) * Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            }
            FadeImg.color = Color.clear;
        }
        else
        {
            while (FadeImg.color.a < 0.99f)
            {
                FadeImg.color += (Color.black - FadeImg.color) * Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            }
            FadeImg.color = Color.black;
        }
    }
}
