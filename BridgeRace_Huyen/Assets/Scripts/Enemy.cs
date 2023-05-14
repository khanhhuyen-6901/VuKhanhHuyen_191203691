using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    findBrick,
    moveToBridge
}
public class Enemy : Controller
{
    public NavMeshAgent enemy;
    public EnemyState enemyState;
    private IState currentState;

    public Transform enemyPoint;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        ChangeAnim("Idle");
        if (GameManager.instance.isStart == true)
        {
            enemyState = EnemyState.findBrick;
            ChangeState(new EatBrickState());
        }

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (GameManager.instance.isFinish == true)
        {
            GameManager.instance.isStart = false;

        }
        if (GameManager.instance.isStart == false) return;
        if (currentState != null)
        {
            currentState.OnExcute(this);
        }

        if (bricks.Count > Random.Range(5, 8))
        {
            enemyState = EnemyState.moveToBridge;
            ChangeState(new BuildBridgeState());
            ChangeAnim("Run");

        }
        if (bricks.Count == 0)
        {
            enemyState = EnemyState.findBrick;
            ChangeState(new EatBrickState());
            ChangeAnim("Run");

        }
        
    }

    public void ChangeState(IState newState)
    {
        // ham chuyen trang thai cua enemy
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }



}
