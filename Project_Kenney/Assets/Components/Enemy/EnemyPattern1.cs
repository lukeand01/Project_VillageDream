using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern1 : EnemyBase
{
    //the pattern 1 is horizontal. when it reachs the limit it goes back.


    int currentDir = 1;
    public Vector3 Dir;
    public override void Move()
    {
        
        if(!MyPathfind.instance.HasTileInPos(transform.position + (Dir * currentDir)))
        {
            currentDir *= -1;
        }

        StartCoroutine(MoveProcess(transform.position + (Dir * currentDir)));

    }
}
