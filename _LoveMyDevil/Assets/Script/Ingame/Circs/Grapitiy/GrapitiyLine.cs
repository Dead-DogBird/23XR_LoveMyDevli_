using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapitiyLine : MonoBehaviour
{
    //�������� 2 
    string nowname;
    
    public static int images = 0; 

    private List<GrapitiyPoint> _grapitiyPoints = new List<GrapitiyPoint>();

    private int _acitvePointsCount = 0;
    private int _PointCount = 0;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        GameManager.Instance.SetPoint();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;

        nowname = gameObject.name;

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


            // 2�������� ���̾�α� 
            // ���ӹ�� �˻絹���� �߻����̾�α� ���� 
            if(nowname == "1_line_graffiti")
            {
                images = 1; 
            }

            if(nowname == "4_line_graffiti")
            {
                images = 2; 
            }

            if(nowname == "6_line_graffiti")
            {
                images = 3; 
            }

            
           


            foreach (var var_ in _grapitiyPoints)
            {
                var_.gameObject.SetActive(false);
                Debug.Log(images);
            }
        }
    }
}
