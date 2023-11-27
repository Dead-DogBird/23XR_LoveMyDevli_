using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Security.Cryptography;

public class texter3 : MonoSingleton<texter3>
{
    public bool playercantmove = true;


    public bool maidcol = false;


    bool maidtextstart = false;

    int checker;

    public Image lucypanel;
    public Image normalpanel;

    public TextMeshProUGUI normalname;



    [Header("Dialogs")]
    public string[] textstring;
    public string[] textstring2;
    public TextMeshProUGUI textObj;

    // Update is called once per frame
    void Update()
    {
        checker = TypingManager.instance.inputcount;

        if (checker == 3)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
            textObj.fontSize = 50;
        }

        if (checker == 7)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            textObj.rectTransform.anchoredPosition = new Vector2(0, 700);
            textObj.fontSize = 35;
        }

        if (checker == 8)
        {
            textObj.rectTransform.anchoredPosition = new Vector2(0, 227);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        }

        if (checker == 12)
        {
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        }

        if (checker == 13)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        }

        if(checker == 14)
        {
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 227);
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        }

        if(checker == 17)
        {
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        }

        if (checker == 18) 
        {
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        }

        if (maidcol == true)
        {
            if (maidtextstart == false)
            {
                maidtext();
            }


        }

        if(checker == 24)
        {
            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            textObj.rectTransform.anchoredPosition = new Vector2(0, 700);
            playercantmove = false;
            PlayerMove.freeze = false;
            PlayerMove.speed = 5.3f;
        }

    }


    private void Start()
    {


        StartCoroutine(StartText());
        //TypingManager.instance.Typing(lucystring, textObj);
    }

    void maidtext()
    {
        maidtextstart = true;
        StartCoroutine(maidscript());

    }

    IEnumerator maidscript()
    {
        PlayerMove.freeze = true;
        PlayerMove.speed = 0;

        yield return new WaitForSeconds(0.5f);

        if (TypingManager.instance.inputcount == 18)
        {

            normalpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
            normalname.text = "∏ﬁ¿ÃµÂ";
            textObj.rectTransform.anchoredPosition = new Vector2(0, 227);
            TypingManager.instance.Typing(textstring2, textObj);
        }
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
}
