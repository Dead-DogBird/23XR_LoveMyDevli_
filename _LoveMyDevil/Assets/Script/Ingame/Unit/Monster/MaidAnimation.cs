using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class MaidAnimation : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _animation;
    public enum States
    {
        idle,
        targeted,
        cry,
        siittingCry,
        run
    }
    public States NowStates { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAnimation(States _animations, bool isloop=false)
    {
        string animationName ="";
        NowStates = _animations;
        switch (NowStates)
        {
            case States.idle:
                animationName = "animation1";
                break;
            case States.targeted:
                animationName = "animation2";
                break;
            case States.cry:
                animationName = "animation3";
                break;
            case States.siittingCry:
                animationName = "animation4";
                break;
            case States.run:
                animationName = "animation5";
                break;
        }
        _animation.AnimationName = animationName;
        _animation.loop = isloop;
    }
}
