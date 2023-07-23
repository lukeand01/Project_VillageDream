using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public int currentScene;

    [SerializeField] GameObject loadingScreen;

    GameHandler handler;

    private void Awake()
    {
        handler = GetComponent<GameHandler>();
        handler.ChooseBackgroundMusic(currentScene);
    }

    public void ChangeScene(int load, string fromWhere)
    {
        StartCoroutine(ChangeSceneProcess(load));
    }


    IEnumerator ChangeSceneProcess(int load)
    {
        loadingScreen.SetActive(true);

       

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive);


        while (!loadOperation.isDone)
        {
            yield return null;
        }

        while(PlayerHandler.instance == null && load != 0)
        {
            yield return null;
        }
       PlayerHandler.instance.block.AddBlock("ChangeScene", BlockClass.BlockType.Complete);


        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(load));
        SceneManager.UnloadSceneAsync(currentScene);

        currentScene = load;

        yield return new WaitForSeconds(0.2f);

        handler.ChooseBackgroundMusic(load);

        if(load == 1)
        {
            handler.CallTutorial();
        }

        loadingScreen.SetActive(false);       

        PlayerHandler.instance.block.RemoveBlock("ChangeScene");


    }


}
