using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Script.System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestBoss : MonoBehaviour
{

    //ToDo:
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Laser;
    public float SkillDamage;
    public bool isDie { get; protected set; } = false;


    protected delegate UniTaskVoid BossPattern();

    protected BossPattern[] BossPatterns;
    [SerializeField] private GameObject[] _toYposes;
    protected Rigidbody2D _rigid;

    protected bool isBossPattern = true;

    private bool waitTime;
    // Start is called before the first frame update
    private PlayerMove _player;

    protected void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        BossPatterns = new BossPattern[] { BossPattern1, BossPattern2, BossPattern3, BossPattern4};
        _player = BossStagePlatformController.Instance.player;
        isDie = false;
        EnterDelay().Forget();
    }
    private void OnEnable()
    {
    }

    private int PatternCount=0;
    // Update is called once per frame
    protected void Update()
    {
        if (!isBossPattern)
        {
            BossPatterns[PatternCount]().Forget();
        }
    }

    protected void OnDestroy()
    {
       
    }
    protected async UniTaskVoid EnterDelay()
    {
        isBossPattern = true;
        BossStagePlatformController.Instance.MovePlatform(0, 0, 2.5f, 3);
        BossStagePlatformController.Instance.MovePlatform(5, 0,2.5f,3);
        isBossPattern = false;
    }
    //1.사탄빔
    async UniTaskVoid BossPattern1()
    {
        isBossPattern = true;
        PatternCount++;
        WaitForSec(2.5f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        while (MathF.Abs(transform.position.y - (_toYposes[0].transform.position.y + 1)) >= 0.08f)
        {
            transform.position +=
                (new Vector3(transform.position.x, _toYposes[0].transform.position.y + 1) - transform.position) *
                (2.5f * Time.deltaTime);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
        for (int i = 0; i < 3; i++)
        {
            WaitForSec(1.5f).Forget();
            await UniTask.WaitUntil(() => waitTime);
            var temp = Instantiate(Laser, transform.position, quaternion.identity);
            temp.transform.rotation = Quaternion.Euler(0, 0,
                CustomAngle.PointDirection(transform.position,_player.transform.position));
            Destroy(temp, 1.6f);
            WaitForSec(1f).Forget();
            await UniTask.WaitUntil(() => waitTime);
        }
        isBossPattern = false;
    }
    List<Bullet> bulletList=new List<Bullet>();
    //2. 사탄 원형총알
    async UniTaskVoid BossPattern2()
    {
        isBossPattern = true;
        PatternCount++;
        BossStagePlatformController.Instance.MovePlatform(1, 1, 2.5f, 3);
        BossStagePlatformController.Instance.MovePlatform(4, 1,2.5f,3);
        WaitForSec(1.5f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        
        float tick = 0;
        for (int i = 0; i < 30; i++)
        {
            tick += 1/(360f/75);
            bulletList.Add(Instantiate(Bullet).GetComponent<Bullet>());
            bulletList.Last().Init(transform.position+new Vector3(MathF.Cos(tick)*1.5f, MathF.Sin(tick)*1.5f),_player.transform.position,20,4);
            await UniTask.Yield(PlayerLoopTiming.LastFixedUpdate);
        }
        WaitForSec(2.2f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        foreach (var _bullet in bulletList)
        {
            _bullet.GetFire(_player.transform.position);
            WaitForSec(0.005f).Forget();
            await UniTask.WaitUntil(() => waitTime);
        }
        bulletList.Clear();
        isBossPattern = false;
    }
    //3. 총알 비
    async UniTaskVoid BossPattern3()
    {
        isBossPattern = true;
        PatternCount++;
        BossStagePlatformController.Instance.MovePlatform(2, 2, 2.5f, 3);
        BossStagePlatformController.Instance.MovePlatform(3, 2,2.5f,3);
        WaitForSec(1.2f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        while (MathF.Abs(transform.position.y - (20)) >= 0.08f)
        {
            transform.position +=
                (new Vector3(transform.position.x, 20) - transform.position) *
                (2.5f * Time.deltaTime);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
        float time = 5;
        while (time >= 0)
        {
            time -= Time.deltaTime;
            if (Random.Range(0, 5) > 3)
            {
                var temp = Instantiate(Bullet).GetComponent<Bullet>();
                temp.Init(transform.position+new Vector3(Random.Range(-10.0f,10.0f), 10),_player.transform.position,Random.Range(14.0f,21.0f),10);
                temp.GetFire(temp.transform.position+new Vector3(Random.Range(-3.0f,3.0f), -20));
                
            }
            await UniTask.Yield(PlayerLoopTiming.LastFixedUpdate);
        }
        WaitForSec(1.5f).Forget();
        await UniTask.WaitUntil(() => waitTime);

        while (MathF.Abs(transform.position.y - (_toYposes[0].transform.position.y + 1)) >= 0.08f)
        {
            transform.position +=
                (new Vector3(transform.position.x, _toYposes[0].transform.position.y + 1) - transform.position) *
                (2.5f * Time.deltaTime);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
        WaitForSec(3.5f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        isBossPattern = false;
    }
    //4. 오른쪽에서 총알 우다다닥닥다닥닥ㄷ가
    async UniTaskVoid BossPattern4()
    {
        isBossPattern = true;
        PatternCount=0;
        isBossPattern = false;
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {

    }

    async UniTaskVoid WaitForSec(float time)
    {
        waitTime = false;
        float _time = time;
        while (_time >= 0)
        {
            _time -= 0.1f;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
        waitTime = true;
    }
    
}
