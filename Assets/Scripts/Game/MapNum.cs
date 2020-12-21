using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapNum : MonoBehaviour
{

    public static int Start_Num;//初始关卡

    static bool isHaveClone = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!isHaveClone)
        {
            isHaveClone = true;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    //跳转关卡
    public void StartGame()
    {
        //Map.Map_Num = Start_Num;
        SceneManager.LoadScene("SampleScene");
       // music.PlaySound("点击");

    }
}
