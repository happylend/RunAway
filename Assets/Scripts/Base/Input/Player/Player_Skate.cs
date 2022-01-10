using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skate : StateBase<PlayerState>
{
    Player_Controller Player;
    public static Vector3 TargetPos = Vector3.zero;
    bool Isfall;
    float h, v;

    public override void Init(FSMController<PlayerState> controller, PlayerState StateType)
    {
        base.Init(controller, StateType);
        Player = controller as Player_Controller;
    }

    public override void OnEnter()
    {
        Isfall = false;
        var CurY = Player.Tran().y - 1f;
        Vector3 curVector = new Vector3(Player.Tran().x, CurY, Player.Tran().z);
        //检测到了前方空地
        if (Physics.Raycast(curVector, Player_Controller.GetDir(Player_Move.dir), out RaycastHit hit, 10f, Player_Controller.IgnoreIceAir))
        {

            float IceLen = Vector3.Distance(curVector, hit.collider.transform.position);
            Debug.Log("长度是" + IceLen);
            //前方有物体挡住
            if (Physics.Raycast(Player.transform.position, Player_Controller.GetDir(Player_Move.dir), out RaycastHit hitCod, IceLen, Player_Controller.IgnoreIceAir))
            {
                Debug.Log("冰路上有障碍");
                TargetPos = hitCod.transform.position - Player_Controller.GetDir(Player_Move.dir);
            }
            else
            {
                Debug.Log("滑冰冲！");
                TargetPos = new Vector3(hit.collider.transform.position.x, Player.transform.position.y, hit.collider.transform.position.z);

            }
        }
        else if (Physics.Raycast(Player.Tran(), Player_Controller.GetDir(Player_Move.dir), out RaycastHit hitt, 10f))
        {
            if (Physics.Raycast(Player.Tran(), Player_Controller.GetDir(Player_Move.dir), out RaycastHit hitCod, Player_Controller.IgnoreAirWall))
            {
                Debug.Log("滑下去冰路上有障碍");
                TargetPos = new Vector3(hitCod.transform.position.x - Player_Controller.GetDir(Player_Move.dir).x, Player.Tran().y, hitCod.transform.position.z - Player_Controller.GetDir(Player_Move.dir).z);
            }
            else
            {
                if (Mathf.Abs(Player_Controller.GetDir(Player_Move.dir).x) == 1)
                {
                    var TargetX = hitt.collider.transform.position.x;
                    TargetPos = new Vector3(TargetX, Player.Tran().y, Player.Tran().z);
                    Debug.Log("滑向为X");
                }
                else if (Mathf.Abs(Player_Controller.GetDir(Player_Move.dir).z) == 1)
                {
                    var TargetZ = hitt.collider.transform.position.z;
                    TargetPos = new Vector3(Player.Tran().x, Player.Tran().y, TargetZ);
                    Debug.Log("滑向为X");
                }
                else
                {
                    Debug.LogError("箱子方位出错了");
                    Time.timeScale = 0;
                }
                TargetPos = Player_Controller.RoundV(TargetPos);
            }
        }
    }

    public override void OnExit()
    {
        Isfall = false;
        
    }

    public override void OnUpdate()
    {
        
        if(TargetPos != Vector3.zero)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, TargetPos, 8f * Time.deltaTime);
            if(!Physics.Raycast(Player.Tran(),Vector3.down,1f,Player_Controller.RestartLayer))
            {
                TargetPos = Player_Controller.RoundV(Player.Tran());
                Isfall = true;
            }
            if(Vector3.Distance(Player.transform.position, TargetPos)<0.20f)
            {
                Debug.Log("目标是+" + TargetPos);
                Player.MovePoint.position = TargetPos;
                Player.transform.position = TargetPos;
                Player_Controller.CanSkate = false;
                TargetPos = Vector3.zero;
                if (Isfall) Player.ChangeState<Player_Fall>(PlayerState.Player_Fall);
                else Player.ChangeState<Player_Move>(PlayerState.Player_Move);
            }
        }
    }
}
