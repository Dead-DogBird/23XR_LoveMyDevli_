using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Stage2_LastLine : MonoBehaviour
{
    private SpriteRenderer _sprite;
    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.color = Color.clear;
        GameManager.Instance.OnMakeOverGraffiti += (sender, args) => GetMakeOverGraffiti();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetMakeOverGraffiti()
    {
        Onfade().Forget();
    }

    async UniTaskVoid Onfade()
    {
        while (_sprite.color.a <= 0.999f)
        {
            _sprite.color += (Color.white - _sprite.color) * Time.deltaTime * 3;
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        }
    }
    
}
