using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Security.Cryptography;
using AmplifyShaderEditor;
using TMPro;

public class texter2 : MonoBehaviour
{
    int checker;
    int checker2stage;

    int stage2dialtriger = 0;

    int trigerdeny = 0; 


    // 이미지 
    public Image lucypanel;
    public Image normalpanel;

    //네임택
    public TextMeshProUGUI normalname;




    // update callback
    bool check1 = false;
    bool check2 = false;
    bool check3 = false; 

    [Header("Dialogs")]
    public string[] textstring;
    public string[] textstring2;
    public string[] textstring3;
    public string[] textstring4;
    public string[] textstring5; 


    public TextMeshProUGUI textObj;


    // Start is called before the first frame update
    void Start()
    {
        noonetalk();
        
        StartCoroutine(StartText());
    }

    // Update is called once per frame
    void Update()
    {
        stage2dialtriger = GrapitiyLine.images; 

        checker = TypingManager.instance.inputcount;

        checker2stage = Stage2Typing.instance.inputcount;

        if(checker == 5)
        {
            noonetalk();           
            trigerdeny = 1;
            check1 = true;
            check2 = true;
            check3 = true; 

        }

        /*
            if (trigerdeny == 1)
            {
            if (check1 == true)
            {
                if (stage2dialtriger == 1)
                {
                    StartCoroutine(SkullText());
                }
            }
            if (check2 == true)
            {
                if (stage2dialtriger == 2)
                {
                    StartCoroutine(Maidtext());
                }
            }
            if (check3 == true)
            {
                if (stage2dialtriger == 3)
                {
                    StartCoroutine(ghosttext());
                }
            }
            }

        if (trigerdeny == 2)
        {
            if (check1 == true)
            {
                if (stage2dialtriger == 1)
                {
                    StartCoroutine(SkullText());
                }
            }
            if (check2 == true)
            {
                if (stage2dialtriger == 2)
                {
                    StartCoroutine(Maidtext());
                }
            }
            if (check3 == true)
            {
                if (stage2dialtriger == 3)
                {
                    StartCoroutine(ghosttext());
                }
            }
        }

        if (trigerdeny == 3)
        {
            if (check1 == true)
            {
                if (stage2dialtriger == 1)
                {
                    StartCoroutine(SkullText());
                }
            }
            if (check2 == true)
            {
                if (stage2dialtriger == 2)
                {
                    StartCoroutine(Maidtext());
                }
            }
            if (check3 == true)
            {
                if (stage2dialtriger == 3)
                {
                    StartCoroutine(ghosttext());
                }
            }
        }

        */
    }


    IEnumerator StartText()
    {
        yield return new WaitForSeconds(2);

        if(TypingManager.instance.inputcount == 0)
        {
            lucytalk();
            textObj.rectTransform.anchoredPosition = new Vector2(0, 227);
            TypingManager.instance.Typing(textstring, textObj);
        }
    }
    
    IEnumerator SkullText()
    {
        check1 = false; 
        trigerdeny  += 1; 
       
        textObj.text = "";
        noonetalk();

        yield return new WaitForSeconds(2);

        lucytalk();
        Stage2Typing.instance.Typing(textstring2, textObj);
        
    }
    
    IEnumerator Maidtext()
    {
        check2 = false; 
        trigerdeny += 1;
        
        textObj.text = "";
        noonetalk();

        yield return new WaitForSeconds(2);

        normaltalk();
        normalname.text = "메이드";
        Stage2Typing.instance.Typing(textstring3, textObj);
    }

    IEnumerator ghosttext() 
    {
        check3 = false; 
        trigerdeny += 1;
        
        textObj.text = "";
        noonetalk();

        yield return new WaitForSeconds(2);

        normaltalk();
        normalname.text = "유령";
        Stage2Typing.instance.Typing(textstring4, textObj);

    }

    IEnumerator endtext()
    {
        trigerdeny += 1;

        textObj.text = "";
        noonetalk();

        yield return new WaitForSeconds(2);

        lucytalk();
        Stage2Typing.instance.Typing(textstring5, textObj);
    }


    void lucytalk()
    {
        normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        lucypanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        textObj.rectTransform.anchoredPosition = new Vector2(0, 227);
    }

    void normaltalk()
    {
        normalpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        textObj.rectTransform.anchoredPosition = new Vector2(0, 227);
    }

    void noonetalk()
    {
        normalpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        textObj.rectTransform.anchoredPosition = new Vector2(0, 700);
    }

}
