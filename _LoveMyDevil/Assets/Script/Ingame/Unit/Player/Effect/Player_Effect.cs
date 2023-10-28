using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Effect : MonoBehaviour
{

    [SerializeField] private GameObject DashEffect;
    [SerializeField] private GameObject RunEffect;
    [SerializeField] private GameObject LandEffect;
    [SerializeField] private GameObject JumpEffect;
    [SerializeField] private GameObject DoubleJumpEffect;

    public enum Effects
    {
        Dash,
        Run,
        Land,
        Jump,
        DoubleJump
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getEffect(Effects _effects)
    {
        switch (_effects)
        {
            case Effects.Dash:
                Instantiate(DashEffect, transform.position, Quaternion.identity);
                break;
            case Effects.Run:
                Instantiate(RunEffect, transform.position, Quaternion.identity);
                break;
            case Effects.Land:
                Instantiate(LandEffect, transform.position-new Vector3(0,0.5f), Quaternion.identity);
                break;
            case Effects.Jump:
                Instantiate(JumpEffect, transform.position-new Vector3(0,0.5f), Quaternion.identity);
                break;
            case Effects.DoubleJump:
                Instantiate(DoubleJumpEffect, transform.position-new Vector3(0,0.5f), Quaternion.identity);
                break;
        }
        
        
    }
}
