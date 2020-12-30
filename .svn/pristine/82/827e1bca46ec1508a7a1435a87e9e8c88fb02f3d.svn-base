using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blow : MonoBehaviour
{
    public LayerMask wallLayer;
    public LayerMask playerLayer;
    public LayerMask[] wallLayers;
    private bool op = false;
    public float distance = 10f;
    public static int num = 2;

    public GameObject People;
 
    //检测方向
    public static Vector3 []postion;

    public static bool ChangeWordPos = false;

    private Vector3 StartPos;

    RaycastHit hitwall;
    public float speed = 10f;

    public enum Pos
    {
        right,
        left,
        forward,
        back
    }

    public Pos pos;
    public static Pos staticpos;
    // Start is called before the first frame update
    void Awake()
    {


        postion = new Vector3[4]
        {
            Vector3.right,
            Vector3.left,
            Vector3.forward,
            Vector3.back
        };
        staticpos = pos;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (ChangeWordPos)
        {
            switch (staticpos)
            {
                case Pos.right:
                    staticpos = Pos.left;
                    ChangeWordPos = false;
                    break;
                case Pos.left:
                    staticpos = Pos.right;
                    ChangeWordPos = false;
                    break;
                case Pos.forward:
                    staticpos = Pos.back;
                    ChangeWordPos = false;
                    break;
                case Pos.back:
                    staticpos = Pos.forward;
                    ChangeWordPos = false;
                    break;

            }
        }

        //Debug.Log(staticpos);
        //开启检测
        if (op)
        {
           
            //检测方向
            Ray ray = new Ray(this.transform.position, postion[(int)staticpos]);
            People = GameObject.FindGameObjectWithTag("Player");
            Debug.Log(postion);

            //检测到人后发射射线
            if (Physics.Raycast(ray, out RaycastHit hit, distance))
            {
                if(hit.collider.gameObject.tag == "Player")
                {
                    //关闭输入
                    InputMgr.GetInstance().StartOrEndCheck(false);

                    //检测到墙
                    if (Physics.Raycast(ray, out hitwall, distance, wallLayer))
                    {
                        if (hitwall.collider.gameObject.tag == "Stone" || hitwall.collider.gameObject.tag == "Wall")
                        {
                            //获得目的地
                            Vector3 poswall = hitwall.transform.position - 1f * postion[(int)staticpos];

                            Debug.Log(poswall);

                            //芜湖 
                            //People.transform.position = Vector3.MoveTowards(People.transform.position, poswall, speed * Time.deltaTime);
                            People.transform.position = poswall;

                            //InputMgr.GetInstance().StartOrEndCheck(false);
                            //到达终点
                            if (People.transform.position == poswall || ChangeWorldCube.ChangeMapActive)
                            {

                                //开启输入
                                InputMgr.GetInstance().StartOrEndCheck(true);

                                //关闭检测
                                op = false;
                            }
                        }                                             
                    }
                    //没有检测到墙
                    else
                    {
                        //获取目的地
                        Vector3 pos = hit.transform.position + new Vector3(postion[(int)staticpos].x * distance, postion[(int)staticpos].y * distance, postion[(int)staticpos].z * distance);
                        //芜湖
                        People.transform.position = Vector3.MoveTowards(People.transform.position, pos, speed * Time.deltaTime);
                        InputMgr.GetInstance().StartOrEndCheck(false);
                        //到达终点
                        if (Vector3.Distance(People.transform.position, StartPos) >= 2f || ChangeWorldCube.ChangeMapActive)
                        {

                            //辅助画线
                            Debug.DrawLine(this.transform.position, pos, Color.red);

                            //Debug.Log("open input");
                            //开启输入
                            InputMgr.GetInstance().StartOrEndCheck(true);

                            //关闭检测
                            op = false;
                        }
                    }


                }
                
                
            }
            
        }


    }
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Player")
        {
          
            Debug.Log("吹");
            StartPos = other.transform.position;
            op = true;
        }
    }
    /// <summary>
    /// 旋转吹风机
    /// 利用key判断是第几关
    /// </summary>
    /// <param name="key">关卡数</param>
    /// 
    
}
