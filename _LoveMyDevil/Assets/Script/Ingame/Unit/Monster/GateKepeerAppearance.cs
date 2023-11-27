using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;
using FMOD.Studio; 


public class GateKepeerAppearance : MonoBehaviour
{
    public float timectrl; 

    bool issound = false; 

    [FMODUnity.EventRef]
    public string SFXCtrl;

    private FMOD.Studio.EventInstance SFXInstance;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        SFXInstance = FMODUnity.RuntimeManager.CreateInstance(SFXCtrl);
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
            if(issound == false)
            {
                soundset();
            }
        }

        if (TypingManager.instance.inputcount == 18&&!isAniEnd)
        {
            GetStart().Forget();
        }
    }

    void soundset()
    {
        issound = true;
        StartCoroutine(timecheck());
    }

    IEnumerator timecheck()
    {
        yield return new WaitForSeconds(timectrl);
        SFXInstance.setVolume(2.0f);
        SFXInstance.setParameterByName("keepersound", 0.0f);
        SFXInstance.start();
    }

    async UniTaskVoid GetStart()
    {
        isAniEnd = true;
        await UniTask.Delay(TimeSpan.FromSeconds(6));
        gameObject.SetActive(false);
        Keepper.gameObject.SetActive(true);
    }
}
