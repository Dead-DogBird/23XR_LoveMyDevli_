using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

[System.Serializable]
struct Videos
{
    [SerializeField] VideoClip[] video;
}

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Image progressBar;
    [SerializeField] Image AnimationCurtain;
    [SerializeField] private GameObject VideoPlayer;
    [SerializeField] private Videos[] videos;
    private int cutsceneID;
    private void Start()
    {
        LoadScene().Forget();
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
        Debug.Log("로딩씬 호출 됨.");
    }

    public static string NowSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    async UniTaskVoid LoadScene()
    {
        progressBar.fillAmount = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount += (op.progress - progressBar.fillAmount) * (Time.unscaledDeltaTime * 10);
            }
            else
            {
                break;
            }
            await UniTask.Yield(PlayerLoopTiming.LastUpdate);
        }
        progressBar.fillAmount = 0.9f;
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        //TODO:컷씬 불러오기
        op.allowSceneActivation = true;
    }

    void NextVideo()
    {
        
    }
}
