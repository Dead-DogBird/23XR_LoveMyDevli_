using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GateKepeerAppearance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private bool aniKey = false;

    private Animator _animator;
    
    
    private bool isAniEnd;

    [SerializeField] private GameObject Keepper;
    // Update is called once per frame
    void Update()
    {
        if (TypingManager.instance.inputcount == 7&&!aniKey)
        {
            aniKey = true;
            _animator.Play("appearance");
        }

        if (TypingManager.instance.inputcount == 18&&!isAniEnd)
        {
            GetStart().Forget();
        }
    }

    async UniTaskVoid GetStart()
    {
        isAniEnd = true;
        await UniTask.Delay(TimeSpan.FromSeconds(6));
        gameObject.SetActive(false);
        Keepper.gameObject.SetActive(true);
    }
}
