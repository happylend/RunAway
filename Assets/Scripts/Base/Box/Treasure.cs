using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure:BoxFather
{
    public static int RestartLayer = ~(1 << 29 | 1 << 28);//忽略重启区域
    public static bool Iswin = false;
    public static bool CanChange = false;

    //public Transform MovePoint;
    private void Awake()
    {
        Init(BoxType.Treasure, gameObject);
        
    }

    private void Update()
    {
        //this.Move(this.gameObject, Player_Controller.Ignorelayer);

        NewMove(Player_Controller.RestartLayer);


        //滑冰
        if (canSkate()) SkateInit();
        SkateMove();     
        
        IsWin();

    }

    private void LateUpdate()
    {
        Fall(gameObject);
        
    }
    public void IsWin()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit WinHit, 1f))
        {
            if (WinHit.collider.tag == "Flag" &&( WinHit.collider.transform.position.x- this.transform.position.x) < 0.2f)
            {
                Iswin = true;
                WinBox();
            } 
        }
    }

    /// <summary>
    /// 胜利箱子转向
    /// </summary>
    public void WinBox()
    {
        //Debug.Log("开始转向");
        switch (Player_Move.dir)
        {
            case Dir.idle:
                break;
            case Dir.forward:
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case Dir.back:
                this.transform.eulerAngles = new Vector3(0, 180f, 0);
                break;
            case Dir.left:
                this.transform.eulerAngles = new Vector3(0, 90f, 0);
                break;
            case Dir.right:
                this.transform.eulerAngles = new Vector3(0,  -90f, 0);
                break;
            default:
                break;
        }
    }


}
