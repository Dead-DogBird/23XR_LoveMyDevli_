using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Spine.Unity;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _animation;
    public CancellationTokenSource cancelToken;
    public enum Animations
    {
        idle,
        Attack,
        Attack2
    }

    public Animations nowAnimation { get; private set; }

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnimation(Animations _animations, bool isloop=false)
    {
        string animationName ="";
        nowAnimation = _animations;
        switch (_animations)
        {
            case Animations.idle:
                animationName = "animation1";
                break;
            case Animations.Attack:
                animationName = "animation2";
                CallBackAnimation().Forget();
                break;
            case Animations.Attack2:
                animationName = "animation2";
                CallBackAnimation().Forget();
                break;
        }
        _animation.AnimationName = animationName;
        _animation.loop = isloop;
    }
    async UniTaskVoid CallBackAnimation()
    {
        switch (nowAnimation)
        {
            case Animations.Attack:
                await UniTask.Delay(TimeSpan.FromSeconds(0.8f),cancellationToken: cancelToken.Token);
                break;
            case Animations.Attack2:
                await UniTask.Delay(TimeSpan.FromSeconds(0.833f),cancellationToken: cancelToken.Token);
                break;
        }
        _animation.AnimationName = "animation1";
        _animation.loop = true;
    }
}
