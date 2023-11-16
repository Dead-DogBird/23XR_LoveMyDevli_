using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GrapitiyPoint : MonoBehaviour
{
    private GrapitiyLine _grapitiyLine;

    private SpriteRenderer _spriteRenderer;

    private SpriteMask _spriteMask;

    [SerializeField] private Material _addMaterial;
    [SerializeField] private Color activeRenderColor = new(255/255f,1/255f,168/255f,1);
    private GameObject activeRenderObj;

    private Tween _tween;

    // Start is called before the first frame update
    void Start()
    {
        _grapitiyLine = transform.GetComponentInParent<GrapitiyLine>();
        _grapitiyLine.SetPoint(this);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteMask = GetComponent<SpriteMask>();
        _spriteRenderer.enabled = false;
        _spriteMask.sprite = _spriteRenderer.sprite;
        activeRenderObj = new GameObject("activeRenderObj");
        activeRenderObj.transform.parent = transform;
        activeRenderObj.transform.localScale = Vector3.one;
        activeRenderObj.transform.localPosition = new Vector3(0, 0, transform.localPosition.y-1);
        activeRenderObj.AddComponent<SpriteRenderer>();
        activeRenderObj.GetComponent<SpriteRenderer>().sprite = _spriteRenderer.sprite;
        activeRenderObj.GetComponent<SpriteRenderer>().material = _addMaterial;
        activeRenderObj.GetComponent<SpriteRenderer>().sortingLayerID = _spriteRenderer.sortingLayerID;
        activeRenderObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
        activeRenderObj.GetComponent<SpriteRenderer>().color = activeRenderColor;
        activeRenderObj.SetActive(false);
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MouseCollider"))
        {
            DoActive();
        }
    }

    void DoActive()
    {
        _grapitiyLine.GetPoint();
        _spriteRenderer.enabled = true;
        _spriteRenderer.color = new Color(1, 1, 1, 0);
        _spriteRenderer.DOColor(Color.white,3f);
        activeRenderObj.SetActive(true);
        activeRenderObj.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 3f);
    }
}
