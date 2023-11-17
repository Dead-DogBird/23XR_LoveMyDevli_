using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class Stage4FaceController : MonoBehaviour
{

    int checker;



    [Header("LucyFaces")]
    public Image[] LucyFaces;


    [Header("SatanFaces")]
    public Image[] SatanFaces;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checker = TypingManager.instance.inputcount;

        if (checker == 3)
        {
            SatanFaces[0].color = new Color(1, 1, 1, 0);
            SatanFaces[1].color = new Color(1, 1, 1, 1);
        }

        if (checker == 5)
        {
            LucyFaces[0].color = new Color(1, 1, 1, 0);
            LucyFaces[1].color = new Color(1, 1, 1, 1);
        }

        if (checker == 6)
        {
            LucyFaces[1].color = new Color(1, 1, 1, 0);
            LucyFaces[0].color = new Color(1, 1, 1, 1);
        }

        if (checker == 7)
        {
            LucyFaces[0].color = new Color(1, 1, 1, 0);
            LucyFaces[2].color = new Color(1, 1, 1, 1);
        }

        if (checker == 9)
        {
            SatanFaces[1].color = new Color(1, 1, 1, 0);
            SatanFaces[0].color = new Color(1, 1, 1, 1);
        }

        if (checker == 14)
        {
            SatanFaces[0].color = new Color(1, 1, 1, 0);
            SatanFaces[1].color = new Color(1, 1, 1, 1);
        }

        if(checker == 15)
        {
            LucyFaces[2].color = new Color(1, 1, 1, 0);
            LucyFaces[3].color = new Color(1, 1, 1, 1);
        }
    }
}
