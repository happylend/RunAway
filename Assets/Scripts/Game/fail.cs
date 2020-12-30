using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fail : MonoBehaviour
{
    // Start is called before the first frame update
    public  int key = 4;
   
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player"  || collision.collider.tag == "Box" )
        {
            EventCenter.GetInstance().EventTrigger("fail", key);
        }
        else if(collision.collider.tag == "Stone")
        {
            Destroy(collision.collider.gameObject);
        }
    }

    // Update is called once per frame

}
