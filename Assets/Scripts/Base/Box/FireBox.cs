using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBox:BoxFather
{

    private void Awake()
    {

        Init(BoxType.FireBox, gameObject);

    }
    private void Update()
    {
        NewMove(Player_Controller.IgnoreGrass);
        DecGrass();
    }

    private void LateUpdate()
    {
        Fall(gameObject);
    }
    private void DecGrass()
    {
        if(Physics.Raycast(transform.position,DeDir,out RaycastHit hit,0.5f))
        {
            if(hit.collider.tag == "Grass")
            {
                GameObject.Destroy(hit.collider.gameObject);
                GameObject.Destroy(this.gameObject);
            }
        }
    }


}
