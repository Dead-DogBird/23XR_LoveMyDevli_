using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 



public class Stage1Facectrl : MonoBehaviour
{
    


    int checker; 

    public Image lucy_fristimage;
    public Image lucy_secondimage;
    public Image lucy_thirdimage;
    public Image lucy_funnyface; 

    public Image keeper_firstimage;
    public Image keeper_secondimage;
    public Image keeper_thirdimage;
    public Image keeper_lastimage; 







    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checker = TypingManager.instance.inputcount;

        if (checker == 2)
        {
            lucy_fristimage.color = new Color(1, 1, 1, 0);
            lucy_secondimage.color = new Color(1, 1, 1, 1);
        }

        if (checker == 3)
        {
            keeper_firstimage.color = new Color(1, 1, 1, 0);
            keeper_secondimage.color = new Color(1, 1, 1, 1);
        }

        if (checker == 4)
        {
            keeper_secondimage.color = new Color(1, 1, 1, 0);
            keeper_thirdimage.color = new Color(1, 1, 1, 1);
        }

        if (checker == 5)
        {
            keeper_thirdimage.color = new Color(1, 1, 1, 0);
            keeper_lastimage.color = new Color(1, 1, 1, 1);
        }

        if (checker == 7)
        {
            keeper_lastimage.color = new Color(1, 1, 1, 0);
            keeper_secondimage.color = new Color(1, 1, 1, 1);
        }

        if (checker == 8)
        {
            lucy_secondimage.color = new Color(1, 1, 1, 0);
            lucy_thirdimage.color = new Color(1, 1, 1, 1);
        }

        if (checker == 12)
        {
            keeper_secondimage.color = new Color(1, 1, 1, 0);
            keeper_firstimage.color = new Color(1, 1, 1, 1);
        }

        if (checker == 14)
        {
            lucy_thirdimage.color = new Color(1, 1, 1, 0);
            lucy_funnyface.color = new Color(1, 1, 1, 1);
        }

        if (checker == 19)
        {
            lucy_funnyface.color = new Color(1, 1, 1, 0);
            lucy_secondimage.color = new Color(1, 1, 1, 1);
        }

        if (checker == 21)
        {
            lucy_secondimage.color = new Color(1, 1, 1, 0);
            lucy_funnyface.color = new Color(1, 1, 1, 1);
        }

        if (checker == 22)
        {
            lucy_funnyface.color = new Color(1, 1, 1, 0);
            lucy_thirdimage.color = new Color(1, 1, 1, 1);
        }

        if(checker == 26)
        {
            lucy_thirdimage.color = new Color(1, 1, 1, 0);
            lucy_funnyface.color = new Color(1, 1, 1, 1);
        }
    }
}
