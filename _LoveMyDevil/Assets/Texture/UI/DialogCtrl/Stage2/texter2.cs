using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Security.Cryptography;
using AmplifyShaderEditor;
using TMPro;

public class texter2 : MonoSingleton<texter2>
{
    public bool playercantmove = true; 


    public bool maidcol = false;

    int checker;
    int checker2stage;

    int stage2dialtriger = 0;

    int trigerdeny = 0; 


    // 이미지 
    public Image lucypanel;
    public Image normalpanel;

    //네임택
    public TextMeshProUGUI normalname;

    bool maidtextstart = false;


    // update callback
    bool check1 = false;
    bool check2 = false;
    bool check3 = false; 

    [Header("Dialogs")]
    public string[] textstring;
    public string[] textstring2;
    


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

        

        if(checker == 5)
        {
            noonetalk();           
            trigerdeny = 1;
            textObj.text = "";
        }

        if(maidcol == true)
        {
            if(maidtextstart == false)
            {
                maidtext(); 
            }

           
        }

        if(checker == 11)
        {
            noonetalk();
            playercantmove = false;
            PlayerMove.freeze = false;
            PlayerMove.speed = 5.3f;
        }
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

        if(TypingManager.instance.inputcount == 5)
        {
            normaltalk();
            textObj.rectTransform.anchoredPosition = new Vector2(0, 227);
            TypingManager.instance.Typing(textstring2, textObj);
        }
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
