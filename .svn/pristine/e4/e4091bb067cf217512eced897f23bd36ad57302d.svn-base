using System.Collections.Generic;
using UnityEngine;

public class ChangeIce : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject Blockfather, Icesfather;
    public static List<GameObject>ices, DarkCubes, LightCubes;
    public LayerMask floor;
    public List<Vector3> IcesPos, BlocksPos;
    //public static bool checkover = 

    //public GameObject[] Blocks, ices;
    void Awake()
    {
        //监听转换世界
        EventCenter.GetInstance().AddEventListener("ChangeWorld", Changeice);

        //实例化
       /*
       ices = new List<GameObject>();
       LightCubes = new List<GameObject>();
       DarkCubes = new List<GameObject>();
        */
        //BlocksPos = IcesPos = new List<Vector3>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void check()
    {
        ices = new List<GameObject>();
        LightCubes = new List<GameObject>();
        DarkCubes = new List<GameObject>();
        //冰块
        Blockfather = GameObject.FindWithTag("IceBlocks");
        //冰道
         Icesfather = GameObject.FindWithTag("Ices");

        //冰块变冰道
        foreach (Transform child in Blockfather.GetComponentInChildren<Transform>())
        {
            //判断是否显示
            if (child.gameObject.activeInHierarchy)
            {              
                //获得全部冰块
               // Blocks.Add(child.gameObject);
                if (Physics.Raycast(child.gameObject.transform.position, Vector3.down, out RaycastHit hit, 2f))
                {
                    //如果是冰块
                    if(hit.collider.gameObject.tag == "Ice")
                    {
                        Debug.Log("检测到下方有冰块");
                        continue;
                    }
                    //如果是地面
                    else if(hit.collider.gameObject.tag == "Floor")
                    {
                        Debug.Log("检测到下方有地板");
                        //获得地面坐标
                        Vector3 landPos = hit.collider.gameObject.transform.position;

                        //IcesPos.Add(landPos);
                        //Debug.Log("地面是" + hit.collider.gameObject.name);

                        //关闭所有地面
                        hit.collider.gameObject.SetActive(false);
                        if(Map.LW)
                        {
                            if(LightCubes.Contains(child.gameObject))
                            {
                                LightCubes.Remove(child.gameObject);
                            }
                            //关闭所有冰块                           
                            child.gameObject.SetActive(false);
                        }
                        else
                        {
                            if (DarkCubes.Contains(child.gameObject))
                            {
                                DarkCubes.Remove(child.gameObject);
                            }
                            //关闭所有冰块                           
                            child.gameObject.SetActive(false);
                        }
                        


                        //出现冰道
                        GameObject ice = PoolMgr.GetInstance().GetObjAsyc("Prefab/Ice", new Vector3(0, 0, 0), Quaternion.identity);
                        ice.name = "Ice2";
                        ice.transform.position = landPos;
                        //放入队列中
                        ices.Add(ice);
                        Debug.Log("放入了冰道"+ ice.name);
                    }
                    
                }

            }
            else
            {
                continue;
            }

        }
        //冰道变冰块
        foreach (Transform child in Icesfather.GetComponentInChildren<Transform>())
        {
            //判断是否显示
            if (child.gameObject.activeInHierarchy)
            {
                if(!ices.Contains(child.gameObject))
                {
                    ices.Add(child.gameObject);
                }                
                if (Physics.Raycast(child.gameObject.transform.position, Vector3.up, out RaycastHit hit, 2f))
                {
                    //如果是冰块
                    if(hit.collider.gameObject.tag == "IceBlock" || hit.collider.gameObject.tag == "Box")
                    {
                        Debug.Log("检测到上方有冰块");
                        continue;
                    }
                    //如果不是                                     
                }
                else
                {
                    Debug.Log("检测成功 可以变成冰块");
                    //获得全部冰道
                    //获得冰块放置的位置
                    Vector3 Pos = child.position + new Vector3(0, 1f, 0);
                    //BlocksPos.Add(Pos);
                    //生成地面
                    Debug.Log("生成冰块");

                    //进入里世界
                    //生成的地面

                    //若在明世界
                    if(Map.LW)
                    {
                        GameObject Darkcube = PoolMgr.GetInstance().GetObjAsyc("Prefab/Floor/DarkCube_Ice", new Vector3(0f, 0f, 0f), Quaternion.identity);
                        Darkcube.name = "DarkCube_Ice";
                        Darkcube.transform.position = child.position;
                        DarkCubes.Add(Darkcube);
                        /*
                        GameObject Darkcubes = GameObject.FindWithTag("DarkCubes");
                        Darkcube.transform.parent = Darkcubes.transform;
                        Debug.Log("生成暗地面");
                        */
  
                    }                     
                    else
                    {
                        GameObject Lightcube = PoolMgr.GetInstance().GetObjAsyc("Prefab/Floor/Cube_Ice", new Vector3(0f, 0f, 0f), Quaternion.identity);
                        Lightcube.name = "Cube";
                        Lightcube.transform.position = child.position;
                        LightCubes.Add(Lightcube);
                        /*
                        GameObject Lightcubes = GameObject.FindWithTag("LightCubes");
                        cube.transform.parent = Lightcubes.transform;
                        Debug.Log("生成明地面");
                        */

                    }



                    //冰道消失
                    child.gameObject.SetActive(false);
                    ices.Remove(child.gameObject);

                    //生成冰块
                    GameObject iceBlock = PoolMgr.GetInstance().GetObjAsyc("Prefab/IceBlock", new Vector3(0f, 0f, 0f), Quaternion.identity);
                    iceBlock.name = "IceBlock";
                    iceBlock.transform.parent = Blockfather.transform;
                    iceBlock.transform.position = Pos;
                }
               

            }
            else
            {
                continue;
            }
        }
        
       
        
    }
    
    void Changeice(object key)
    {
        int num = (int)key+1;
        if (num == 8 || num == 9 || num == 10)
        {
            //进入暗世界
            if (!Map.LW)
            {
                /*
                if (ices != null)
                {
                    //Icesfather = GameObject.FindWithTag("Ices");
                    foreach (GameObject ice in ices)
                    {
                        Debug.Log("加入的冰块有" + ice.name);
                        if(ice.transform.parent != Icesfather.transform)
                            ice.transform.parent = Icesfather.transform;
                    }
                    //ices.Clear();
                }
                */

                check();
            }

            //回到光世界
            else
            {
                /*
                if (ices != null)
                {
                    //Icesfather = GameObject.FindWithTag("Ices");
                    foreach (GameObject ice in ices)
                    {
                        if (ice.transform.parent != Icesfather.transform)
                            ice.transform.parent = Icesfather.transform;
                    }
                   // ices.Clear();
                }
                */
                check();
                
            }
        }
        else
            return;
        
    }
    
   
}
