using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titlework : MonoBehaviour
{
    private bool movestop = false;
    public Button button;

    public GameObject Titlelogo; 


    public float cameraworkspeed = 30;
    public float tartgetY = 41.4f;

    private bool isMoving = false;

    private void Start()
    {
        StartCoroutine(camerawork());
        StartCoroutine(CountDown());
        StartCoroutine(MoveTitleLogo());
        button.transform.localPosition = new Vector3(0, -620, 0);
        Invoke("StartMovingButton", 2.0f);
    }

    private void StartMovingButton()
    {
        movestop = true;
        StartCoroutine(MoveButtonCoroutine());

    }

    IEnumerator camerawork() 
    {
        isMoving = true;

        while(transform.position.y < tartgetY)
        {
            Vector3 newPosition = transform.position + Vector3.up * cameraworkspeed * Time.deltaTime;
            transform.position = newPosition;

            yield return null; 
        }

        Debug.Log("마무리");
        isMoving = false;    
    }

    private System.Collections.IEnumerator CountDown()
    {

        int targetValue = 2;

        float duration = 2.25f;

        float timer = 0.0f;


        while (cameraworkspeed > targetValue)
        {
            timer += Time.deltaTime;

           
            cameraworkspeed = Mathf.RoundToInt(Mathf.Lerp(30, 3, timer / duration));

            
            Debug.Log("현재 값: " + cameraworkspeed);

            yield return null;
        }

        cameraworkspeed = targetValue; 
        Debug.Log("최종 값: " + cameraworkspeed);
    }

    private IEnumerator MoveButtonCoroutine()
    {
        float duration = 3.0f; 
        float elapsedTime = 0.0f;
        Vector3 startPosition = button.transform.localPosition;
        Vector3 targetPosition = new Vector3(0, -455, 0); 

        while (elapsedTime < duration)
        {
            
            elapsedTime += Time.deltaTime;

           
            float t = Mathf.Clamp01(elapsedTime / duration); 
            button.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        
        button.transform.localPosition = targetPosition;
    }

    private IEnumerator MoveTitleLogo()
    {

        float duration = 5.0f;
        float elapsedTime = 0.0f;
        Vector3 startPosition = Titlelogo.transform.position;
        Vector3 targetPosition = new Vector3(0, 42.5f, -4.6f);

        while (elapsedTime < duration)
        {

            elapsedTime += Time.deltaTime;


            float t = Mathf.Clamp01(elapsedTime / duration);
            Titlelogo.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }
        Titlelogo.transform.position = targetPosition;
    }
        
}

