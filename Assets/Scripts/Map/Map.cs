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
    public static bool MapComplete = false;

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

    void Start()
    {
        //num = 0;
        Light_light.gameObject.SetActive(true);
        Dark_light.gameObject.SetActive(false);


        LightWorld = new List<string>();
        DarkWorld = new List<string>();
        DoubleWorld = new List<string>();

        for (int i = 1; i <= MapCount; i++)
        {
            LightWorld.Add("Prefab/Level_" + i + "/LightWorld");
            DarkWorld.Add("Prefab/Level_" + i + "/DarkWorld");
            DoubleWorld.Add("Prefab/Level_" + i + "/DoubleWorld");
        }

        Debug.Log("加载了第" + MapNum.Start_Num + "关");

        EventCenter.GetInstance().AddEventListener("InitMap", InitWorld);
        

        EventCenter.GetInstance().AddEventListener("RestartGame", RestartGame);
        //添加事件中心检测
        EventCenter.GetInstance().AddEventListener("ChangeMap", ChangeMap);

        

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

        var BlockTran = GameObject.FindObjectsOfType<IceBlock>();
        foreach (var item in BlockTran)
        {
            EventCenter.GetInstance().RomoveEventListener("BChangeI", item.BChangeI);
            Debug.Log("清除了监听");
        }
        var IceTran = GameObject.FindObjectsOfType<Ice>();
        foreach (var item in IceTran)
        {
            EventCenter.GetInstance().RomoveEventListener("IChangeB", item.IChangeB);
            Debug.Log("清除了监听");
        }
        LightMap[MapNum] = PoolMgr.GetInstance().GetObjAsyc(LightWorld[MapNum], new Vector3(0, 0, 0), Quaternion.identity);
        DoubleMap[MapNum] = PoolMgr.GetInstance().GetObjAsyc(DoubleWorld[MapNum], new Vector3(0, 0, 0), Quaternion.identity);

        //重置胜利
        Treasure.Iswin = false;

        


        
        //异步加载
        PoolMgr.GetInstance().GetObj(DarkWorld[MapNum], new Vector3(0, 0, 0), Quaternion.identity, (obj) =>
        {
            DarkMap[MapNum] = obj;
            DarkMap[MapNum].SetActive(false);
            Dark_light.gameObject.SetActive(false);
        });
        //重置胜利


    }

   
    /// <summary>
    /// 改变地图
    /// </summary>
    /// <param name="key"></param>
    void ChangeMap(object key)
    {
        int nextnum = (int)key + 1;
        MapNum.Start_Num = nextnum;
        PoolMgr.GetInstance().Clear();
        Destroy(LightMap[nextnum - 1]);
        Destroy(DoubleMap[nextnum - 1]);
        Destroy(DarkMap[nextnum - 1]);
        Destroy(GameObject.FindGameObjectWithTag("Point"));
        if(nextnum-1 == 7 || nextnum-1 == 8)
        {
            var BlockTran = GameObject.FindObjectsOfType<IceBlock>();
            foreach (var item in BlockTran)
            {
                EventCenter.GetInstance().RomoveEventListener("BChangeI", item.BChangeI);
                Debug.Log("清除了监听");
            }
            var IceTran = GameObject.FindObjectsOfType<Ice>();
            foreach (var item in IceTran)
            {
                EventCenter.GetInstance().RomoveEventListener("IChangeB", item.IChangeB);
                Debug.Log("清除了监听");
            }
        }
        //重置胜利
        Treasure.Iswin = false;


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
        int num = (int)key;
        MapNum.Start_Num = num;
        if (LW)
        {
            LightMap[num].SetActive(false);
            DarkMap[num].SetActive(true);

            Light_light.gameObject.SetActive(false);
            Dark_light.gameObject.SetActive(true);

            MapComplete = true;
            LW = false;                       
        }
        else
        {         
            DarkMap[num].SetActive(false);
            LightMap[num].SetActive(true);

            Light_light.gameObject.SetActive(true);
            Dark_light.gameObject.SetActive(false);

            MapComplete = true;
            LW = true;

        }
        if (num == 7 || num == 8)
        {
            EventCenter.GetInstance().EventTrigger("IChangeB", MapNum.Start_Num);

            var types = GameObject.FindObjectsOfType<Ice>();
            foreach (var item in types)
            {
                item.CanChange = true;
            }
        }

    }
   
    //key为场景值。第一关为0，第二关为1
    void RestartGame(object key)
    {
        Light_light.gameObject.SetActive(true);
        Dark_light.gameObject.SetActive(false);

        //重置转换世界关卡
        LW = true;

        //重置胜利
        Treasure.Iswin = false;

        int nextnum = (int)key;
        if (nextnum == 7 || nextnum == 8)
        {
            var BlockTran = GameObject.FindObjectsOfType<IceBlock>();
            foreach (var item in BlockTran)
            {
                EventCenter.GetInstance().RomoveEventListener("BChangeI", item.BChangeI);
                Debug.Log("清除了监听");
            }
            var IceTran = GameObject.FindObjectsOfType<Ice>();
            foreach (var item in IceTran)
            {
                EventCenter.GetInstance().RomoveEventListener("IChangeB", item.IChangeB);
                Debug.Log("清除了监听");
            }
        }
        PoolMgr.GetInstance().Clear();


        Destroy(LightMap[nextnum]);
        Destroy(DoubleMap[nextnum]);
        Destroy(DarkMap[nextnum]);
        Destroy(GameObject.FindGameObjectWithTag("Point"));



        LightMap[nextnum] = PoolMgr.GetInstance().GetObjAsyc(LightWorld[nextnum], new Vector3(0, 0, 0), Quaternion.identity);
        DoubleMap[nextnum] = PoolMgr.GetInstance().GetObjAsyc(DoubleWorld[nextnum], new Vector3(0, 0, 0), Quaternion.identity);
        num = 0;
        PoolMgr.GetInstance().GetObj(DarkWorld[nextnum], new Vector3(0, 0, 0), Quaternion.identity, (obj) =>
        {
            DarkMap[nextnum] = obj;
            DarkMap[nextnum].SetActive(false);
        });


    }
    public void DestroyListener()
    {
        EventCenter.GetInstance().RomoveEventListener("InitMap", InitWorld);
        EventCenter.GetInstance().RomoveEventListener("RestartGame", RestartGame);
        EventCenter.GetInstance().RomoveEventListener("ChangeMap", ChangeMap);
        EventCenter.GetInstance().RomoveEventListener("ChangeWorld", ChangeWorld);
        EventCenter.GetInstance().RomoveEventListener("fail", RestartGame);
        var BlockTran = GameObject.FindObjectsOfType<IceBlock>();
        foreach (var item in BlockTran)
        {
            EventCenter.GetInstance().RomoveEventListener("BChangeI", item.BChangeI);
            Debug.Log("清除了监听");
        }
        var IceTran = GameObject.FindObjectsOfType<Ice>();
        foreach (var item in IceTran)
        {
            EventCenter.GetInstance().RomoveEventListener("IChangeB", item.IChangeB);
            Debug.Log("清除了监听");
        }

    }
}
