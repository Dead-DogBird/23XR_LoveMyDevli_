using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapitiyLine : MonoBehaviour
{
    private List<GrapitiyPoint> _grapitiyPoints = new List<GrapitiyPoint>();
    private int _acitvePointsCount = 0;
    private int _PointCount = 0;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        GameManager.Instance.SetPoint();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPoint(GrapitiyPoint _point)
    {
        _grapitiyPoints.Add(_point);
        _PointCount++;
    }
    public void GetPoint()
    {
        _acitvePointsCount++;
        if (_acitvePointsCount == _PointCount)
        {
            GameManager.Instance.GetPoint();
            _spriteRenderer.enabled = true;
        }
    }
}
