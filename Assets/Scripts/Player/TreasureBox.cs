using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : AllBoxs
{
    protected GameObject box;
    public TreasureBox(GameObject Box)
    {
        box = Box;
    }
    public override bool CanMove(Vector3 Dir)
    {
        RaycastHit hit;
        if(Physics.Raycast(box.transform.position, Dir, out hit, 0.7f))
        {
            GameObject hits = hit.transform.gameObject;
            
        }
        return true;    
    }

    public override void Move(Vector3 Dir)
    {
        
    }

    // Start is called before the first frame update

}
