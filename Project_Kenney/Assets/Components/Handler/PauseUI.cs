using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    GameObject holder;

    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;
    }
    public void Control()
    {
        Debug.Log("supposed to pause");
        if (holder.activeInHierarchy)
        {
            holder.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            holder.SetActive(true);
            Time.timeScale = 0;
        }
    }


    public void Restart()
    {
        Control();
        GameHandler.instance.EndGame(true);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
