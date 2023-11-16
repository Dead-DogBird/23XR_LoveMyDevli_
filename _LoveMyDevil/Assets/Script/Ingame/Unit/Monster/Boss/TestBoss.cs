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
    [SerializeField] private GameObject RepeatFireObj;
    [SerializeField] private GameObject FireWorkObj;
    [SerializeField] private GameObject Laser;
    [SerializeField] private GameObject SprayItem;
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
        //BossStagePlatformController.Instance.MovePlatform(0, 0, 2.5f, 3);
       // BossStagePlatformController.Instance.MovePlatform(5, 0,2.5f,3);
        isBossPattern = false;
    }
    //1.사탄빔
    async UniTaskVoid BossPattern1()
    {
        isBossPattern = true;
        PatternCount++;
        WaitForSec(2.5f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        //불똥 올라오는 패턴
        for (int i=0; i < 6; i++)
        {
            Instantiate(Bullet).GetComponent<Bullet>()
                .Init(new Vector3(-8.5f + (i * 1.5f), -5), new Vector3(-8.5f + (i * 1.5f), 20), 15, 4).GetFire(new Vector3(-8.5f + (i * 1.5f), 20));
            Instantiate(Bullet).GetComponent<Bullet>()
                .Init(new Vector3(8.5f - (i * 1.5f), -5), new Vector3(8.5f - (i * 1.5f), 20), 15, 4).GetFire(new Vector3(8.5f - (i * 1.5f), 20));
            
            WaitForSec(1.5f).Forget();
            await UniTask.WaitUntil(() => waitTime);
        }
        WaitForSec(3f).Forget();
        await UniTask.WaitUntil(() => waitTime);

        float pos;
        // 뭐시기 날라오는 패턴
        for (int i=0; i < 4; i++)
        {
            pos = (Random.Range(0, 2) == 1) ? -8.5f : 8.5f;
           for(int j=0;j<3;j++)
           {
               Instantiate(Bullet).GetComponent<Bullet>()
                .Init(new Vector3(pos, -3.75f+j*0.2f), new Vector3(pos*-1, -3.75f), 15, 4).
                GetFire(new Vector3(pos*-1 , -3.75f+j*0.2f));
           }
           WaitForSec(1.5f).Forget();
            await UniTask.WaitUntil(() => waitTime);
        }
        
        
        //레이저 패턴
        WaitForSec(1.5f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        for (int i = 0; i < 10; i++)
        {
            var temp = Instantiate(Laser, new Vector3(-8.5f+i*2.5f,20), quaternion.identity);
            temp.transform.rotation = Quaternion.Euler(0, 0,
                CustomAngle.PointDirection(temp.transform.position, temp.transform.position-new Vector3(0,10)));
            Destroy(temp, 1.6f);
            WaitForSec(0.5f).Forget();
            await UniTask.WaitUntil(() => waitTime);
        }
        
        // await UniTask.WaitUntil(() => waitTime);
        // while (MathF.Abs(transform.position.y - (_toYposes[0].transform.position.y + 1)) >= 0.08f)
        // {
        //     transform.position +=
        //         (new Vector3(transform.position.x, _toYposes[0].transform.position.y + 1) - transform.position) *
        //         (2.5f * Time.deltaTime);
        //     await UniTask.Yield(PlayerLoopTiming.Update);
        // }
        //
        // for (int i = 0; i < 3; i++)
        // {
        //     WaitForSec(1.5f).Forget();
        //     await UniTask.WaitUntil(() => waitTime);
        //     var temp = Instantiate(Laser, transform.position, quaternion.identity);
        //     temp.transform.rotation = Quaternion.Euler(0, 0,
        //         CustomAngle.PointDirection(transform.position, _player.transform.position));
        //     Destroy(temp, 1.6f);
        //     WaitForSec(1f).Forget();
        //     await UniTask.WaitUntil(() => waitTime);
        // }
        Instantiate(SprayItem, new Vector3(Random.Range(-1.16f, 1.16f), -3.3f), Quaternion.identity);
        isBossPattern = false;
    }
    List<Bullet> bulletList=new List<Bullet>();
    //2. 
    async UniTaskVoid BossPattern2()
    {
        isBossPattern = true;
        PatternCount++;
        BossStagePlatformController.Instance.MovePlatform(0, 3, 1.25f, 3);
        BossStagePlatformController.Instance.MovePlatform(1, 3, 1.25f, 3);
        BossStagePlatformController.Instance.MovePlatform(4, 3, 1.25f, 3);
        BossStagePlatformController.Instance.MovePlatform(5, 3, 1.25f, 3);
        WaitForSec(4.5f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        RepeatFire().Forget();
        WaitForSec(3.5f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        float pos;
        for (int i = 0; i < 3; i++)
        {
            pos = Random.Range(0, 2)==1 ? 1.6f : -1.6f;
            var temp = Instantiate(Laser, new Vector3(pos,4.25f), quaternion.identity);
            temp.transform.rotation = Quaternion.Euler(0, 0,
                CustomAngle.PointDirection(temp.transform.position, temp.transform.position+new Vector3(0,-4.25f)));
            Destroy(temp, 1.6f);
            WaitForSec(3.5f).Forget();
            await UniTask.WaitUntil(() => waitTime);

        }
        // for (int i = 0; i < 30; i++)
        // {
        //     tick += 1/(360f/75);
        //     bulletList.Add(Instantiate(Bullet).GetComponent<Bullet>());
        //     bulletList.Last().Init(transform.position+new Vector3(MathF.Cos(tick)*1.5f, MathF.Sin(tick)*1.5f),_player.transform.position,20,4);
        //     await UniTask.Yield(PlayerLoopTiming.LastFixedUpdate);
        // }
        // WaitForSec(2.2f).Forget();
        // await UniTask.WaitUntil(() => waitTime);
        // foreach (var _bullet in bulletList)
        // {
        //     _bullet.GetFire(_player.transform.position);
        //     WaitForSec(0.005f).Forget();
        //     await UniTask.WaitUntil(() => waitTime);
        // }
        //bulletList.Clear();
        Instantiate(SprayItem, new Vector3(Random.Range(-6f, 6f), -3.3f), Quaternion.identity);
        endRepeatFire = true;
        isBossPattern = false;
    }
    //3. 
    async UniTaskVoid BossPattern3()
    {
        isBossPattern = true;
        PatternCount++;
        for (int i = 0; i < 6; i++)
        {
            BossStagePlatformController.Instance.MovePlatform(i, 4, 3.25f, 3);
        }
        WaitForSec(3.2f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        for (int i = 0; i < 4; i++)
        {
            var temp = Instantiate(Laser, new Vector3(-1.6f-i*2f,4.25f), quaternion.identity);
            temp.transform.rotation = Quaternion.Euler(0, 0,
                CustomAngle.PointDirection(temp.transform.position, temp.transform.position+new Vector3(0,-4.25f)));
            Destroy(temp, 1.6f);
        }
        WaitForSec(3.5f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        for (int i = 0; i < 4; i++)
        {
            var temp = Instantiate(Laser, new Vector3(1.6f+i*2f,4.25f), quaternion.identity);
            temp.transform.rotation = Quaternion.Euler(0, 0,
                CustomAngle.PointDirection(temp.transform.position, temp.transform.position+new Vector3(0,-4.25f)));
            Destroy(temp, 1.6f);
        }
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
        WaitForSec(1.5f).Forget();
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
                CustomAngle.PointDirection(transform.position, _player.transform.position));
            Destroy(temp, 1.6f);
            WaitForSec(1f).Forget();
            await UniTask.WaitUntil(() => waitTime);
        }
        WaitForSec(2f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        int random = Random.Range(5, 9);
        for (int i = 0; i < random; i++)
        {
            var pos = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-5.0f, -4.0f));
            var temp =  Instantiate(FireWorkObj,pos,
                Quaternion.identity);
            temp.GetComponent<FireWork>().Init(pos,Random.Range(10, 15));
            WaitForSec(1.2f).Forget();
            await UniTask.WaitUntil(() => waitTime);
        }
        WaitForSec(3f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        Instantiate(SprayItem, new Vector3(Random.Range(-6f, 6f), -3.3f), Quaternion.identity);
        isBossPattern = false;
    }
   
    async UniTaskVoid BossPattern4()
    {
        isBossPattern = true;
        PatternCount=0;
        WaitForSec(3f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        //TODO : 화면 어두워지는 셰이더 적용 할 것
        for (int i = 0; i < 3; i++)
        {
            WaitForSec(1.5f).Forget();
            await UniTask.WaitUntil(() => waitTime);
            var temp = Instantiate(Laser, transform.position, quaternion.identity);
            temp.transform.rotation = Quaternion.Euler(0, 0,
                CustomAngle.PointDirection(transform.position, _player.transform.position));
            Destroy(temp, 1.6f);
            WaitForSec(1f).Forget();
            await UniTask.WaitUntil(() => waitTime);
        }
        WaitForSec(3f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        
        for (int i = 0; i < 3; i++)
        {
            //X레이저
            var temp = Instantiate(Laser, new Vector3(-8.5f,_player.transform.position.y), quaternion.identity);
            temp.transform.localScale *= 0.5f;
            temp.transform.rotation = Quaternion.Euler(0, 0,
                CustomAngle.PointDirection(temp.transform.position, temp.transform.position+new Vector3(8,0)));
            Destroy(temp, 1.6f);
            //Y레이저
            var temp_ = Instantiate(Laser, new Vector3(_player.transform.position.x,4.25f), quaternion.identity);
            temp_.transform.localScale *= 0.5f;
            temp_.transform.rotation = Quaternion.Euler(0, 0,
                CustomAngle.PointDirection(temp_.transform.position, temp_.transform.position+new Vector3(0,-8)));
            Destroy(temp_, 1.6f);
            WaitForSec(2.5f).Forget();
            await UniTask.WaitUntil(() => waitTime);
        }
        
        WaitForSec(3f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        while (Vector3.Distance(new Vector3(8.32f,0),transform.position) >= 0.08f)
        {
            transform.position +=
                (new Vector3(8.32f,0) - transform.position) *
                (2.5f * Time.deltaTime);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
        WaitForSec(1.5f).Forget();
        await UniTask.WaitUntil(() => waitTime);
        float time = 5;
        while (time >= 0)
        {
            time -= Time.deltaTime;
            if (Random.Range(0, 5) > 3)
            {
                var temp = Instantiate(Bullet).GetComponent<Bullet>();
                temp.Init(transform.position+new Vector3(10, Random.Range(-10.0f,10.0f)),_player.transform.position,Random.Range(14.0f,21.0f),10);
                temp.GetFire(temp.transform.position+new Vector3(-20,Random.Range(-3.0f,3.0f)));
                
            }
            await UniTask.Yield(PlayerLoopTiming.LastFixedUpdate);
        }
        Instantiate(SprayItem, new Vector3(Random.Range(-6f, 6f), -3.3f), Quaternion.identity);
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

    private bool endRepeatFire;
    async UniTaskVoid RepeatFire()
    {
        endRepeatFire = false;
        var temp = Instantiate(RepeatFireObj, new Vector3(-9, -3, 0), Quaternion.identity);
        var temp2 = Instantiate(RepeatFireObj, new Vector3(9, -3, 0), Quaternion.identity);

        float tick = 0;
        while (!endRepeatFire)
        {
            tick += 0.008f;
            temp.transform.position = new Vector3(temp.transform.position.x + MathF.Cos(tick) * 0.2f, temp.transform.position.y);
            temp2.transform.position = new Vector3(temp2.transform.position.x - MathF.Cos(tick) * 0.2f, temp2.transform.position.y);

            await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        }
        Destroy(temp.gameObject);
        Destroy(temp2.gameObject);

    }
}
