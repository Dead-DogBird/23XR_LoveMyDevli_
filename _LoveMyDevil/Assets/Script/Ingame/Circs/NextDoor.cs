using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NextDoor : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter Player");
        Debug.Log(GameManager.Instance.GetProgress);
        if (other.gameObject.CompareTag("Player") && GameManager.Instance.GetProgress)
        {
            LoadingSceneManager.LoadScene("Stage2",1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("enter Player");
        Debug.Log(GameManager.Instance.GetProgress);
        if (other.collider.CompareTag("Player") && GameManager.Instance.GetProgress)
        {
            LoadingSceneManager.LoadScene("Stage2",1);
        }
    }
}
