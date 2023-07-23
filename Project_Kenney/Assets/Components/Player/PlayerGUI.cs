using JetBrains.Annotations;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
    [Separator("HEART")]
    [SerializeField] Transform heartContainer;
    [SerializeField] Image fullHeart;
    [SerializeField] Image emptyHeart;


    [Separator("SOUND")]
    [SerializeField] AudioClip caughtClip;
    [SerializeField] AudioClip deadClip;

    public void UpdateHealth(int current, int total)
    {
        ClearUI();
        for (int i = 0; i < total; i++)
        {
            
            if(current == 0)
            {
                Image newObject = Instantiate(emptyHeart, heartContainer.position, Quaternion.identity);
                newObject.transform.parent = heartContainer;
            }
            else
            {
                Image newObject = Instantiate(fullHeart, heartContainer.position, Quaternion.identity);
                newObject.transform.parent = heartContainer;
                current--;
            }

        }     

    }

    

    IEnumerator HealthShakeProcess()
    {
        Vector3 originalPos = heartContainer.transform.position;

        float current = 0;
        float total = 0.3f;

        while(current < total)
        {
            float x = Random.Range(-1.2f, 1.2f);
            float y = Random.Range(-0.3f, 0.3f);
            heartContainer.transform.position = new Vector3(originalPos.x + x, originalPos.y + y, 0);
            current += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }

        heartContainer.transform.position = originalPos;

    }


    void ClearUI()
    {
        for (int i = 0; i < heartContainer.childCount; i++)
        {
            Destroy(heartContainer.GetChild(i).gameObject);
        }
    }

    [Separator("DARK BACKGROUND")]
    [SerializeField] Image darkBackground;

    public void PlayerCaught(int current, int total)
    {
        //first we turn it to black.
        StartCoroutine(CaughtProcess(current, total));

    }

    IEnumerator CaughtProcess(int current, int total)
    {
        //first turn the ebackground on.
        PlayerHandler.instance.block.AddBlock("Caught", BlockClass.BlockType.Complete);

        darkBackground.color = new Color(darkBackground.color.r, darkBackground.color.g, darkBackground.color.b, 0);
        darkBackground.gameObject.SetActive(true);

        while(darkBackground.color.a < 1)
        {
            darkBackground.color += new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);
        UpdateHealth(current, total);
        StartCoroutine(HealthShakeProcess());
        yield return new WaitForSeconds(0.5f);

        if(current <= 0 || PlayerHandler.instance.progress >= 100)
        {
            //
            GameHandler.instance.LoseGame();
            PlayerHandler.instance.block.RemoveBlock("Caught");
            GameHandler.instance.CreateSFX(deadClip);
            GameHandler.instance.observer.OnEndGame();
            darkBackground.gameObject.SetActive(false);
            yield return null;
        }
        GameHandler.instance.EndGame();
        GameHandler.instance.CreateSFX(caughtClip);

        while (darkBackground.color.a > 0)
        {
            darkBackground.color -= new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        PlayerHandler.instance.block.RemoveBlock("Caught");
        darkBackground.gameObject.SetActive(false);

    }




    [Separator("Turn")]
    [SerializeField] GameObject turnHolder;

    public void UpdateTurn(bool isPlayerTurn)
    {

    }

    [Separator("PROGRESS")]
    [SerializeField] Image progressBar;

    public void UpdateProgress(float current, float total)
    {
        progressBar.fillAmount = current / total;
    }

}
