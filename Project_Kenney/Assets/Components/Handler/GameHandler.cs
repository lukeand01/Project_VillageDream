using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;


    [HideInInspector]public SceneLoader loader;
    [HideInInspector]public Observer observer;


    public List<EnemyBase> enemiesList = new();

    public bool isPlayerTurn;

    AudioSource backgroundMusic;

    [SerializeField] AudioClip mainMenuClip;
    [SerializeField] AudioClip gameClip;


    bool alreadyShowedTutorial;

    public void CallTutorial()
    {
        if (alreadyShowedTutorial) return;
        alreadyShowedTutorial = true;
        UIHolder.instance.tutorial.OpenTutorial();
    }


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        

        loader = GetComponent<SceneLoader>();
        observer = GetComponent<Observer>();
    }

    public void ChooseBackgroundMusic(int choice)
    {
       if(backgroundMusic == null) backgroundMusic = gameObject.AddComponent<AudioSource>();

        backgroundMusic.Stop();
        if(choice == 0)
        {
            backgroundMusic.clip = mainMenuClip;

        }

        if(choice == 1)
        {
            backgroundMusic.clip = gameClip;
        }

        backgroundMusic.Play();
    }

    private void Start()
    {
       GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var item in array)
        {
            enemiesList.Add(item.GetComponent<EnemyBase>());
        }

        observer.EventNextTurn += Turn;

    }


    private void Update()
    {
        if (!isPlayerTurn)
        {
            if (AllEnemiesMoved())
            {
                isPlayerTurn = true;
            }
        }
    }

    void Turn()
    {
        isPlayerTurn = false;
    }

    public void CreateSFX(AudioClip clip, float volume = 1)
    {
        GameObject newObjecet = new GameObject();
        newObjecet.AddComponent<SFXUnit>().SetUp(clip, volume);
    }

    public void LoseGame()
    {
        UIHolder.instance.winLose.Lost();
    }
    public void WinGame()
    {
        UIHolder.instance.winLose.Won();
    }
   

    public void EndGame(bool restart = false)
    {

        if (restart)
        {
            PlayerHandler.instance.resource.ResetResource();
        }

        PlayerHandler.instance.ResetPlayer(restart);

    }


    [SerializeField] Image darkBackground;

   

    public bool AllEnemiesMoved()
    {
        foreach (var item in enemiesList)
        {
            if (item.isMoving) return false;
        }
        return true;
    }


    [Separator("GHOST")]
    [SerializeField] EnemyChase ghostTemplate;
    
    public void SpawnGhostAt(Vector3 pos)
    {
        Instantiate(ghostTemplate, pos, Quaternion.identity);

    }


    public void ControlAudioSource(bool choice)
    {
        if (choice)
        {
            backgroundMusic.Play();
        }
        else
        {
            backgroundMusic.Stop();
        }
    }

}
