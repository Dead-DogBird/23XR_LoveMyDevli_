using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class openscene : MonoBehaviour
{
    public GameObject pho1;
    public GameObject pho2;

    SpriteRenderer sr1;
    SpriteRenderer sr2; 

    // Start is called before the first frame update
    void Start()
    {
        sr1 = pho1.GetComponent<SpriteRenderer>();
        sr2 = pho2.GetComponent<SpriteRenderer>();
        StartCoroutine(anim());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator anim()
    {
        yield return new WaitForSeconds(2);
        sr1.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("TitleScene");
    }
}
