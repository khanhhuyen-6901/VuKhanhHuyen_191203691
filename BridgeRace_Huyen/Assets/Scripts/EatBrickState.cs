using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBrickState : IState
{
    private Vector3 pointBrick;
    public void OnEnter(Enemy enemy)
    {

    }

    public void OnExcute(Enemy enemy)
    {
        if (enemy.currentStage == null) return;
        foreach (GameObject obj in enemy.currentStage.listBrick[enemy.characterMaterial.color])
        {
           
            if (!enemy.bricks.Contains(obj))
            {
                pointBrick = obj.transform.position;
                break;
            }
        }
        //Debug.Log(pointBrick);
        enemy.enemy.SetDestination(pointBrick);
    }

    public void OnExit(Enemy enemy)
    {

    }
}
