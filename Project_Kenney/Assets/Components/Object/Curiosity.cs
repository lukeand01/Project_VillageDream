using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Curiosity : MonoBehaviour
{
    [SerializeField] float progressValue;
    [SerializeField] bool ghostTrigger;
    [ConditionalField(nameof(ghostTrigger))]
    [SerializeField]
    Transform pos;
    
    GameObject graphic;
    bool isUsed;

    private void Awake()
    {
        graphic = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        GameHandler.instance.observer.EventEndGame += ResetCurisiosity;
    }


    void ResetCurisiosity()
    {
        isUsed = false;
        graphic.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        if (isUsed) return;

        if (ghostTrigger)
        {
            //theen we preparee to summon a ghost.
            GameHandler.instance.SpawnGhostAt(pos.position);

        }
        UIHolder.instance.notification.ReceiveOrder("you gained " + progressValue + " Progress");
        PlayerHandler.instance.GainProgress(progressValue);
        



        graphic.SetActive(false);
        isUsed = true;
    }

}
