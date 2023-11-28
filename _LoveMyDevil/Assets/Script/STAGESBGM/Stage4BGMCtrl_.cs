using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.SceneManagement;

public class Stage4BGMCtrl_ : MonoBehaviour
{
    bool isenddial = false; 



    int checkGraffiti;

    [FMODUnity.EventRef]
    public string BgmCtrl;  
    private FMOD.Studio.EventInstance BGMInstance;

    [FMODUnity.EventRef]
    public string SFXCtrl;
    private FMOD.Studio.EventInstance SFXInstance;

    [FMODUnity.EventRef]
    public string BgmCtrl2;
    private FMOD.Studio.EventInstance BGMInstance2;


    // Start is called before the first frame update
    protected void Start()
    {
        BGMInstance = FMODUnity.RuntimeManager.CreateInstance(BgmCtrl);
        SFXInstance = FMODUnity.RuntimeManager.CreateInstance(SFXCtrl);
        BGMInstance2 = FMODUnity.RuntimeManager.CreateInstance(BgmCtrl2);

       
        BGMInstance.start();
        BGMInstance2.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);


       

    }

     void OnEnable()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadedsceneEvent;
    }

    // Update is called once per frame
    void Update()
    {
        checkGraffiti = TypingManager.instance.inputcount;

        if (isenddial == false)
        {
            if (checkGraffiti == 16)
            {
                sfxstartbgmstop();
            }
        }

        if(PlayerMove.soundstop == true)
        {
            BGMInstance2.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }


    void sfxstartbgmstop()
    {
        isenddial = true;

        
        StartCoroutine(sfxtillsoundoff());
    }

    IEnumerator sfxtillsoundoff()
    {
        SFXInstance.start();
        yield return new WaitForSeconds(0.18f);
        BGMInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield return new WaitForSeconds(0.3f);
        BGMInstance2.start();
        yield return new WaitForSeconds(0.2f);
        SFXInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        
        
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        isenddial = false;
        StopAllCoroutines();
        BGMInstance2.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }


}
