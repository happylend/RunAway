using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowTrigger : MonoBehaviour
{
    public List<Vector3> BaseDir;
    public List<Vector3> DirList;
    // Start is called before the first frame update
    public Blower blower;
    private void Awake()
    {
        //blower = new Blower();
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Map.LW)
            {
                Player_Controller.BlowDir = Vector3.right;
                Player_Controller.CanBlow = true;
            }
            else
            {
                Player_Controller.BlowDir = Vector3.left;
                Player_Controller.CanBlow = true;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        Player_Controller.BlowDir = Vector3.zero;
        Player_Controller.CanBlow = false;
    }
}
