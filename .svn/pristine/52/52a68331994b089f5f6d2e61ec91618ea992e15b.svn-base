using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    public static int RestartLayer = ~(1 << 29 | 1 << 28);//忽略重启区域
    private Vector3 StartPos;
    public float checkWall = 1f;
    public static bool flag = false;

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        flag = Fall(Vector3.down);
        CheckFall();

        if (Physics.Raycast(transform.position, new Vector3(0, -1f, 0), out hit, checkWall))
        {
            if (hit.transform.tag == "Ice")
            {
                print("滑冰");
                if (this.GetComponent<Rigidbody>().velocity.magnitude == 0)//停在冰上
                {
                    InputMgr.GetInstance().StartOrEndCheck(true);
                }

                else
                {
                    InputMgr.GetInstance().StartOrEndCheck(false);
                }
            }
            else
            {
                //print("到终点");
                InputMgr.GetInstance().StartOrEndCheck(true);
            }
        }
    }
    //下落检测
    bool Fall(Vector3 pos)
    {
        if (!Physics.Raycast(transform.position, new Vector3(0, -1f, 0), checkWall, RestartLayer))
        {

            return true;
        }
        else
        {

            return false;
        }
    }


    void CheckFall()
    {
        if (flag)
        {
            if (InputMgr.isStart == false)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                EventCenter.GetInstance().RomoveEventListener("Keydown", PlayerControl.CheckInputDown);
                GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;
            }

        }
        else
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
