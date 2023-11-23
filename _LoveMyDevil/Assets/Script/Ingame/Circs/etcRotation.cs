using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class etcRotation : MonoBehaviour
{
    [SerializeField] private GameObject RotationObj;

    private ParticleSystem _particle;

    private ParticleSystem.MainModule _mainModule;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        _particle = GetComponent<ParticleSystem>();
        _mainModule = _particle.main;
        
        _mainModule.startRotation =  WrapAngle(RotationObj.transform.eulerAngles.z) + 90;
         Debug.Log(_mainModule.startRotation.constant);
        _particle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        _mainModule.startRotationZ = 90;
    }

    float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;
        return angle;
    }
}
