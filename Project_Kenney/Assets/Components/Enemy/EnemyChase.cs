using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChase : EnemyBase
{
    //this enemy chases at intervals. it always know the player is.
    [SerializeField] float speed;
    float current;

    bool active;
    float activationCurrent;
    float activationTotal = 5;


    [SerializeField]SpriteRenderer rend;
    [SerializeField]Image fillImage;
    [SerializeField] Color notReadyColor;

    private void Awake()
    {
        rend.color = notReadyColor;
        fillImage.gameObject.SetActive(true);
    }


    private void OnDestroy()
    {
        GameHandler.instance.observer.EventEndGame -= ResetEnemy;
    }
    private void Update()
    {

        if (!active)
        {
            if(activationCurrent > activationTotal)
            {
                active = true;
                rend.color = Color.white;
            }
            else
            {
                activationCurrent += Time.deltaTime;
                fillImage.fillAmount = activationCurrent / activationTotal;
            }
            return;
        }

        if (isMoving) return;
        if(current >= speed)
        {
            //then wee get a list
           List<MyNode> pathList = MyPathfind.instance.GetPathThroughVector(transform.position, PlayerHandler.instance.transform.position);
            StartCoroutine(MoveProcess(pathList[0].transform.position));
            current = 0;
        }
        else
        {
            current += Time.deltaTime;           
        }

        fillImage.fillAmount = current / speed;
    }


    
    public override void Move()
    {
        //it always goes after the player.
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        Destroy(gameObject);
    }
}
