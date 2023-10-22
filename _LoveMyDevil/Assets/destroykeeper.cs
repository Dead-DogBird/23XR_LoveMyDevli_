using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroykeeper : MonoBehaviour
{

    public GameObject keeper;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(keeper);
        }
    }



}
