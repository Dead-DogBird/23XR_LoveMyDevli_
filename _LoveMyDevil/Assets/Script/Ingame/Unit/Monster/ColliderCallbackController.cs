using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderCallbackController : MonoBehaviour
{
    public event Action<Collider2D> onColliderEnter;
    public event Action<Collider2D> onColliderExit;
    
    public event Action<Collision2D> onCollisionEnter;

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
        onColliderEnter?.Invoke(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        onColliderExit?.Invoke(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        onCollisionEnter?.Invoke(other);
    }
}
