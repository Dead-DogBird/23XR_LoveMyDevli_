using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image SettingCanvasBG;
    [SerializeField] private Canvas SettingCanvas;

    private bool isSetting = false;
    private bool isEnterGame = false;

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
                SettingCanvas.transform.localPosition += (new Vector3(0, -1800)) * (Time.unscaledDeltaTime * 10);
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
    async UniTaskVoid EnterGameTask()
    {
        await UniTask.WaitUntil(() =>Mathf.Abs(SettingCanvasBG.color.a-1)<0.004f);
        //TODO : 게임시작
        //로딩씬
        Debug.Log("게임시작");
    }
}