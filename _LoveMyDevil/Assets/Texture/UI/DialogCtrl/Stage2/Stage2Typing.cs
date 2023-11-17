using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stage2Typing : MonoBehaviour
{

    //5  1초 



    private bool freezetime = true; 

    private int SpaceClicked = 1;

    public int inputcount = 0;



    public static Stage2Typing instance;

    [Header("Times for each character")]
    public float timeForCharacter;

    [Header("Times for each character when speed up")]
    public float timeForCharacter_Fast;

    float characterTime;

    string[] dialogsSave;
    TextMeshProUGUI tmpSave;

    public static bool isDialogEnd;

    bool isTypingEnd = false;
    int dialogNumber = 0;

    float timer;


    private void Update()
    {
        
        if (freezetime == true)
        {
            if (SpaceClicked == 1)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GetInputDown();
                    SpaceClicked = -1;
                }
            }

            if (SpaceClicked == -1)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    GetInputUp();
                    SpaceClicked = 1;
                }
            }
        }

        if (inputcount == 5)
        {
            freezetime = false;
            
        }

        

    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        timer = timeForCharacter;
        characterTime = timeForCharacter;
    }

    public void Typing(string[] dialogs, TextMeshProUGUI textObj)
    {

        inputcount += 1;
        Debug.Log(inputcount);
        isDialogEnd = false;
        dialogsSave = dialogs;
        tmpSave = textObj;
        if (dialogNumber < dialogs.Length)
        {
            char[] chars = dialogs[dialogNumber].ToCharArray();
           
               StartCoroutine(Typer(chars, textObj));

        }
        else
        {


            tmpSave.text = "";
            isDialogEnd = true;
            dialogsSave = null;
            tmpSave = null;
            dialogNumber = 0;
        }

    }

    public void GetInputDown()
    {

        if (dialogsSave != null)
        {
            if (isTypingEnd)
            {
                tmpSave.text = "";
                Typing(dialogsSave, tmpSave);
            }
            else
            {
                characterTime = timeForCharacter_Fast;
            }
        }
    }

    public void GetInputUp()
    {
        //인풋이 끝났을때.
        if (dialogsSave != null)
        {
            characterTime = timeForCharacter;
        }
    }

    IEnumerator Typer(char[] chars, TextMeshProUGUI textObj)
    {
        int currentChar = 0;
        int charLength = chars.Length;
        isTypingEnd = false;
      
        while (currentChar < charLength)
        {
            if (timer >= 0)
            {
                yield return null;
                timer -= Time.deltaTime;
            }
            else
            {
                textObj.text += chars[currentChar].ToString();
                currentChar++;
                timer = characterTime; //타이머 초기화
            }
        }

        if(freezetime == false)
        {
            
            yield return new WaitForSeconds(1f);
            // 1초 대기 후에 다음 대화로 넘어가기
            tmpSave.text = "";
            dialogNumber++;
            Typing(dialogsSave, tmpSave);
            
        }

        if (freezetime == true)
        {
            if (currentChar >= charLength)
            {
                isTypingEnd = true;

                dialogNumber++;
                yield break;
            }
        }
        Debug.Log(freezetime);

       

    }



}
