using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoxs : AllBoxs
{
    float speed = 0.7f;
    GameObject box;
    public MyBoxs(GameObject Box)
    {
        box = Box;
    }

    public override bool CanMove(Vector3 Dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(box.transform.position, Dir, out hit, 0.7f))
        {
            if(hit.transform.gameObject.tag == "Grass")
            {
                GameObject.Destroy(hit.transform.gameObject);
                GameObject.Destroy(box);
                return true;
            }
            return false;
        }
        return true;
    }
    public override void Move(Vector3 Dir)
    {
        if (CanMove(Dir))
        {
            Vector3.Lerp(box.transform.position, box.transform.position + Dir, speed);
        }
    }
}
