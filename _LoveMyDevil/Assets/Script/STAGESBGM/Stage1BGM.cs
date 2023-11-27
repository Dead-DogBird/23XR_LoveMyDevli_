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

     int valuetask; 


    private FMOD.Studio.EventInstance BGMInstance;


    // Start is called before the first frame update
    void Start()
    {      
        BGMInstance = FMODUnity.RuntimeManager.CreateInstance(BgmCtrl);
        BGMInstance.start();
    }

    // Update is called once per frame
  public  void Update()
    {

        valuetask = NextDoor.Instance.value;

        checkGraffiti = GameManager.Instance.graffitiactive1; 
       
        if(checkGraffiti == 1)
        {
            BGMInstance.setParameterByName("On or Off", 3.0f);
        }

        if (valuetask == 1)
        {
            Debug.Log("d");
            BGMInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
