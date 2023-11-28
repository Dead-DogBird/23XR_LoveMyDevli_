using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int graffitiactive1 = 0;
    public int clear = 0; 

    public event EventHandler OnMakeOverGraffiti;
    
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

    public bool GetProgress
    {
        get
        {
            return nowPoints >= allPoints;
        }
    }

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
        if(CurStage==Stage.stage2&&nowPoints / allPoints*100>50f)
            DialogManagerv.Instance.SetID(3);
        if (nowPoints >= allPoints)
            GetNextStage().Forget();
    }

    async UniTaskVoid GetNextStage()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        OnMakeOverGraffiti?.Invoke(this, EventArgs.Empty);
        if (CurStage == Stage.stage1) return;
        _player.GetComponent<Rigidbody2D>().simulated = false;
        
        
        switch (CurStage)
        {
            case Stage.stage2:
                clear = 1; 
                await UniTask.Delay(TimeSpan.FromSeconds(3f));
                UImanager.Instance.Fade(false);
                await UniTask.Delay(TimeSpan.FromSeconds(3f));
                LoadingSceneManager.LoadScene("Stage3",2);
                break;
            case Stage.stage3:
                clear = 1;
                await UniTask.Delay(TimeSpan.FromSeconds(10f));
                UImanager.Instance.Fade(false);
                await UniTask.Delay(TimeSpan.FromSeconds(3f));
                LoadingSceneManager.LoadScene("Stage4",3);
                break;
            case Stage.stage4:
                clear = 1;
                await UniTask.Delay(TimeSpan.FromSeconds(3f));
                UImanager.Instance.Fade(false);
                await UniTask.Delay(TimeSpan.FromSeconds(3f));
                LoadingSceneManager.LoadScene("TitleScene",4);
                break;
        }

    }

}
