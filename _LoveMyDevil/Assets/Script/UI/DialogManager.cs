using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class DialogManagerv : MonoSingleton<DialogManagerv>
{
    [SerializeField] private Text _mainText;
    [SerializeField] private Text _nameText;
    [SerializeField] private Image _mainBoardImage;
    [SerializeField] private Image _mainProfilImage;
    [SerializeField] private Narrations[] _narrations;
    [SerializeField] private int NarrationID;
    [SerializeField] private int TextID;
    private bool isSkip=false;
    public Sprite[] Profil;
    public Sprite[] Board;
    // Start is called before the first frame update
    void Start()
    {
        _mainProfilImage.color = Color.clear;
        _mainBoardImage.color = Color.clear;
        _mainText.text = null;
        _nameText.text = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSkip&&TextID<_narrations[NarrationID]._narrations.Length)
        {
            TypingText(_narrations[NarrationID]._narrations[TextID].Text,_narrations[NarrationID]._narrations[TextID].Name,_narrations[NarrationID]._narrations[TextID].Id).Forget();
        }
    }
    async UniTaskVoid TypingText(string pText,string pName,int ID,float dur = 1.75f)
    {
        isSkip = false;
        _mainProfilImage.sprite = Profil[ID];
        _mainBoardImage.sprite = Board[ID == 0 ? 0 : 1];
        _mainProfilImage.color = Color.white;
        _mainBoardImage.color = Color.white;
        _mainText.text = null;
        _nameText.text = pName;
        _mainText.DOText(pText, 0.75f);
        await UniTask.Delay(TimeSpan.FromSeconds(dur+0.1f));
        TextID++;
        await UniTask.Delay(TimeSpan.FromSeconds(3));
        _mainProfilImage.color = Color.clear;
        _mainBoardImage.color = Color.clear;
        _mainText.text = null;
        _nameText.text = null;
        isSkip = true;
    }

    public void SetID(int id)
    {
        NarrationID = id;
        TextID = 0;
        Debug.Log("Length : "+_narrations[NarrationID]._narrations.Length);
        isSkip = true;
    }
}

[System.Serializable]
public struct Narration
{
    public string Text;
    public string Name;
    public int Id;
}
[System.Serializable]
public struct Narrations
{
    public Narration[] _narrations;
}