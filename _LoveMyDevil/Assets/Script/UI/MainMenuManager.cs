using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;


public class MainMenuManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string buttonClickEvent;

    [FMODUnity.EventRef]
    public string TitleBgmCtrl; 

    private FMOD.Studio.EventInstance eventInstance;

    private FMOD.Studio.EventInstance BgmInstance; 

    [SerializeField] private Image SettingCanvasBG;
    [SerializeField] private Canvas SettingCanvas;
    [SerializeField] private Image Keyboards;
    [SerializeField] private Sprite[] KeyBoardSet;
    private bool isSetting = false;
    private bool isEnterGame = false;

    private int KeybordSetting;
    private Vector3 Onpos = new Vector2(201,-260);
   public void Start()
    {
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(buttonClickEvent);
        BgmInstance = FMODUnity.RuntimeManager.CreateInstance(TitleBgmCtrl);

        BgmInstance.start();
        Keyboards.sprite = KeyBoardSet[PlayerPrefs.GetInt("KeyboardSetting")];
        
    }

    void Update()
    {
        if (!isEnterGame)
        {
            if (isSetting)
            {
                SettingCanvas.transform.localPosition += new Vector3(0,
                    (0.5f - SettingCanvas.transform.localPosition.y) *
                    (Time.unscaledDeltaTime * 7));
                SettingCanvasBG.color += (new Color(0, 0, 0, 0.8f) - SettingCanvasBG.color) *
                                         (Time.unscaledDeltaTime * 5);
                BgmInstance.setParameterByName("TitleWaveChange", 0.5f);
            }
            else
            {
                SettingCanvas.transform.localPosition += (new Vector3(0, -1800)) * (Time.unscaledDeltaTime * 7);
                SettingCanvasBG.color += (Color.clear - SettingCanvasBG.color) *
                                         (Time.unscaledDeltaTime * 5);
                BgmInstance.setParameterByName("TitleWaveChange", 0f);
            }
            
        }
        else
        {
            SettingCanvasBG.color += (Color.black - SettingCanvasBG.color) *
                                     (Time.unscaledDeltaTime * 5);
        }

       

    }

    public void SettingButton(bool isOn)
    {       
        eventInstance.setParameterByName("Buttons", 1.0f);
        eventInstance.start();
        isSetting = isOn;      
    }

    public void GameStart()
    {
        BgmInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        eventInstance.setParameterByName("Buttons", 2.0f);
        eventInstance.start();
        isEnterGame = true;
        EnterGameTask().Forget();
    }

    private bool isBlinkMode;
    public void KeyboardSet(bool isUp)
    {
        eventInstance.setParameterByName("Buttons", 3.0f);
        eventInstance.start();
        if (isUp) KeybordSetting = 1;
        else KeybordSetting = 0;
        Keyboards.sprite = KeyBoardSet[KeybordSetting];
        PlayerPrefs.SetInt("KeyboardSetting",KeybordSetting);
    }
    async UniTaskVoid EnterGameTask()
    {
        await UniTask.WaitUntil(() =>Mathf.Abs(SettingCanvasBG.color.a-1)<0.004f);
        //TODO : 게임시작
        //로딩씬
        LoadingSceneManager.LoadScene("Stage1",0);
    }

    public void OnMouseEnter()
    {
        eventInstance.setParameterByName("Buttons", 0.0f);
        eventInstance.start();
    }



}