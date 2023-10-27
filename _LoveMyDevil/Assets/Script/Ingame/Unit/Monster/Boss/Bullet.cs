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

    public GameObject Init(Vector3 bulletpos, Vector3 _toVector, float _speed, float delay = 0,float deleteDelay = 3)
    {

        time = 0;
        transform.position = bulletpos;
        toVector = CustomAngle.VectorRotation(CustomAngle.PointDirection(bulletpos,
            _toVector));
        //damage = getinfo.damage;
        speed = _speed;
        // _sprite.color = getinfo.bulletColor;
        // orbitColor = getinfo.orbitcolors;
        // targetFigure = getinfo.targetFigure;
        transform.rotation = Quaternion.Euler(0, 0,
            CustomAngle.PointDirection(bulletpos, _toVector));
        FireTask(delay).Forget();
        Destroy(gameObject,deleteDelay);
        return gameObject;
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