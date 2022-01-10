using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : BoxFather
{
    public bool CanChange;


    // Start is called before the first frame update


    private void Start()
    {
        Init(BoxType.IceBlock, gameObject);
        EventCenter.GetInstance().AddEventListener("BChangeI", BChangeI);
    }
    // Update is called once per frame
    private void Update()
    {
        NewMove(Player_Controller.RestartLayer);
        if (canSkate()) SkateInit();
        SkateMove();
    }


    private void LateUpdate()
    {
        Fall(gameObject);
    }
    /// <summary>
    /// 冰块换成冰面
    /// </summary>
    public void BChangeI(object key)
    {
        int Num = (int)key;
        Debug.Log("开始从冰块变成冰面咯！");        //在滑雪关调用
        if (Num == 7 || Num == 8)
        {
            IceF = GameObject.FindWithTag("Ice").transform;
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.6f, Player_Controller.IceLayer))
            {
                if (hit.collider.tag == "Ice")  return;
            }
            else
            {
                //下面是地面
                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitt, 0.6f))
                {
                    //底下生成冰块
                    GameObject obj = hitt.collider.gameObject;
                    Vector3 iceTran = obj.transform.position;
                    GameObject Ice = PoolMgr.GetInstance().GetObjAsyc("Prefab/New Materials/Ice", iceTran, Quaternion.identity);
                    Ice.name = "Ice";
                    Ice.transform.parent = IceF;
                    Ice.GetComponent<Ice>().CanChange = false;


                    EventCenter.GetInstance().RomoveEventListener("BChangeI", BChangeI);
                    //销毁地块 和 自己
                    obj.SetActive(false);
                    gameObject.SetActive(false);
                    
                }
            }
        }
        //检测到下面是冰面  
    }
}
