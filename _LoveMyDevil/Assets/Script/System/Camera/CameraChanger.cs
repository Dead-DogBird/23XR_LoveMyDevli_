using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public GameObject[] Cameras;

    public int activeCameraID;
    // Start is called before the first frame update
    void Start()
    {
        Cameras[activeCameraID].SetActive(true);
        Cameras[(Cameras.Length-1)-activeCameraID].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCamera(int CameraID)
    {
        if (activeCameraID != CameraID)
        {
            activeCameraID = CameraID;
            Cameras[CameraID].SetActive(true);
            Cameras[(Cameras.Length-1)-CameraID].SetActive(false);
        }
    }
}
