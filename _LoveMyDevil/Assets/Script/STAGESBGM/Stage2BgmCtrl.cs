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
        

       
        BGMInstance.setParameterByName("On or Off", GameManager.Instance.graffitiactive1);
        


    }
}
