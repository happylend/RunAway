using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public Vector3 TargetPos = Vector3.zero;
    public bool CanSkate = false;
    public bool CanChange;
    private Transform LightCubeF,IceBlockF, DarkCubeF;
    private void Awake()
    {       
        EventCenter.GetInstance().AddEventListener("IChangeB", IChangeB);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Player_Controller.CanSkate = true;
        }
        if(collision.collider.tag == "Floor")
        {
            Transform colliderTran = collision.collider.transform;
            if (colliderTran.position == transform.position)
            {
                colliderTran.gameObject.SetActive(false);
      
            }

            
        } 
    }


    /// <summary>
    /// 变成冰块
    /// </summary>
    public void IChangeB(object key)
    {
        int Num = (int)key;
        if(Num == 7 || Num == 8)
        {
            if (this.CanChange)
            {
                if (Physics.Raycast(this.transform.position, Vector3.up, out RaycastHit blockHit, 0.6f)) return;

                //上面没有东西
                else
                {
                    Debug.Log(this.name);
                    //如果进入暗世界
                    if (!Map.LW)
                    {
                        DarkCubeF = GameObject.FindWithTag("DarkCubes").transform;
                        //在下方放一个DarkCube
                        GameObject Darkcube = PoolMgr.GetInstance().GetObjAsyc("Prefab/Floor/DarkCube_Ice", this.transform.position, Quaternion.identity);
                        //Darkcube.transform.position = Tran();
                        Darkcube.transform.parent = DarkCubeF;

                    }
                    else
                    {
                        LightCubeF = GameObject.FindWithTag("LightCubes").transform;
                        //在下方放一个LightCube
                        GameObject Lightcube = PoolMgr.GetInstance().GetObjAsyc("Prefab/Floor/Cube_Ice", this.transform.position, Quaternion.identity);
                        Lightcube.transform.position = this.transform.position;
                        Lightcube.transform.parent = LightCubeF;
                    }
                    var BlockY = this.transform.position.y + 1f;
                    //上方放一个冰块
                    GameObject IceBlock = PoolMgr.GetInstance().GetObjAsyc("Prefab/New Materials/IceBlock", new Vector3(this.transform.position.x, BlockY, this.transform.position.z), Quaternion.identity);
                    //
                    EventCenter.GetInstance().RomoveEventListener("IChangeB", IChangeB);
                    IceBlockF = GameObject.FindWithTag("IceBlock").transform;
                    IceBlock.transform.parent = IceBlockF;
                    gameObject.SetActive(false);
                }
            }
        }


        //上面如果有东西
    }


}