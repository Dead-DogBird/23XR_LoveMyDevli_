using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingToMiddle : MonoBehaviour
{


    private void Start()
    {
        StartCoroutine(LoadScene());
    }


    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);
        SceneLoadCtrl.LoadScene("MiddletestScene");
    }



}
