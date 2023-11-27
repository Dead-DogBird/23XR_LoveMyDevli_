using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


public class Stage2BgmCtrl : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string BgmCtrl;

    int checkGraffiti;

    private FMOD.Studio.EventInstance BGMInstance;




    // Start is called before the first frame update
    void Start()
    {
        BGMInstance = FMODUnity.RuntimeManager.CreateInstance(BgmCtrl);
        BGMInstance.start();

        //stop ±¸Çö X 

        BGMInstance.setVolume(0.5f);
    }

    // Update is called once per frame
    void Update()
    {


        if (GameManager.Instance.graffitiactive1 == 1)
        {
            BGMInstance.setParameterByName("On or Off", 2.0f);
        }

        if(GameManager.Instance.graffitiactive1 == 2)
        {
            BGMInstance.setParameterByName("On or Off", 4.0f);
        }

        if (GameManager.Instance.graffitiactive1 == 3)
        {
            BGMInstance.setParameterByName("On or Off", 6.0f);
        }

        if (GameManager.Instance.graffitiactive1 == 4)
        {
            BGMInstance.setParameterByName("On or Off", 8.0f);
        }

        if(GameManager.Instance.graffitiactive1 == 5)
        {
            BGMInstance.setParameterByName("On or Off", 10.0f);
        }

        if (GameManager.Instance.graffitiactive1 == 6)
        {
            BGMInstance.setParameterByName("On or Off", 12.0f);
        }

        if (GameManager.Instance.graffitiactive1 == 7)
        {
            BGMInstance.setParameterByName("On or Off", 14.0f);
        }

        if (GameManager.Instance.graffitiactive1 == 8)
        {
            BGMInstance.setParameterByName("On or Off", 15.0f);
        }


        if(GameManager.Instance.clear == 1)
        {
            BGMInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}

