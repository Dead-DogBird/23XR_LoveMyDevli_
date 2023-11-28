using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Security.Cryptography;
using AmplifyShaderEditor;

public class texter4 : MonoBehaviour
{
    int checker;

    public Image lucypanel;
    public Image satanmpanel;

    [Header("Dialogs")]
    public string[] textstring;

    public TextMeshProUGUI textObj;




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartText());
    }

    // Update is called once per frame
    void Update()
    {
        checker = TypingManager.instance.inputcount;

        if (checker == 2)
        {
            lucytalk();
        }
        if(checker == 3)
        {
            satantalk();
        }
        if(checker == 5)
        {
            lucytalk();
        }
        if(checker == 8)
        {
            satantalk();
        }
        if(checker == 14)
        {
            textObj.fontSize = 50;
            textObj.color = Color.red;
        }
        if(checker == 15)
        {
            textObj.fontSize = 50;
            textObj.color = Color.white; 
            lucytalk();
        }
        if(checker == 16)
        {
            textObj.color = Color.white;
            textObj.fontSize = 35;
            satanmpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
            lucypanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        }


    }

    IEnumerator StartText()
    {

        yield return new WaitForSeconds(2);



        if (TypingManager.instance.inputcount == 0)
        {
            satantalk();
            textObj.rectTransform.anchoredPosition = new Vector2(0, 227);
            TypingManager.instance.Typing(textstring, textObj);
        }
    }








    void lucytalk()
    {
        satanmpanel.rectTransform.anchoredPosition = new Vector2(0, 700);
        lucypanel.rectTransform.anchoredPosition = new Vector2(0, 227);
    }

    void satantalk()
    {
        satanmpanel.rectTransform.anchoredPosition = new Vector2(0, 227);
        lucypanel.rectTransform.anchoredPosition = new Vector2(0,700);
    }
}
