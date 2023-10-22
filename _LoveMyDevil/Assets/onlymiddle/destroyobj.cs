using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyobj : MonoBehaviour
{
   

    public Transform keeppos;

    private float yoffest = -3.0f;
   
    private void Update()
    {
     if(keeppos != null)
     {
            Vector3 newPosition = new Vector3(transform.position.x, keeppos.position.y + yoffest, transform.position.z);
            transform.position = newPosition;
     }      

    }

  


   
}
