using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Scenectrl : MonoBehaviour
{ 
   

    public float SceneChangeTime = 35.5f;


    private void Start()
    {
        StartCoroutine(SceneCheckTime());
    }

    IEnumerator SceneCheckTime()
    {
        yield return new WaitForSeconds(SceneChangeTime);
        
        SceneLoadCtrl.LoadScene("MiddletestScene");

        
    }



}
