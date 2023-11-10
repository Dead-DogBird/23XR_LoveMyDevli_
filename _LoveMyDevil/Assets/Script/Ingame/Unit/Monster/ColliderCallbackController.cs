using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderCallbackController : MonoBehaviour
{
    public event Action<Collider2D> onColiderEnter;
    public event Action<Collider2D> onColiderExit;

    public event Action<Collision2D> onCollisionEnter;
    public event Action<Collision2D> onCollisionExit;
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
        Debug.Log("OnTriggerEnter2D");
        Debug.Log(other.tag);
        onColiderEnter?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
        Debug.Log(other.tag);
        onColiderExit?.Invoke(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D");
        onCollisionEnter?.Invoke(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D");
        onCollisionExit?.Invoke(other);
    }
}
