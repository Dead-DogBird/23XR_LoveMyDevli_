using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class Stage3Bgm : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string BgmCtrl;

    [FMODUnity.EventRef]
    public string SFXCtrl;


    int checkGraffiti;

    bool sound1 = true; 

    private FMOD.Studio.EventInstance BGMInstance;
    private FMOD.Studio.EventInstance SFXInstance; 

  private void OnEnable()
    {
        SceneManager.sceneLoaded += OncseneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OncseneLoaded;
    }


    // Start is called before the first frame update
   protected void Start()
    {
        BGMInstance = FMODUnity.RuntimeManager.CreateInstance(BgmCtrl);
        // BGMInstance.start();
        SFXInstance = FMODUnity.RuntimeManager.CreateInstance(SFXCtrl);


        Debug.Log(PlayerMove.soundstop);

        
        BGMInstance.start();
        BGMInstance.setParameterByName("Parameter 3", 7.0f);
        

      
        BGMInstance.setVolume(0.5f);

        
    }

  

    // Update is called once per frame
    void Update()
    {
        if(PlayerMove.soundstop == true)
        {
            Debug.Log("¤·¤·");
            BGMInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }


        if(sound1 == true)
        {
            if(TypingManager.instance.inputcount == 18)
            {
                
                check1();
            }
        }



        if (GameManager.Instance.graffitiactive1 == 1)
        {
            BGMInstance.setParameterByName("Parameter 3", 1.0f);
        }

        if (GameManager.Instance.graffitiactive1 == 2)
        {
            BGMInstance.setParameterByName("Parameter 3", 2.0f);
        }

        if (GameManager.Instance.graffitiactive1 == 3)
        {
            BGMInstance.setParameterByName("Parameter 3", 3.0f);
        }

        if (GameManager.Instance.graffitiactive1 == 4)
        {
            BGMInstance.setParameterByName("Parameter 3", 4.0f);
        }

        if (GameManager.Instance.graffitiactive1 == 5)
        {
            BGMInstance.setParameterByName("Parameter 3", 5.0f);
        }

        if (GameManager.Instance.graffitiactive1 == 6)
        {
            BGMInstance.setParameterByName("Parameter 3", 6.0f);
        }

      

        if (GameManager.Instance.clear == 1)
        {
            BGMInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

    }

    void check1()
    {
        sound1 = false;
        BGMInstance.setParameterByName("Parameter 3", 0.0f);
        StartCoroutine(SwapSound());
    }

    IEnumerator SwapSound()

    {  SFXInstance.start();
        yield return new WaitForSeconds(0.2f);
        BGMInstance.setVolume(0);
       
        
        yield return new WaitForSeconds(0.5f);
        BGMInstance.setVolume(0.5f);
    }

    private void OncseneLoaded(Scene scene, LoadSceneMode mode)
    {

       
        BGMInstance.setParameterByName("Parameter 3", 7.0f);
        BGMInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        StopAllCoroutines();


    }
}
