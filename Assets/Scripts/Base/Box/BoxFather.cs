using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BoxFather : MonoBehaviour
{
    public bool CanMove = true;

    public GameObject Box;
    public Vector3 Target;
    public Vector3 DeDir = Vector3.zero;
    public List<Vector3> BaseDir;
    public int BoxLayer;
    public enum BoxType
    {
        //宝箱
        Treasure,
        //石头
        Stone,
        //火
        FireBox,
        //吹风机
        Blower
    }

    public BoxType Boxtype;



    /// <summary>
    /// 设置id
    /// </summary>
    public void Init(BoxType type, GameObject box)
    {
        Boxtype = type;
        Box = box;
        
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
    /*
    /// <summary>
    /// 盒子移动
    /// </summary>
    /// <param name="Box"></param>
    /// <param name="Player"></param>
    /// <param name="controller"></param>
    public void Move(GameObject Box, int Ignore)
    {
        float raylen = 0.55f;
        RaycastHit hit;
        Vector3 Target;
        if (Physics.Raycast(Box.transform.position, Vector3.forward, out hit, raylen, Ignore))
        {
            BoxCanMove(Vector3.back, Ignore);
            if (hit.transform.CompareTag("Player") && CanMove)
            {

                Target = Box.transform.position + new Vector3(0, 0, -1f);
                Target = Player_Controller.RoundV(Target);
                Box.transform.position = Vector3.MoveTowards(Box.transform.position, Target, 4f * Time.deltaTime);
                if (Vector3.Distance(Box.transform.position, Target) < 0.15f)
                    Box.transform.position = Player_Controller.RoundV(Box.transform.position);

            }
        }
        else if (Physics.Raycast(Box.transform.position, Vector3.back, out hit, raylen, Ignore))
        {
            BoxCanMove(Vector3.forward, Ignore);
            if (hit.transform.CompareTag("Player") && CanMove)
            {
                Target = Box.transform.position + new Vector3(0, 0, 1f);
                Target = Player_Controller.RoundV(Target);
                Box.transform.position = Vector3.MoveTowards(Box.transform.position, Target, 4f * Time.deltaTime);


            }
        }
        else if (Physics.Raycast(Box.transform.position, Vector3.right, out hit, raylen, Ignore))
        {
            BoxCanMove(Vector3.left, Ignore);
            if (hit.transform.CompareTag("Player") && CanMove)
            {
                Target = Box.transform.position + new Vector3(-1f, 0, 0);
                Target = Player_Controller.RoundV(Target);
                Box.transform.position = Vector3.MoveTowards(Box.transform.position, Target, 4f * Time.deltaTime);
                Debug.Log(Boxtype + "移动了");
            }
        }
        else if (Physics.Raycast(Box.transform.position, Vector3.left, out hit, raylen, Ignore))
        {
            BoxCanMove(Vector3.right, Ignore);
            if (hit.transform.CompareTag("Player") && CanMove)
            {
                Target = Box.transform.position + new Vector3(1f, 0, 0);
                Target = Player_Controller.RoundV(Target);

                Box.transform.position = Vector3.MoveTowards(Box.transform.position, Target, 4f * Time.deltaTime);
                if (Vector3.Distance(Box.transform.position, Target) < 0.5f)
                    Box.transform.position = Player_Controller.RoundV(Box.transform.position);
            }
        }
        

    }
    */

    public void NewMove(int Ignore)
    {
        if(DeDir != Vector3.zero)
        {
            if(BoxCanMove(DeDir,Ignore))
            {             
                Box.transform.position = Vector3.MoveTowards(Box.transform.position, Target, 4f * Time.deltaTime);
                if (Vector3.Distance(Box.transform.position, Target) < 0.15f)
                    Box.transform.position = Player_Controller.RoundV(Box.transform.position);
            }

        }
    }


    public void Fall(GameObject Box)
    {
        
        if (!Physics.Raycast(Box.transform.position, Vector3.down, 1f, Player_Controller.RestartLayer))
        {
            Debug.Log("下水了");
            Box.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;
        }
        else
        {
            Box.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            BaseDir = new List<Vector3> { Vector3.forward, Vector3.back, Vector3.right, Vector3.left};
            Vector3 dir = Box.transform.position - collision.collider.transform.position;
            dir = Player_Controller.RoundV(dir);
            Debug.Log(dir);
            if (BaseDir.Contains(dir))
            {
                DeDir = dir;
                Target = Box.transform.position + DeDir;
            }
        }

    }

}
