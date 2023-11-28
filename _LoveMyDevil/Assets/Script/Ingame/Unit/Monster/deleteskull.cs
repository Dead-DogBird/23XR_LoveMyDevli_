using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteskull : MonoBehaviour
{

    public GameObject skull; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("3")) 
        {
            Destroy(skull);
        }
    }
}
