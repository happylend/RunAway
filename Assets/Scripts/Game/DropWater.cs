using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWater : MonoBehaviour
{
    public int key = 1;
    //bool first = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Grass"|| other.gameObject.tag == "Box" ||
            other.gameObject.tag == "Fire"|| other.gameObject.tag == "Blow"||
            other.gameObject.tag == "Stone"|| other.gameObject.tag == "Player")
        {
            //AnimControl.Drop();
            EventCenter.GetInstance().EventTrigger("DropWater", key);
        }

        

    }
}
