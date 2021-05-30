using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWorldCube : MonoBehaviour
{
    // private GameObject LightMap, DarkMap;
    // public static bool Lmap = true;
    // Start is called before the first frame update
    [Header("===第一关为0===")]
    public int key ;

    public static bool ChangeMapActive = false;
    
    private void Awake()
    {

        
    }

    void Start()
    {
        // LightMap = Map.LightMap[1];
        //  DarkMap = Map.DarkMap[1];

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (InputMgr.isStart == true)
        {
            if (collision.gameObject.tag == "Player")
            {
                FallCheck.CanFall = false;
                ChangeMapActive = true;

                //检测世界转换
                EventCenter.GetInstance().EventTrigger("ChangeWorld", key);
            }
        }

    }
}
