using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapitiyPoint : MonoBehaviour
{
    private GrapitiyLine _grapitiyLine;

    private SpriteRenderer _spriteRenderer;

    private SpriteMask _spriteMask;
    // Start is called before the first frame update
    void Start()
    {
        _grapitiyLine = transform.GetComponentInParent<GrapitiyLine>();
        _grapitiyLine.SetPoint(this);

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteMask = GetComponent<SpriteMask>();
        _spriteRenderer.enabled = false;
        _spriteMask.sprite = _spriteRenderer.sprite;
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MouseCollider"))
        {
            _grapitiyLine.GetPoint();
            _spriteRenderer.enabled = true;
        }
    }
}
