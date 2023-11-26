using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISpray : MonoBehaviour
{
    [SerializeField] private GameObject Spray;
    private GameObject nowSpray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnSpray();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnDeSpray();
        }
        if (Input.GetMouseButton(0))
        {
            if(nowSpray)
                nowSpray.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0,0,40);
        }
        
    }

    void OnSpray()
    {
        nowSpray = Instantiate(Spray, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
    }

    void OnDeSpray()
    {
        nowSpray = null;
    }
}
