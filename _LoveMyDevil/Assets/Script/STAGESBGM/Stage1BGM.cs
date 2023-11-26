using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity; 



public class Stage1BGM : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        checkGraffiti = GameManager.Instance.graffitiactive1; 

        if(checkGraffiti == 1)
        {
            BGMInstance.setParameterByName("On or Off", 1.0f);
        }


       
        if(checkGraffiti == 2)
        {
            BGMInstance.setParameterByName("On or Off", 2.0f);
        }

        if(checkGraffiti == 3)
        {
            BGMInstance.setParameterByName("On or Off", 3.0f);
        }
      
    }
}
