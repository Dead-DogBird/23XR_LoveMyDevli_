using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ColoredPlatform : MonoBehaviour
{

    public float maxHeight = 1.0f; // 최대 높이
    private float initialYOffset = -0.5f; // 초기 Offset Y 값
    private float initialSizeY = 0.0f; // 초기 Y 축 크기
    private float ratio; //비율

    private BoxCollider2D _collider;
    private Vector3 _scale;
    private float max = 6;
    private float curfillingAmount = 0;
    [SerializeField] private GameObject _mask;
    private SpriteRenderer _maskSprite;
    [Header("사라지는 속도(기본값 : 0.1)")]
    [SerializeField] private float disappearFigure = 0.1f;
    [Header("채워지는 속도(기본값 : 0.1)")] 
    [SerializeField] private float fillFigure = 0.1f;

    [Header("사라질 때 까지 걸리는 시간(초 단위)")]
    [SerializeField] private float disappearDelay = 2.5f;
    private bool isDie = false;

    private bool isDone = false;

    [SerializeField] private Sprite[] sprites;
    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = false;
        _scale = transform.localScale;
        ratio = (0 - initialYOffset) / (1 - initialSizeY);
        _maskSprite = _mask.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        OnSprayHit();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void OnSprayHit()
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, _collider.size, 0);
        foreach (Collider2D i in hit)
        {
            if (i.CompareTag("MouseCollider"))
            {
                PaintedPlatform(true,fillFigure);
                if (!isDisapper)
                    disappearFillAmount().Forget();
                if (!isDelay)
                    TaskDelay().Forget();
            }
        }
    }

    private int currentIntAmount;
    bool PaintedPlatform(bool addAmount = true,float fillcount = 0.1f)
    {
        curfillingAmount += addAmount ? fillcount : -fillcount;
        if (curfillingAmount > max)
            curfillingAmount = max;
        if (curfillingAmount < 0)
            curfillingAmount = 0;
        if (currentIntAmount != (int)MathF.Floor(curfillingAmount))
        {
            currentIntAmount = (int)MathF.Floor(curfillingAmount);
            SetSprite(currentIntAmount,addAmount);
        } 
        return true;
    }

    private bool isDisapper = false;
    private bool isDelay;
    async UniTaskVoid disappearFillAmount()
    {
        isDisapper = true;
        while (true)
        {
            if (isDie) return;
            if (!isDelay)
            { if (!PaintedPlatform(false, disappearFigure)) break; }
            await UniTask.Delay(TimeSpan.FromSeconds(0.02f));
        }
        isDisapper = false;
    }

    async UniTaskVoid TaskDelay()
    {
        isDelay = true;
        float timer = disappearDelay;
        while (timer > 0)
        {
            if (isDie) return;
            timer -= 0.1f;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
        isDelay = false;
    }

    void SetSprite(int i,bool isAdd)
    {
        Debug.Log(i);
        if (i < 1 && i > sprites.Length) return;
        if (isAdd)
        {
            _maskSprite.sprite = sprites[i-1];
            if(i-1>=0)
                _collider.enabled = true;
        }
        else
        {
            if(sprites.Length - (i - 1)>6)
                _collider.enabled = false;
            if (sprites.Length - (i - 1) < sprites.Length)
            {
                _maskSprite.sprite = sprites[^(i - 1)];
            }
            else
            {
                _maskSprite.sprite = sprites[^1];
                
            }
        }
    }
    
}