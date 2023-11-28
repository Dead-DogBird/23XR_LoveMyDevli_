using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 toVector { get; protected set; }

    protected float speed;
    public float damage { get; protected set; }

    // protected float orbitDeleay = 0.004f;
    // protected float targetFigure;
     protected SpriteRenderer _sprite;

     private bool isFire=false;
    // Start is called before the first frame update
    void Start()
    {
        //base.Start();
        _sprite = GetComponent<SpriteRenderer>();
    }

    protected void OnEnable()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if(isFire)
            transform.Translate(toVector * (speed * Time.deltaTime), Space.World);
    }

    private void FixedUpdate()
    {
    }

    private float time;

    protected void OnDisable()
    {
    }

    public Bullet Init(Vector3 bulletpos, Vector3 _toVector, float _speed,float deleteDelay = 3)
    {
        time = 0;
        transform.position = bulletpos;
        speed = _speed;
        this.deleteDelay = deleteDelay;
        return this;
    }

    private float deleteDelay;
    public void GetFire(Vector3 _toVector)
    {
        toVector = CustomAngle.VectorRotation(CustomAngle.PointDirection(transform.position,
            _toVector));
        //transform.rotation = Quaternion.Euler(0, 0,CustomAngle.PointDirection(transform.position, _toVector));
        Destroy(gameObject,deleteDelay);
        isFire = true;
    }
    async UniTaskVoid FireTask(float _delay)
    {
        float temp = _delay;
        while (temp >= 0)
        {
            temp -= 0.1f;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
        isFire = true;
    }
}