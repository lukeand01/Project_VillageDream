using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Door targetDoor;
    [SerializeField] Vector3Int dir;
    [SerializeField] string title;
    [SerializeField] AudioClip clip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //get it to the next scene.

        if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "Ghost") 
        targetDoor.ReceivePlayer(collision.gameObject);
        
    }

    public void ReceivePlayer(GameObject target)
    {
        if(target.GetComponent<PlayerHandler>() != null)
        {
            PlayerHandler.instance.controller.StopMoving();
            UIHolder.instance.notification.ReceiveOrder("Welcome to the " + title);
            GameHandler.instance.CreateSFX(clip);
        }
        if (target.GetComponent<EnemyBase>() != null) target.GetComponent<EnemyBase>().StopMove();

        target.transform.position = transform.position + dir;
        
    }
}
