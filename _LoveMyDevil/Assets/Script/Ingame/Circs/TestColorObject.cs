using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

public class TestColorObject : MonoBehaviour
{
    [SerializeField] private GameObject colordPart;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
       
    
        GameManager.Instance.SetPoint();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("MouseCollider"))
       {
            colordPart.SetActive(true);
            isActive = true;
            GameManager.Instance.GetPoint();
            SetImageTask().Forget();
            Destroy(this);
       }
    }

    async UniTaskVoid SetImageTask()
    {
        var ColorPart = colordPart.GetComponent<SpriteRenderer>();
        colordPart.SetActive(true);
        ColorPart.color = Color.clear;
        for (int i = 0; i < 50; i++)
        {
            ColorPart.color += (Color.white - ColorPart.color) / 10;
            await UniTask.Yield(PlayerLoopTiming.LastFixedUpdate);
        }
    }

}
