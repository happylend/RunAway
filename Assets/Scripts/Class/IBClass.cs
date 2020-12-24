using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBClass : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject father;
    private static int op = 0;
    private void Awake()
    {
        //Set the iceConvertblock listener
        EventCenter.GetInstance().AddEventListener("ChangeWorld", ChangeChild);
        father = this.gameObject;
        
    }
    /// <summary>
    /// 冰面冰块转换函数
    /// </summary>
    /// <param name="key"></param>
    public static void ChangeChild(object key)
    {
        int num = (int)key + 1;
        //find all the child
        foreach(Transform child in father.GetComponentInChildren<Transform>())
        {
            //the amount of child
            if(child.gameObject.activeInHierarchy)
            {
                op++;
            }
        }
        foreach (Transform child in father.GetComponentInChildren<Transform>())
        {
            //ice and block all exist
            if (op % 2 == 0 && op != 0)
            {
                break;
            }
            //switch
            if (child.gameObject.activeInHierarchy)
            {
                child.gameObject.SetActive(false);
            }
            else
                child.gameObject.SetActive(true);

        }


    }
}
