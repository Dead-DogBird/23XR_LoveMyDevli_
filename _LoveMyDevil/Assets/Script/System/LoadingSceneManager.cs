using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using FMODUnity;
using FMOD.Studio;


[System.Serializable]
struct Videos
{
    [SerializeField] public VideoClip[] video;
}

public class LoadingSceneManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string SFXCtrl;

    bool soundeffect = false; 

    private FMOD.Studio.EventInstance SFXInstance;

    public static string nextScene;
    [SerializeField] Image progressBar;
    [SerializeField] Image AnimationCurtain;
    [SerializeField] private VideoPlayer VideoPlayer;
    [SerializeField] private Videos[] videos;
    [SerializeField] private Animator CutainAnimation;
    static int cutsceneID;

    static bool OnCutSceneEnd;
    //0: 인트로,1: 1스테이지 완료,2:2스테이지 완료,3:3스테이지 완료 4: 4스테이지 완료(엔딩씬은 별도)
    private void Start()
    {
        Cursor.visible = true;
        LoadScene().Forget();
        VideoPlayer.loopPointReached += NextVideo;

        SFXInstance = FMODUnity.RuntimeManager.CreateInstance(SFXCtrl);

        SFXInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));

        SFXInstance.start();
        SFXInstance.setVolume(2);
    }

    private void Update()
    {
        if(soundeffect == true)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("soundlowhigh", 0.7f);
        }

        if(soundeffect == false)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("soundlowhigh", 2.0f);
        }
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
        Debug.Log("로딩씬 호출 됨.");
    }
    public static void LoadScene(string sceneName,int sceneID)
    {
        nextScene = sceneName;
        cutsceneID = sceneID;
        SceneManager.LoadScene("LoadingScene");
        Debug.Log("로딩씬 호출 됨.");
    }

    public static string NowSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    async UniTaskVoid LoadScene()
    {
        //progressBar.fillAmount = 0;
        Debug.Log("로딩씬 Task호출 됨.");
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        await UniTask.WaitUntil(() => op.progress>=0.9f);
        Debug.Log("로딩씬 Task 완료.");
        //progressBar.fillAmount = 0.9f;
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        //TODO:컷씬 불러오기
        OnCutSceneEnd = false;
        CutScene().Forget();
        await UniTask.WaitUntil(()=>OnCutSceneEnd);
        
        op.allowSceneActivation = true;
    }

    private int maxVideo;
    private bool nextVideo;
    async UniTaskVoid CutScene()
    {
        Debug.Log("컷씬 실행");
        int CurVideo=0;
        maxVideo = videos[cutsceneID].video.Length;
        while (true)
        {
            if (isSkip) break;
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            CutainAnimation.Play("Curtain");
            nextVideo = false;
            VideoPlayer.clip = videos[cutsceneID].video[CurVideo];
            VideoPlayer.Play();
            await UniTask.WaitUntil(()=>nextVideo);
            CurVideo++;
            if (CurVideo >= maxVideo)            
            break;
        }
        SFXInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        OnCutSceneEnd = true;
    }
    void NextVideo(VideoPlayer vp)
    {

        soundeffect = true; 
        nextVideo = true;
        //TODO: 닫았다가 여는 애니메이션 재생
        CutainAnimation.Play("CurtainClose");
        StartCoroutine(lowsound());
    }

    IEnumerator lowsound()
    {
        yield return new WaitForSeconds(1f);
        soundeffect = false;
    }


    private bool isSkip;
    public void SkipButton()
    {
        isSkip = true;
        SFXInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
