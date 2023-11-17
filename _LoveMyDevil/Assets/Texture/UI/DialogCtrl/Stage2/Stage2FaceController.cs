using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Legacy;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class Stage2FaceController : MonoBehaviour
{

    int checker;



    [Header("LucyFaces")]
    public Image[] LucyFaces;


    [Header("Monster'sFaces")]
    public Image[] MonstersFaces;






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checker = Stage2Typing.instance.inputcount;

        if(checker == 2)
        {
            LucyFaces[0].color = new Color(1, 1, 1, 0);
            LucyFaces[1].color = new Color(1, 1, 1, 1);
        }
        if(checker == 3)
        {
            LucyFaces[1].color = new Color(1, 1, 1, 0);
            LucyFaces[2].color = new Color(1, 1, 1, 1);
        }
        if(checker == 4)
        {
            LucyFaces[2].color = new Color(1, 1, 1, 0);
            LucyFaces[3].color = new Color(1, 1, 1, 1);
        }

    }
}
