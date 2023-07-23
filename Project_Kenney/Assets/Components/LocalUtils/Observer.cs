using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{

    public event Action EventNextTurn;
    public void OnNextTurn() => EventNextTurn?.Invoke();

    public event Action EventEndGame;
    public void OnEndGame() => EventEndGame?.Invoke();

}
