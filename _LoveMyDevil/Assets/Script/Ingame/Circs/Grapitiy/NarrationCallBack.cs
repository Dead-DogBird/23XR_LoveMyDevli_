using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationCallBack : MonoBehaviour
{
    [SerializeField] private int ID;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<GrapitiyLine>().GetNarration += GetNarration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetNarration()
    {
        DialogManagerv.Instance.SetID(ID);
    }
}
