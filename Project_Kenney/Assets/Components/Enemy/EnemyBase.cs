using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBase : MonoBehaviour
{
    public int movePoints = 1;
    public int damage;
    public bool isMoving;

    Vector3 initialPos;
    private void Awake()
    {

        initialPos = transform.position;
    }

    private void Start()
    {
        GameHandler.instance.observer.EventNextTurn += Move;
        GameHandler.instance.observer.EventEndGame += ResetEnemy;
    }

    public virtual void ResetEnemy()
    {
        transform.position = initialPos;
        StopMove();
    }

    public void StopMove()
    {
        isMoving = false;
        StopAllCoroutines();
    }

    public virtual void Move()
    {
        //they can either move in a pattern
        //or move towarsd the player.


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if its a parent then you reset to a place.
        //if its a monter you are killed.
        if (collision.gameObject.tag != "Player") return;



        PlayerHandler.instance.resource.ReceiveDamage(damage);
    }


    

    public IEnumerator MoveProcess(Vector3 target)
    {
        isMoving = true;
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 45);
            yield return new WaitForSeconds(0.005f);
        }

        yield return new WaitForSeconds(0.2f);
        isMoving = false;
        
    }



}
