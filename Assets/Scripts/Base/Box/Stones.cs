using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stones : BoxFather
{


    // Start is called before the first frame update
    void Awake()
    {
        Init(BoxType.Stone,this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        NewMove(Player_Controller.RestartLayer);
        //滑冰
        if (canSkate()) SkateInit();
        SkateMove();
    }
    private void LateUpdate()
    {
        Fall(this.gameObject);
    }

}
