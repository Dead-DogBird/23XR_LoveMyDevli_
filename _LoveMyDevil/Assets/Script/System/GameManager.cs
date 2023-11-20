using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int graffitiactive1 = 0;

    
    public float progress;
    internal PoolingManager _poolingManager;

    private float allPoints;
    private float nowPoints;

    public enum Stage
    {
        stage1,
        stage2,
        stage3,
        stage4
    }
    [SerializeField] Stage CurStage;
    public GameObject _player { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPlayer(GameObject player)
    {
        _player = player;
    }
    public void SetPoint()
    {
        allPoints++;
        UImanager.Instance.SetStageProgress(0);
    }

    public void GetPoint()
    {
        nowPoints++;
        graffitiactive1 += 1;
        UImanager.Instance.SetStageProgress(nowPoints / allPoints);
        if (nowPoints >= allPoints)
            GetNextStage().Forget();
    }

    async UniTaskVoid GetNextStage()
    {
        if (CurStage == Stage.stage1) return;
        _player.GetComponent<Rigidbody2D>().simulated = false;
        await UniTask.Delay(TimeSpan.FromSeconds(1.6f));
        switch (CurStage)
        {
            case Stage.stage2:
                LoadingSceneManager.LoadScene("Stage3",2);
                break;
            case Stage.stage3:
                LoadingSceneManager.LoadScene("Stage4",3);
                break;
        }

    }

}
