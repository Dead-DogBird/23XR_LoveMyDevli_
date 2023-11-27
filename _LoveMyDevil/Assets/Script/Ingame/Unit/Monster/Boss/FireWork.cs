using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FireWork : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string SFXCtrl;

    private FMOD.Studio.EventInstance SFXInstance;


    [SerializeField] private GameObject bullet;

    private float speed;

    private Vector3 toVector;
    //l Start is called before the first frame update
    void Start()
    {
        SFXInstance = FMODUnity.RuntimeManager.CreateInstance(SFXCtrl);

        SFXInstance.setParameterByName("SatanSfx", 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (toVector - transform.position)*Time.deltaTime;
        if (Vector3.Distance(toVector, transform.position) < 0.8f)
        {
            Explosion();
        }
    }

    public FireWork Init(Vector3 pos, float speed)
    {
        toVector = pos + new Vector3(0, 4);
        this.speed = speed;
        return this;
    }

    private void Explosion()
    {
        int toI = Random.Range(7, 16);
        for (int i = 0; i < toI; i++)
        {
            

            SFXInstance.start();

            var temp = Instantiate(bullet).GetComponent<Bullet>();
            temp.Init(transform.position, new Vector3(0,0),3);
            temp.GetFire(temp.transform.position+new Vector3(Random.Range(-3.0f,3.0f), Random.Range(-3.0f,3.0f)));

        }
        Destroy(gameObject);
    }
}
