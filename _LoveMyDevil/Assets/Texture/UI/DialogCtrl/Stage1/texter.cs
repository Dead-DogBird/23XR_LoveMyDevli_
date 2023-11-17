using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Security.Cryptography;

public class texter : MonoBehaviour
{
    
    public GameObject textinput2; 

    int checker; 

    // 이미지들
    public Image lucypanel;
    public Image normalpanel;

    bool adclickedcheck = false; 
    bool adclicked = false;
    bool coloredtime = false;

    bool noncolored = true;
    bool onceplayco = false; 
    bool iscolored;


    [Header("Dialogs")]
    public string[] textstring;

    public string[] textstring2;

    public string[] textstring3; 

    public TextMeshProUGUI textObj;

    private void Update()
    {
        checker = TypingManager.instance.inputcount;


        if (checker == 2)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        }

        if (checker == 6)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 227);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        }

        if (checker == 7)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        }

        if (checker == 8)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 227);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);

        }

        if (adclickedcheck == false)
        {
            if (adclicked == false)
            {
                if (checker >= 9)
                {
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                    {
                        adclicked = true;
                        textObj.text = "(?)";
                    }
                    if (checker == 10)
                    {
                        textObj.text = "(대화 말구 a랑 d를 눌러보자구;;)";
                    }
                }
            }
        }

        if (adclicked == true)
        {
            StartCoroutine(secondText());
        }

        if (checker == 12)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        }

        if (checker == 14)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 227);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        }

        if (checker == 15)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        }

        if (checker == 18)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            coloredtime = true;
        }

        if (noncolored == true)
        {
            if (coloredtime == true)
            {
                if (GameManager.Instance.graffitiactive1 == 1)
                {
                    textObj.text = "(?)";
                    StartCoroutine(thirdText());
                }
            }
        }

        if (checker == 20)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        }

        if (checker == 21)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 227);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        }

        if (checker == 23)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        }

        if (checker == 24)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 227);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        }

        if(checker == 27)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        }

    }


    private void Start()
    {
        
        
        StartCoroutine(StartText());
        //TypingManager.instance.Typing(lucystring, textObj);
    }



    IEnumerator StartText()
    {
        
        yield return new WaitForSeconds(2);
        if (TypingManager.instance.inputcount == 0)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 227);
            textObj.rectTransform.anchoredPosition = new Vector2(0, 227);
            TypingManager.instance.Typing(textstring, textObj);
        }
    }

    IEnumerator secondText()
    {
        adclickedcheck = true;
        adclicked = false;
        textObj.text = "";
        textObj.rectTransform.anchoredPosition = new Vector2(0, 700);
        lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);

        if(TypingManager.instance.inputcount == 9)
        {
            Debug.Log("여기도 받아옴");            
            TypingManager.instance.Typing(textstring2, textObj);            

        }


        yield return new WaitForSeconds(2);
        if (TypingManager.instance.inputcount == 10 )
        {  
            TypingManager.instance.Typing(textstring2, textObj);          
            Debug.Log("받아옴");                 
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 227);
            textObj.rectTransform.anchoredPosition = new Vector2(0, 227);          
        }
    }

    IEnumerator thirdText()
    {
        noncolored = false; 
        coloredtime = false;
        textObj.text = "";
        
        yield return new WaitForSeconds(2);
        TypingManager.instance.Typing(textstring3, textObj);
        lucypanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        textObj.rectTransform.anchoredPosition = new Vector2(0, 227);
    }
}
