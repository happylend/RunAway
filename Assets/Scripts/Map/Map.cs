 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    [Header("---关卡测试专用变量（第一关为0）---")]
    public static int Map_Num;
    // Start is called before the first frame update
    public static int num = 2;


    //定义坐标系
    private int MapCount = 11;
    public GameObject[] LightMap, DarkMap, DoubleMap = new GameObject[12];

    public static GameObject Cubefather;
    public static bool Lmap = true;
    //public GameObject Cubefather;
    //public int MaxRandomNum = 3;
    //private List<int> randomNum;
    //储存位置
    public List<string> LightWorld;
    public List<string> DarkWorld;
    public List<string> DoubleWorld ;

    //灯光
    public Light Light_light, Dark_light;

    public static bool LW = true;

    //吹风机变量
    public static Transform parent;
    public static List<GameObject> blow;

    //雪山关变量

    public GameObject Lightcubes;
    public GameObject Darkcubes;
    public GameObject Icesfather;
    void Start()
    {
        //num = 0;
        Light_light.gameObject.SetActive(true);
        Dark_light.gameObject.SetActive(false);


        LightWorld = new List<string>();
        DarkWorld = new List<string>();
        DoubleWorld = new List<string>();
        blow = new List<GameObject>();

        for (int i = 1; i <= MapCount; i++)
        {
            LightWorld.Add("Prefab/Level_" + i + "/LightWorld");
            DarkWorld.Add("Prefab/Level_" + i + "/DarkWorld");
            DoubleWorld.Add("Prefab/Level_" + i + "/DoubleWorld");
        }

        Debug.Log("加载了第" + MapNum.Start_Num + "关");

        //开启输入检测
        InputMgr.GetInstance().StartOrEndCheck(true);

        //EventCenter.GetInstance().AddEventListener("Keydown", PlayerControl.CheckInputDown);

        EventCenter.GetInstance().AddEventListener("InitMap", InitWorld);

        //EventCenter.GetInstance().AddEventListener("InitMap", BGM.PlayBK);

        EventCenter.GetInstance().AddEventListener("RestartGame", RestartGame);
        //添加事件中心检测
        EventCenter.GetInstance().AddEventListener("ChangeMap", ChangeMap);

        EventCenter.GetInstance().AddEventListener("ChangeWorld", ChangePos);


        EventCenter.GetInstance().AddEventListener("ChangeWorld", ChangeWorld);

        EventCenter.GetInstance().AddEventListener("fail", RestartGame);

        //增加事件中心触发
        EventCenter.GetInstance().EventTrigger("InitMap", MapNum.Start_Num);
    }
    
    void InitWorld(object key)
    {
        int MapNum = (int)key;
        //压入缓存池 同步加载
        //初始值是0
        Debug.Log("加载成功" + MapNum);
        //Debug.Log(");


        LightMap[MapNum] = PoolMgr.GetInstance().GetObjAsyc(LightWorld[MapNum], new Vector3(0, 0, 0), Quaternion.identity);
        DoubleMap[MapNum] = PoolMgr.GetInstance().GetObjAsyc(DoubleWorld[MapNum], new Vector3(0, 0, 0), Quaternion.identity);



        //异步加载
        PoolMgr.GetInstance().GetObj(DarkWorld[MapNum], new Vector3(0, 0, 0), Quaternion.identity, (obj) =>
        {
            DarkMap[MapNum] = obj;
            DarkMap[MapNum].SetActive(false);
            Dark_light.gameObject.SetActive(false);
        });


    }

   
    /// <summary>
    /// 改变地图
    /// </summary>
    /// <param name="key"></param>
    void ChangeMap(object key)
    {
        int nextnum = (int)key + 1;
        PoolMgr.GetInstance().Clear();
        Destroy(LightMap[nextnum - 1]);
        Destroy(DoubleMap[nextnum - 1]);
        Destroy(DarkMap[nextnum - 1]);
        Destroy(GameObject.FindGameObjectWithTag("Point"));

        //将新的地图压入缓存池
        LightMap[nextnum] = PoolMgr.GetInstance().GetObjAsyc(LightWorld[nextnum], new Vector3(0, 0, 0), Quaternion.identity);
        DoubleMap[nextnum] = PoolMgr.GetInstance().GetObjAsyc(DoubleWorld[nextnum], new Vector3(0, 0, 0), Quaternion.identity);
        PoolMgr.GetInstance().GetObj(DarkWorld[nextnum], new Vector3(0, 0, 0), Quaternion.identity, (obj) =>
        {
            DarkMap[nextnum] = obj;
            DarkMap[nextnum].SetActive(false);

        });
    }

    /// <summary>
    /// 改变场景
    /// </summary>
    /// <param name="key"></param>
    void ChangeWorld(object key)
    {
        int num = (int)key + 1;       
        if(LW)
        {
            LightMap[num-1].SetActive(false);
            DarkMap[num-1].SetActive(true);

            Light_light.gameObject.SetActive(false);
            Dark_light.gameObject.SetActive(true);

            //雪山关
            if (num == 8 ||num == 9 || num == 10)
            {
                /*   
                Darkcubes = GameObject.FindWithTag("DarkCubes");
                Icesfather = GameObject.FindWithTag("Ices");
                if(ChangeIce.DarkCubes != null)
                {
                    foreach (GameObject DarkCube in ChangeIce.DarkCubes)
                    {
                        //Debug.Log("加入的暗地面有" + DarkCube.name);
                        if (DarkCube.transform.parent != Darkcubes.transform)
                            DarkCube.transform.parent = Darkcubes.transform;
                    }
                }
             
                if (ChangeIce.ices != null)
                {
                    foreach (GameObject ice in ChangeIce.ices)
                    {
                        Debug.Log("加入的冰块有" + ice.name);
                        if (ice.transform.parent != Icesfather.transform)
                            ice.transform.parent = Icesfather.transform;
                    }
                }
                */
                
            }
            FallCheck.CanFall = true;
            LW = false;                       
        }
        else
        {         
            DarkMap[num-1].SetActive(false);
            LightMap[num-1].SetActive(true);

            Light_light.gameObject.SetActive(true);
            Dark_light.gameObject.SetActive(false);
            //雪山关
            if (num == 8 || num == 9 || num == 10)
            {
                /*
                Lightcubes = GameObject.FindWithTag("LightCubes");
                Icesfather = GameObject.FindWithTag("Ices");
                foreach (GameObject LightCube in ChangeIce.LightCubes)
                {
                    Debug.Log("加入的光地面有" + LightCube.name);
                    if (LightCube.transform.parent != Lightcubes.transform)
                        LightCube.transform.parent = Lightcubes.transform;
                }
                if (ChangeIce.ices != null)
                {
                    foreach (GameObject ice in ChangeIce.ices)
                    {
                        Debug.Log("加入的冰块有" + ice.name);
                        if (ice.transform.parent != Icesfather.transform)
                            ice.transform.parent = Icesfather.transform;
                    }
                }
                */
            }
            FallCheck.CanFall = true;
            LW = true;

        }
        ChangeWorldCube.ChangeMapActive = false;
    }
   
    //key为场景值。第一关为0，第二关为1
    void RestartGame(object key)
    {
        Light_light.gameObject.SetActive(true);
        Dark_light.gameObject.SetActive(false);

        //重置转换世界关卡
        LW = true;

        int nextnum = (int)key;
        PoolMgr.GetInstance().Clear();
        Destroy(LightMap[nextnum]);
        Destroy(DoubleMap[nextnum]);
        Destroy(DarkMap[nextnum]);
        Destroy(GameObject.FindGameObjectWithTag("Point"));
        //雪山关
        /*
        if(nextnum == 7||nextnum == 8)
        {
            Destroy(ChangeIce.Icesfather);
            Destroy(ChangeIce.Blockfather);
            
            ChangeIce.DarkCubes.Clear();
            ChangeIce.LightCubes.Clear();
            ChangeIce.ices.Clear();
            
        }
        */
        LightMap[nextnum] = PoolMgr.GetInstance().GetObjAsyc(LightWorld[nextnum], new Vector3(0, 0, 0), Quaternion.identity);
        DoubleMap[nextnum] = PoolMgr.GetInstance().GetObjAsyc(DoubleWorld[nextnum], new Vector3(0, 0, 0), Quaternion.identity);
        num = 0;
        PoolMgr.GetInstance().GetObj(DarkWorld[nextnum], new Vector3(0, 0, 0), Quaternion.identity, (obj) =>
        {
            DarkMap[nextnum] = obj;
            DarkMap[nextnum].SetActive(false);

        });


    }
    /// <summary>
    /// 吹风机机制
    /// </summary>
    /// <param name="key"></param>
    public static void ChangePos(object key)
    {  
        int numcount = (int)key;
        if(numcount == 4)
        {
            num++;
            parent = GameObject.FindWithTag("Blows").transform;
            if (parent == null)
                return;
            foreach (Transform child in parent.GetComponentInChildren<Transform>())
            {
                print("Blow" + child.name);
                blow.Add(child.gameObject);

            }

            foreach (GameObject child in blow)
            {
                if (num % 2 == 0)
                {
                    child.transform.rotation = Quaternion.Euler(Vector3.up * 0);
                }
                else
                {
                    child.transform.rotation = Quaternion.Euler(Vector3.up * 180);
                }
            }

            //检测射线旋转  
            Blow.ChangeWordPos = true;

            blow.Clear();
        }


    }

    public void DestroyListener()
    {
        EventCenter.GetInstance().RomoveEventListener("InitMap", InitWorld);
        //EventCenter.GetInstance().RomoveEventListener("InitMap", InitWorld);
        EventCenter.GetInstance().RomoveEventListener("RestartGame", RestartGame);
        EventCenter.GetInstance().RomoveEventListener("ChangeMap", ChangeMap);
        EventCenter.GetInstance().RomoveEventListener("ChangeWorld", ChangePos);
        EventCenter.GetInstance().RomoveEventListener("ChangeWorld", ChangeWorld);
        EventCenter.GetInstance().RomoveEventListener("ChangeWorld", IBClass.ChangeChild);
        //EventCenter.GetInstance().RomoveEventListener("ChangeWorld", ChangeIce.Changeice);
        EventCenter.GetInstance().RomoveEventListener("fail", RestartGame);
        
    }
}
