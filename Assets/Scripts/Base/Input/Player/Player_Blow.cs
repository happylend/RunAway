using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player_Blow : StateBase<PlayerState>
{
    public Player_Controller player;
    GameObject PlayerObject;
    private Vector3 TargetPos;
    public float moveSpeed = 8f;
    public float timer = 1.0f;
    public override void Init(FSMController<PlayerState> controller, PlayerState StateType)
    {
        base.Init(controller, StateType);
        player = controller as Player_Controller;
    }

    // Start is called before the first frame update
    public override void OnEnter()
    {
  
        RaycastHit hit;
        if (Physics.Raycast(player.Tran(), Player_Controller.BlowDir, out hit, 10f, Player_Controller.IgnoreAirWall))
        {
            TargetPos = hit.transform.position -= Player_Controller.BlowDir;
            Debug.Log("飞行目标是" + TargetPos);
        }
    }

    public override void OnExit()
    {
        Player_Controller.CanBlow = false;
    }

    public override void OnUpdate()
    {
        this.Blow(TargetPos);
        if (player.transform.position == TargetPos)
        {


                player.MovePoint.position = TargetPos;
                player.ChangeState<Player_Move>(PlayerState.Player_Move);
            

        }

    }

    private void Blow(Vector3 TargetPos)
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, TargetPos, moveSpeed * Time.deltaTime);


    }
}
