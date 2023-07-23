using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    
    public void Play()
    {
        GameHandler.instance.loader.ChangeScene((int)SceneIndex.House, "play");
    }
    public void Quit()
    {
        Application.Quit();
    }


}
