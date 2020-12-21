using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public static int Ignorelayer = ~(1 << 9);//忽视玩家

    public static bool op;
    public int checkline = 1;

    public float speed = 10;
    private Vector3 IcePos, ObjPos;
    private GameObject Obj;
    private Vector3 dir, end, dirY, dt;
    private bool Check_End_Pos;

// Start is called before the first frame update

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        IcePos = this.transform.position;
        if (op && Obj != null)
        {
            Ray rayStop = new Ray(this.transform.position + new Vector3(0, 1f, 0), dt);

            if(Check_End_Pos)
            {
                if (Physics.Raycast(rayStop, out RaycastHit hitStop, checkline, Ignorelayer))
                {
                    end = IcePos + dirY;
                    Check_End_Pos = false;
                }
                else
                {
                    end = IcePos + dir;
                    Check_End_Pos = false;
                }
            }

            print("EndPos" + end);
            Obj.transform.position = Vector3.MoveTowards(Obj.transform.position, end, speed * Time.deltaTime);
            if (Obj.transform.position == end)
            {
                InputMgr.GetInstance().StartOrEndCheck(true);
                Obj = null;

            }

        }

        /*
        if (op)
        {           
            //Debug.Log(dt);
            Ray rayWall = new Ray(this.transform.position, dt);
            Ray rayStop = new Ray(this.transform.position + new Vector3(0, 1f, 0), dt);
            //检测正常地面
            if (Physics.Raycast(rayWall, out RaycastHit hitWall, checkline, floot))
            {
                Vector3 end;
                //检测有没有墙
                if (Physics.Raycast(rayStop, out RaycastHit hitStop, checkline, Wall))
                {
                     end = hitStop.transform.position - 1.5f * dt;
                }
                else
                {
                    //获得终点
                     end = hitWall.transform.position;
                }
               
                Debug.Log(end);

                //人芜湖
                if(pob)
                {
                    people.transform.position = Vector3.MoveTowards(people.transform.position, new Vector3(end.x, people.transform.position.y, end.z), 1f);
                    if ((people.transform.position.x == end.x && people.transform.position.z == end.z))
                    {
                        op = false;
                    }
                }
                //箱子芜湖
                else
                {
                    box.transform.position = Vector3.MoveTowards(box.transform.position, new Vector3(end.x, box.transform.position.y, end.z), 1f);
                    if ((box.transform.position.x == end.x && box.transform.position.z == end.z))
                    {
                        op = false;
                    }
                }

            }
        }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "IceBlock" || collision.transform.tag == "Box" || collision.transform.tag == "Stone")
        {
            InputMgr.GetInstance().StartOrEndCheck(false);
            print(collision.transform.tag);
            ObjPos = collision.transform.position;
            Obj = collision.collider.gameObject;
            dirY.y = ObjPos.y;
            GetMoveDirection();
            op = true;
            Check_End_Pos = true;
            //InputMgr.GetInstance().StartOrEndCheck(true);
        }

    }

    private void GetMoveDirection()
    {
        switch(PlayerControl.direction.ToString())
        {
            case "left":
                dir = new Vector3(-1, Obj.transform.position.y, 0);
                dt = new Vector3(-1, 0, 0);
                break;
            case "right":
                dir = new Vector3(1, Obj.transform.position.y, 0);
                dt = new Vector3(1, 0, 0);
                break;
            case "forward":
                dir = new Vector3(0, Obj.transform.position.y, 1);
                dt = new Vector3(0, 0, 1);
                break;
            case "back":
                dir = new Vector3(0, Obj.transform.position.y, -1);
                dt = new Vector3(0, 0, -1);
                break;
        }
    }

}
