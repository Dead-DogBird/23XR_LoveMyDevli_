using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _animation;
    public enum Animations
    {
        idle,
        rightrun,
        leftrun,
        rightjump,
        leftjump,
        doublejump,
        hit,
        successes
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
            case Animations.leftrun:
                animationName = "animation2";
                break;
            case Animations.leftjump:
                animationName = "animation4-1";
                break;
            case Animations.rightrun:
                animationName = "animation2-1";
                break;
            case Animations.rightjump:
                animationName = "animation4-1-1";
                break;
            case Animations.doublejump:
                animationName = "animation4-3";
                break;
            case Animations.hit:
                animationName = "animation5-1";
                break;
            case Animations.successes:
                animationName = "animation6";
                break;
        }
        _animation.AnimationName = animationName;
        _animation.loop = isloop;
    }
}
