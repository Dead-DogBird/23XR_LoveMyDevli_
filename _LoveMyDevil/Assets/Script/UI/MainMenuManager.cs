using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image SettingCanvasBG;
    [SerializeField] private Canvas SettingCanvas;
    [SerializeField] private Image Keyboards;
    [SerializeField] private Sprite[] KeyBoardSet;
    private bool isSetting = false;
    private bool isEnterGame = false;

    private int KeybordSetting;
    private Vector3 Onpos = new Vector2(201,-260);
    void Start()
    {
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
            }
            else
            {
                SettingCanvas.transform.localPosition += (new Vector3(0, -1800)) * (Time.unscaledDeltaTime * 7);
                SettingCanvasBG.color += (Color.clear - SettingCanvasBG.color) *
                                         (Time.unscaledDeltaTime * 5);
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
        isSetting = isOn;
    }

    public void GameStart()
    {
        isEnterGame = true;
        EnterGameTask().Forget();
    }

    private bool isBlinkMode;
    public void KeyboardSet(bool isUp)
    {
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
}