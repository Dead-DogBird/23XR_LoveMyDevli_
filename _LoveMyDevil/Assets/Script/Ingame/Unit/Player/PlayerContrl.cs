using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 



public class PlayerContrl : MonoBehaviour
{
    private string Scenename; 


    public struct UserInput
    {
        float _nowAxisState;
        bool _nowSpace;
        bool _leftMouseButton;
        private Vector3 _mousePos;
        private bool _skillKey;
        public bool SpaceState => _nowSpace;
        public bool LeftMouseState => _leftMouseButton;
        public float AxisState => _nowAxisState;
        public bool SkillKey => _skillKey;
        public Vector3 MousePos => _mousePos;
        public void InputUpdate()
        {
            _nowAxisState = Input.GetAxisRaw("Horizontal");
            _nowSpace = Input.GetKeyDown(mode?KeyCode.W:KeyCode.Space);
            _leftMouseButton = Input.GetMouseButton(0);
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _skillKey = Input.GetKeyDown(KeyCode.LeftShift);
        }
    }
    public UserInput Userinput;
    static bool mode;
    void Start()
    {
        Scenename = SceneManager.GetActiveScene().name;
        mode = PlayerPrefs.GetInt("KeyboardSetting")==0;
    }
    void Update()
    {
        if(Scenename == "Stage1")
        {
            if(TypingManager.instance.inputcount >= 9)
            {
                Userinput.InputUpdate();
                PlayerMove.unlockfreeze();            
            }
            if(TypingManager.instance.inputcount >= 18)
            {
                PlayerAct.unlockspray();
            }           
        }

        if(Scenename == "Stage2")
        {
            if(TypingManager.instance.inputcount >= 5)
            {
                Userinput.InputUpdate();
                PlayerMove.unlockfreeze();
                PlayerAct.unlockspray();
            }
        }

        if (Scenename == "Stage3")
        {
            if (TypingManager.instance.inputcount >= 18)
            {
                Userinput.InputUpdate();
                PlayerMove.unlockfreeze();
                PlayerAct.unlockspray();
            }
        }

        if (Scenename == "stage4")
        {
            if (TypingManager.instance.inputcount >= 16)
            {
                Userinput.InputUpdate();
                PlayerMove.unlockfreeze();
                PlayerAct.unlockspray();
            }
        }

    }
}
