using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class BoxFather : MonoBehaviour
{
    /*
    public BoxFather(BoxType type, GameObject box)
    {
        Boxtype = type;
        Box = box;
        Isfall = false;
    }
    */

    public GameObject Box;
    
    public int BoxLayer;
    public float Speed = 5f;

    public Transform LightCubeF, IceBlockF, DarkCubeF,IceF;


    public Vector3 TargetPos = Vector3.zero;
    public Vector3 DeDir = Vector3.zero;
    public Vector3 Target;
    public List<Vector3> BaseDir;

    public bool CanSkate = false;
    public bool Isfall = false;
    public bool CanMove = true;


    public enum BoxType
    {
        //宝箱
        Treasure,
        //石头
        Stone,
        //火
        FireBox,
        //吹风机
        Blower,
        //冰块
        IceBlock,
        //草
        Grass
    }

    public BoxType Boxtype;


    public void Init(BoxType type, GameObject box)
    {
        Boxtype = type;
        Box = box;
        Isfall = false;
    }

    /// <summary>
    /// 获得id
    /// </summary>
    /// <returns></returns>
    public BoxType GetId()
    {
        return Boxtype;
    }

    /// <summary>
    /// 判断盒子是否能移动
    /// </summary>
    /// <param name="Box"></param>
    /// <param name="h"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public bool BoxCanMove(Vector3 dir, int Ignore)
    {
        RaycastHit Barhit;
        //检测到有物体
        if (Physics.Raycast(Box.transform.position, dir, out Barhit, 0.6f, Ignore))
        {
            return false;
        }
        else return true;
    }

    public void NewMove(int Ignore)
    {
        if (DeDir != Vector3.zero && BoxCanMove(DeDir, Ignore) && !CanSkate)
        {
            Box.transform.position = Vector3.MoveTowards(Box.transform.position, Target, Speed * Time.deltaTime);
            if (Vector3.Distance(Box.transform.position, Target) < 0.15f)
            {
                Debug.Log("方向是+" + DeDir + "被碰的目标是" + Target);
                Box.transform.position = Target;
                Target = DeDir = Vector3.zero;
                TargetPos = Vector3.zero;
            }
        }

    }
    
    /// <summary>
    /// 坠落函数
    /// </summary>
    /// <param name="Box"></param>
    public void Fall(GameObject Box)
    {
        
        if (!Physics.Raycast(Box.transform.position, Vector3.down, 1f, Player_Controller.RestartLayer))
        {
            Box.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;
        }
        else if(Physics.Raycast(Box.transform.position, Vector3.down, 1f, Player_Controller.RestartLayer))
        {
            Box.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

    }
    
    /// <summary>
    /// 返回箱子坐标
    /// </summary>
    /// <returns></returns>
    public Vector3 Tran()
    {
        return Box.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            DeDir = Player_Controller.GetDir(Player_Move.dir);
            Target = Player_Controller.RoundV(Box.transform.position + DeDir);
        }
        else if (collision.collider.tag == "Ice")
        {

            CanSkate = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&&!Physics.Raycast(Tran(), Player_Controller.GetDir(Player_Move.dir),0.6f))
        {
            DeDir = Player_Controller.GetDir(Player_Move.dir);
            Target = Player_Controller.RoundV(Tran() + DeDir);
        }
        else if (other.tag == "Ice")
        {
            CanSkate = true;
        }
    }
    /// <summary>
    /// 是否可以滑冰
    /// </summary>
    /// <returns></returns>
    public bool canSkate()
    {
        if (CanSkate && TargetPos == Vector3.zero) return true;
        else return false;       
    }

    /// <summary>
    /// 滑冰判断终点
    /// </summary>
    public void SkateInit()
    {
        //检测到人
        if (Physics.Raycast(Box.transform.position, -1*DeDir,0.6f,Player_Controller.IgnoreIceAir))
        {
            Debug.Log("当前箱子的方位是"+DeDir);
            var CurY = Box.transform.position.y - 1f;
            var curVector = new Vector3(Box.transform.position.x, CurY, Box.transform.position.z);
            if (Physics.Raycast(curVector, DeDir, out RaycastHit hit, 10f, Player_Controller.IgnoreIceAir))
            {

                float IceLen = Vector3.Distance(curVector, hit.collider.transform.position);
                Debug.Log("长度是" + IceLen);
                //前方有物体挡住
                if (Physics.Raycast(Box.transform.position, DeDir, out RaycastHit hitCod, IceLen))
                {
                    Debug.Log("冰路上有障碍");
                    TargetPos = hitCod.transform.position - DeDir;
                }
                else
                {
                    Debug.Log("滑冰冲！");
                    TargetPos = new Vector3(hit.collider.transform.position.x, Box.transform.position.y, hit.collider.transform.position.z);

                }
            }
            //冰块直通到底
            else if (Physics.Raycast(Box.transform.position, DeDir, out RaycastHit hitt, 10f))
            {
                if (Physics.Raycast(Box.transform.position, DeDir, out RaycastHit hitCod ,Player_Controller.IgnoreAirWall))
                {
                    Debug.Log("滑下去冰路上有障碍");
                    TargetPos = new Vector3( hitCod.transform.position.x - DeDir.x, Box.transform.position.y, hitCod.transform.position.z - DeDir.z);
                }
                else
                {
                    if(Mathf.Abs(DeDir.x) == 1)
                    {
                        var TargetX = hitt.collider.transform.position.x-DeDir.x;
                        TargetPos = new Vector3(TargetX, Box.transform.position.y, Box.transform.position.z);
                    }
                    else if(Mathf.Abs(DeDir.z) == 1)
                    {
                        var TargetZ = hitt.collider.transform.position.z-DeDir.z;
                        TargetPos = new Vector3(Box.transform.position.x, Box.transform.position.y, TargetZ);
                    }
                }

            }
            TargetPos = Player_Controller.RoundV(TargetPos);
            Debug.Log("箱子目标滑倒" + TargetPos);
        }
    }
   
    /// <summary>
    /// 滑冰
    /// </summary>
    public void SkateMove()
    {
        if (CanSkate && TargetPos != Vector3.zero)
        {
            Box.transform.position = Vector3.MoveTowards(Box.transform.position, TargetPos, 10f * Time.deltaTime);
            if(Vector3.Distance(Box.transform.position, TargetPos)<0.17f)
            {
                Box.transform.position = TargetPos;
                Debug.Log("开始滑冰" + Box.transform.position);         
                CanSkate = false;
                TargetPos = Vector3.zero;                
            }           
        }
    }


}
