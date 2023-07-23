using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] GameObject winHolder;
    [SerializeField] GameObject loseHolder;
    [SerializeField] AudioClip victoryClip;
    public void Won()
    {
        Time.timeScale = 0;
        PlayerHandler.instance.block.AddBlock("End", BlockClass.BlockType.Complete);
        GameHandler.instance.CreateSFX(victoryClip);
        GameHandler.instance.ControlAudioSource(false);
        winHolder.SetActive(true);
        loseHolder.SetActive(false);
    }
    public void Lost()
    {
        Time.timeScale = 0;
        PlayerHandler.instance.block.AddBlock("End", BlockClass.BlockType.Complete);
        winHolder.SetActive(false);
        loseHolder.SetActive(true);
    }



    public void PlayAgain()
    {

        Time.timeScale = 1;
        PlayerHandler.instance.block.RemoveBlock("End");

        winHolder.SetActive(false);
        loseHolder.SetActive(false);

        GameHandler.instance.EndGame(true);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
