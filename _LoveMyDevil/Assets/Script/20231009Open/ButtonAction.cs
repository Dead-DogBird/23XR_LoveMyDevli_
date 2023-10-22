using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonAction : MonoBehaviour
{
    public Button button;
    public Transform sprite1;
    public Transform sprite2;
    public float moveSpeed = 15.0f;
    public float targetZ = -9.0f; // ¸ñÇ¥ Z °ª

    
    private bool isMoving = false;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = new Vector3(0f, 41.4f, targetZ);
        button.onClick.AddListener(MoveSpritesToTarget);
    }

    public void click()
    {
        MoveSpritesToTarget();
        coucaller();
    }

    IEnumerator Scenectrl()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("CutScene");
    }




   public void coucaller()
    {
        button.transform.localPosition = new Vector3(0, -620, 0);
        StartCoroutine(Scenectrl());
    }
   





    private void Update()
    {
        if (isMoving)
            return;

        if (Input.GetKeyDown(KeyCode.Insert))
        {
            MoveSpritesToTarget();
        }
    }

    private void MoveSpritesToTarget()
    {
        if (isMoving)
            return;

        isMoving = true;
    }

    private void LateUpdate()
    {
        if (isMoving)
        {
            sprite1.position = Vector3.MoveTowards(sprite1.position, targetPosition, moveSpeed * Time.deltaTime);
            sprite2.position = Vector3.MoveTowards(sprite2.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(sprite1.position, targetPosition) < 0.01f && Vector3.Distance(sprite2.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }
}
