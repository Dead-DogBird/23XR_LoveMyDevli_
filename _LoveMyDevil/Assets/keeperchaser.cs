using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keeperchaser : MonoBehaviour
{
    public Transform targetObject; 

    private float yOffset = -3.0f; 
    private void Update()
    {
        if (targetObject != null)
        {
            
            Vector3 newPosition = new Vector3(transform.position.x, targetObject.position.y + yOffset, transform.position.z);
            transform.position = newPosition;
        }
    }
}
