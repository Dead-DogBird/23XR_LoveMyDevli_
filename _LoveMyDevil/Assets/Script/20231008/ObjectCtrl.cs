using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectCtrl : MonoBehaviour
{
    private Transform object1;
    private Transform object2;

    public GameObject[] cuts;
    private void Start()
    {
        object1 = GameObject.Find("protocutwall1").transform;
        object2 = GameObject.Find("protocutwall2").transform;      
        StartCoroutine(MoveObjects());
        StartCoroutine(deletecuts());
    }

    private IEnumerator MoveObjects()
    {
        yield return new WaitForSeconds(1.0f);

        float startTime = Time.time;

        while (Time.time - startTime < 1.0f)
        {

            float t = (Time.time - startTime) / 1.0f;
            object1.position = new Vector3(object1.position.x, Mathf.Lerp(4.5f, 8.5f, t), object1.position.z);
            object2.position = new Vector3(object2.position.x, Mathf.Lerp(-4.5f, -8.5f, t), object2.position.z);
            yield return null;
        }


        object1.position = new Vector3(object1.position.x, 8.5f, object1.position.z);
        object2.position = new Vector3(object2.position.x, -8.5f, object2.position.z);


        yield return new WaitForSeconds(2.0f);


        StartCoroutine(ReturnToInitialPosition());
    }

    private IEnumerator ReturnToInitialPosition()
    {
        float startTime = Time.time;

        while (Time.time - startTime < 1.0f)
        {

            float t = (Time.time - startTime) / 1.0f;
            object1.position = new Vector3(object1.position.x, Mathf.Lerp(8.5f, 4.5f, t), object1.position.z);
            object2.position = new Vector3(object2.position.x, Mathf.Lerp(-8.5f, -4.5f, t), object2.position.z);
            yield return null;
        }


        object1.position = new Vector3(object1.position.x, 4.5f, object1.position.z);
        object2.position = new Vector3(object2.position.x, -4.5f, object2.position.z);

        StartCoroutine(MoveObjects());
    }

    IEnumerator deletecuts()
    {
        yield return new WaitForSeconds(5f);
        Destroy(cuts[0]);
        yield return new WaitForSeconds(5f);
        Destroy(cuts[1]);
        yield return new WaitForSeconds(5f);
        Destroy(cuts[2]);
        yield return new WaitForSeconds(5f);
        Destroy(cuts[3]);
        yield return new WaitForSeconds(5f);
        Destroy(cuts[4]);
        yield return new WaitForSeconds(5f);
        Destroy(cuts[5]);
        yield return new WaitForSeconds(5.5f);
        Destroy(cuts[6]);

        Debug.Log(Time.fixedTime);
        StopCoroutine(deletecuts());
        StopAllCoroutines();
    }

}
