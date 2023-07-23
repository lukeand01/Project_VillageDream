using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if we can in thee game.
        if (collision.gameObject.tag != "Player") return;

        if(PlayerHandler.instance.progress >= 100)
        {
            //win game.
            GameHandler.instance.WinGame();
        }

    }

}
