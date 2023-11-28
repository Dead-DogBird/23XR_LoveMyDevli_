using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;


public class SkullMonster : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string SFXCtrl;

    private FMOD.Studio.EventInstance SFXInstance;




    [SerializeField] private ColliderCallbackController MonsterBody;
    [SerializeField] private GameObject RightPos,LeftPos;
    [Header("³Ë¹é °­µµ")] [SerializeField] private float _knockbackFos;
    private Rigidbody2D _rigid;
    [SerializeField] private float speed = 3;
    int nextMove=1;
    void Start()
    {
        MonsterBody.onColliderEnter += OnTriggerEnter2DBody;
        MonsterBody.onCollisionEnter += OnCollisionEnterBody;
        _rigid = MonsterBody.GetComponent<Rigidbody2D>();

        SFXInstance = FMODUnity.RuntimeManager.CreateInstance(SFXCtrl);
    }
    void Update()
    {
        MonsterBody.transform.position += new Vector3((speed * Time.deltaTime) * nextMove, 0);
    }
    private void OnTriggerEnter2DBody(Collider2D other)
    {
        if(other.CompareTag("PosCollider"))
        {
            nextMove *= -1;
            MonsterBody.transform.localScale = nextMove > 0 ? new Vector3(-1,1) : new Vector3(1,1);
        }

        

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMove>().GetKnockBack(MonsterBody.transform.position,_knockbackFos);

        }
    }

    void OnCollisionEnterBody(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            SFXInstance.start();
            other.transform.GetComponent<PlayerMove>().GetKnockBack(MonsterBody.transform.position,_knockbackFos);
        }
    }
}
