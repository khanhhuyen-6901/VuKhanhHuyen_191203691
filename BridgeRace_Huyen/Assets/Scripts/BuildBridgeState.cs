using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridgeState : IState
{

    public void OnEnter(Enemy enemy)
    {

    }

    public void OnExcute(Enemy enemy)
    {
        if (enemy.currentStage == null) return;
        Vector3 bridgePoint = enemy.enemyPoint.position;
        enemy.enemy.SetDestination(bridgePoint);

    }

    public void OnExit(Enemy enemy)
    {

    }
}
