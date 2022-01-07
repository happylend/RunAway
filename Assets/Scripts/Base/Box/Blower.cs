﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : BoxFather
{
    public Vector3 BlowerDir;



    // Start is called before the first frame update
    private void Awake()
    {

        Init(BoxType.Blower, this.gameObject);
    }
    private void Update()
    {
        this.NewMove(Player_Controller.RestartLayer);
        if (!Map.LW)
        {
            this.transform.eulerAngles = new Vector3(0, 180f, 0);
            if (Physics.Raycast(this.transform.position, Vector3.left, out RaycastHit hit, 1.2f))
            {
                if (hit.transform.tag == "Player")
                {
                    Player_Controller.BlowDir = Vector3.left;
                    Player_Controller.CanBlow = true;
                }
            }
        }
        else
        {
            this.transform.eulerAngles = BlowerDir;
            if (Physics.Raycast(this.transform.position, Vector3.right, out RaycastHit hit, 1.2f))
            {
                if (hit.transform.tag == "Player")
                {
                    Player_Controller.BlowDir = Vector3.right;
                    Player_Controller.CanBlow = true;
                }
            }
        }

    }
    private void FixedUpdate()
    {

    }
    private void LateUpdate()
    {
        Fall(this.gameObject);
    }
    /*
    public void DirDective(Vector3 vector)
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, vector, out hit, 1.2f))
        {
            if (hit.transform.CompareTag("Player"))
            {                       
                switch (vector)
                {
                    case Vector3 v when v.Equals(Vector3.forward):
                        Player_Controller.BlowDir = Vector3.forward;
                        Player_Controller.CanBlow = true;

                        break;
                    case Vector3 v when v.Equals(Vector3.back):
                        Player_Controller.BlowDir = Vector3.back;
                        Player_Controller.CanBlow = true;

                        break;
                    case Vector3 v when v.Equals(Vector3.left):
                        Player_Controller.BlowDir = Vector3.left;
                        Player_Controller.CanBlow = true;

                        break;
                    case Vector3 v when v.Equals(Vector3.right):
                        Player_Controller.BlowDir = Vector3.right;
                        Player_Controller.CanBlow = true;
    
                        break;
                    default:
                        //Debug.Log("Dir is " + Blowdir);
                        break;
                }

            }
        }
    }
    */




}
