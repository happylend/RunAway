using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [Header("---重置关卡变量（第一关为0）---")]
    public int key = 2;
    // Start is called before the first frame update
    //回溯关卡
    public static Vector3 PeoplePos;
    public static bool CanFall = true;
    public static GameObject box;
    public static Vector3 PulledBoxPos;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player"||other.gameObject.tag=="Box")
        {

            EventCenter.GetInstance().EventTrigger("RestartGame", key);
        }
        else
        {
         
            Destroy(other.gameObject);

            
        }
    }
    void OpenInput()
    {
        EventCenter.GetInstance().AddEventListener("Keydown", PlayerControl.CheckInputDown);
    }
}
