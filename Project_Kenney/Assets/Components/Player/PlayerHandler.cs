using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    public static PlayerHandler instance;

    [HideInInspector]public PlayerController controller;
    [HideInInspector]public PlayerResource resource;

    public BlockClass block;

    public float progress;
    public bool isTransition;
    public void GainProgress(float newValue)
    {
        progress += newValue;
        UpdateProgress();

        if(progress >= 100)
        {
            UIHolder.instance.notification.ReceiveOrder("You are satisfied. lets return home");

            //summon an additional ghost.
        }
    }
    public void UpdateProgress()
    {
        UIHolder.instance.player.UpdateProgress(progress, 100);
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        controller = GetComponent<PlayerController>();
        resource = GetComponent<PlayerResource>();

        block = new BlockClass();

        
    }

    private void Start()
    {
        UpdateProgress();
    }


    public void ResetPlayer(bool choice)
    {
        transform.position = new Vector3(-0.5f, -0.5f, 0);

        if (choice)
        {
            progress = 0;
            UpdateProgress();
        }

        GameHandler.instance.ControlAudioSource(true);

        controller.StopMoving();
    }
}
