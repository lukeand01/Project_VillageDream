using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool isMoving;
    PlayerHandler handler;

    [SerializeField] AudioClip walkClip;

    private void Awake()
    {
        SetUpKeys();
        handler = GetComponent<PlayerHandler>();
    }
    private void Update()
    {
        if (handler.block.HasBlock(BlockClass.BlockType.Complete)) return;

        PauseInput();
        JournalInput();

        if (handler.block.HasBlock(BlockClass.BlockType.Partial)) return;

        MoveInput();
        
        
    }


    #region KEYCODES
    KeyCode keyMoveLeft;
    KeyCode keyMoveRight;
    KeyCode keyMoveUp;
    KeyCode keyMoveDown;
    KeyCode keyJournal;
    KeyCode keyPause;

    public KeyCode GetKey(string id)
    {
        switch (id)
        {
            case "MoveLeft":
                return keyMoveLeft;
            case "MoveRight":
                return keyMoveRight;
            case "MoveUp":
                return keyMoveUp;
            case "MoveDown":
                return keyMoveDown;
            case "Pause":
                return keyPause;
            case "Journal":
                return keyJournal;
        }

        return KeyCode.None;
    }

    void ChangeKey(string id, KeyCode key)
    {

    }

    void SetUpKeys()
    {
        keyMoveLeft = KeyCode.A;
        keyMoveRight = KeyCode.D;
        keyMoveUp = KeyCode.W;
        keyMoveDown = KeyCode.S;

        keyJournal = KeyCode.Tab;
        keyPause = KeyCode.Escape;
        
    }
    #endregion


    void JournalInput()
    {
        if (Input.GetKeyDown(GetKey("Journal")))
        {
            UIHolder.instance.journal.Control();
        }
    }
    void PauseInput()
    {
        if (Input.GetKeyDown(GetKey("Pause")))
        {
            Debug.Log("pause");
            UIHolder.instance.pause.Control();
        }
    }

    void MoveInput()
    {
        //check if there is a tile by the side
        //move one tile and then allow everyone to move.

        if (isMoving) return;

        if (Input.GetKeyDown(GetKey("MoveUp")))
        {
            HandleMovement(Vector3.up);
            return;
        }
        if (Input.GetKeyDown(GetKey("MoveDown")))
        {
            HandleMovement(Vector3.down);
            return;
        }
        if (Input.GetKeyDown(GetKey("MoveRight")))
        {
            HandleMovement(Vector3.right);
            return;
        }
        if (Input.GetKeyDown(GetKey("MoveLeft")))
        {
            HandleMovement(Vector3.left);
            return;
        }

    }
    void HandleMovement(Vector3 dir)
    {

        if(!MyPathfind.instance.HasTileInPos(transform.position + dir))
        {

            return;
        }
 
        StartCoroutine(MovingProcess(transform.position + dir));
    }

    public void StopMoving()
    {
        isMoving = false;
        StopAllCoroutines();
    }

    IEnumerator MovingProcess(Vector3 target)
    {
        isMoving = true;
        GameHandler.instance.CreateSFX(walkClip, 0.6f);
        while(transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 45);
            yield return new WaitForSeconds(0.005f);
        }

        isMoving = false;
        GameHandler.instance.observer.OnNextTurn();

    }

}
