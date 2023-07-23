using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UIHolder : MonoBehaviour
{
    public static UIHolder instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public PlayerGUI player;
    public Journal journal;
    public PauseUI pause;
    public WinLoseUI winLose;
    public NotificationUI notification;
    public TutorialUI tutorial;
}
