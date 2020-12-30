using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakList : MonoBehaviour
{
    public static GameObject BreakIcesFather, BlocksFather;
    public static List<GameObject> ices, DarkCubes_1, LightCubes_1;
    public List<Vector3> BreakIcePos, BlockPos;
    // Start is called before the first frame update
    void Start()
    {
        //监听转换世界
        EventCenter.GetInstance().AddEventListener("ChangeWorld", ChangeBreakice);
    }
    /// <summary>
    /// 碎冰变冰块
    /// </summary>
    // Update is called once per frame
    public static void iceCheck()
    {
        ices = new List<GameObject>();
        LightCubes_1 = new List<GameObject>();
        DarkCubes_1 = new List<GameObject>();
        //冰块
        BlocksFather = GameObject.FindWithTag("IceBlocks");
        //碎冰道
        BreakIcesFather = GameObject.FindWithTag("BreakIce");
        //碎冰道变冰块
        foreach (Transform child in BreakIcesFather.GetComponent<Transform>())
        { 
            if(child.gameObject.activeInHierarchy)
            {
                
            }
        }
    }
    public  static void blockCheck()
    {

    }
    public static void ChangeBreakice(object key)
    {
        int num = (int)key + 1;
        if(num==8||num==9||num==10)
        {
         
        }
                

      
    }
}
