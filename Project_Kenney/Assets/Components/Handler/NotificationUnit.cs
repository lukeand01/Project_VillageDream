using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationUnit : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    Vector2 originalPos;

    public void SetUp(string value)
    {
        text.text = value;
        total = 2f;
        originalPos = transform.position;
    }


    bool isMoving;
    bool isDone;

    float current;
    float total;


    private void Update()
    {
        

        if(current >= total)
        {
            if (isMoving) return;
            StartCoroutine(FadeOut());
        }
        else
        {
            current += Time.deltaTime;
        }
    }


    //starts a delay.
    public void MoveFurtherBelow()
    {
        if (isDone) return;
        StartCoroutine(MoveFurtherBelowProcess());
    }
    IEnumerator MoveFurtherBelowProcess()
    {
        //it moves up.
        isMoving = true;

        //it goes down by x.

        Vector2 firstPos = transform.position;

        while (transform.position.y > firstPos.y - Screen.height / 10)
        {
            transform.position -= new Vector3(0, 0.6f, 0);
            yield return new WaitForSeconds(0.00001f);
        }

        isMoving = false;
    }


    IEnumerator FadeOut()
    {
        //remove this fella.
        isMoving = true;
        isDone = true;
        while(transform.position.y < originalPos.y + Screen.height / 5)
        {
            transform.position += new Vector3(0, 0.6f, 0);
            yield return new WaitForSeconds(0.000001f);
        }


        Destroy(gameObject);
    }
}
