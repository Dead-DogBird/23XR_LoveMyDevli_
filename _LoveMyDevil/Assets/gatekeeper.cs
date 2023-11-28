using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity; 


public class gatekeeper : MonoBehaviour
{
    private SkeletonAnimation skl; 



    // Start is called before the first frame update
    void Start()
    {
        skl = GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.graffitiactive1 == 1)
        {
            flip(true);
        }
    }

    void flip(bool flipX)
    {
        skl.Skeleton.FlipX = flipX; 
    }


}
