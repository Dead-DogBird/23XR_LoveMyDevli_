using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class Stage3FaceControl : MonoBehaviour
{
    int checker;

    

    [Header("LucyFaces")]
    public Image[] LucyFaces;


    [Header("KeeperFaces")]
    public Image[] KeeperFaces;







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
            LucyFaces[0].color = new Color(1, 1, 1, 0);
            LucyFaces[3].color = new Color(1, 1, 1, 1);
        }

        if (checker == 11)
        {
            KeeperFaces[1].color = new Color(1, 1, 1, 0);
            KeeperFaces[0].color = new Color(1, 1, 1, 1);
        }

        if (checker == 12)
        {
            LucyFaces[3].color = new Color(1, 1, 1, 0);
            LucyFaces[1].color = new Color(1, 1, 1, 1);
        }

        if (checker == 13)
        {
            KeeperFaces[0].color = new Color(1, 1, 1, 0);
            KeeperFaces[1].color = new Color(1, 1, 1, 1);
        }



        if (checker == 15)
        {
            LucyFaces[1].color = new Color(1, 1, 1, 0);
            LucyFaces[2].color = new Color(1, 1, 1, 1);
        }

        if (checker == 16)
        {
            LucyFaces[2].color = new Color(1, 1, 1, 0);
            LucyFaces[1].color = new Color(1, 1, 1, 1);
        }

        if (checker == 17)
        {
            KeeperFaces[1].color = new Color(1, 1, 1, 0);
            KeeperFaces[0].color = new Color(1, 1, 1, 1);
        }

        if(checker == 19)
        {
            KeeperFaces[0].color = new Color(1, 1, 1, 0);
            KeeperFaces[3].color = new Color(1, 1, 1, 1);
        }

        if(checker == 23)
        {
            KeeperFaces[3].color = new Color(1, 1, 1, 0);
            KeeperFaces[4].color = new Color(1, 1, 1, 1);
        }
    }
}
