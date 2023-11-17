using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int graffitiactive1 = 0;


    public float progress;
    internal PoolingManager _poolingManager;

    private float allPoints;
    private float nowPoints;

    public GameObject _player { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

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
    }

}
