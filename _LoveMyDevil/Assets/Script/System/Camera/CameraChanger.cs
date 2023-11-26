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
        GameManager.Instance.OnMakeOverGraffiti += (o, args) => GetMakeOverGraffiti();
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

            switch (activeCameraID)
            {
                case 0:
                    Cameras[0].SetActive(true);
                    Cameras[1].SetActive(false);
                    break;
                case 1:
                    Cameras[1].SetActive(true);
                    Cameras[0].SetActive(false);
                    break;
            }
            
        }
    }

    void GetMakeOverGraffiti()
    {
        Cameras[0].SetActive(false);
        Cameras[1].SetActive(false);
        Cameras[2].SetActive(true);
    }
}
