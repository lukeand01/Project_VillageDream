using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    //just a simplee teext that wwhen you click it goes away.


    GameObject holder;

    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;
    }

    public void OpenTutorial()
    {
        holder.SetActive(true);
        Time.timeScale = 0;
        PlayerHandler.instance.block.AddBlock("Tutorial", BlockClass.BlockType.Complete);
    }

    void CloseTutorial()
    {
        holder.SetActive(false);
        Time.timeScale = 1;
        PlayerHandler.instance.block.RemoveBlock("Tutorial");

    }


    private void Update()
    {
        if (!holder.activeInHierarchy) return;
        //
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //we close this.
            CloseTutorial();
        }
    }


}
