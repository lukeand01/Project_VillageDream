using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public int initialHealth = 3;
    int totalHealth;
    int currentHealth;
    PlayerHandler handler;
    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();
        
    }

    private void Start()
    {
        ResetResource();
    }

    public void ReceiveDamage(int damage)
    {

        //first turn it off.
        //


        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);
        UIHolder.instance.player.PlayerCaught(currentHealth, totalHealth);
    }

    void Die()
    {
        GameHandler.instance.LoseGame();
        handler.controller.StopMoving();
    }

    public void ResetResource()
    {
        totalHealth = initialHealth;
        currentHealth = totalHealth;
        UIHolder.instance.player.UpdateHealth(currentHealth, totalHealth);
    }
}
