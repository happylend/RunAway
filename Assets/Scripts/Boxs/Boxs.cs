using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxs : MonoBehaviour
{
    public float checkWall = 1f;
    public static int RestartLayer = ~(1 << 29 | 1 << 28);
    private Vector3 StartPos;

    public enum CanMove
    {
        idle,//初始,
        Down,
        Save
    }

    public static CanMove dMove = CanMove.Save;

    void Start()
    {
        StartPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y != StartPos.y && dMove == CanMove.Save)
        {
            Vector3 pos = transform.position;
            pos.y = StartPos.y;
            transform.position = pos;

        }

        dMove = Fall(Vector3.down);
        CheckFall();
    }

    CanMove Fall(Vector3 pos)
    {
        if (!Physics.Raycast(transform.position, new Vector3(0, -1f, 0), checkWall, RestartLayer))
        {
            return CanMove.Down;
        }
        else
        {
            return CanMove.Save;
        }

    }

    void CheckFall()
    {
        if (dMove == CanMove.Down)
            GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;
        else if (dMove == CanMove.Save)
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
