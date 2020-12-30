using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [Header("通关菜单界面，分为返回主菜单和下一关")]
    public static int key = 0;
    private GameObject Menu;
    public GameObject Map;
    // Start is called before the first frame update
    void Start()
    {
        Menu = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextLevel()
    {
        MapControl.Restart_Num++;
        EventCenter.GetInstance().EventTrigger("ChangeMap", key++);
        Menu.SetActive(false);
        
    }

    public void ReturnStart()
    {
        PoolMgr.GetInstance().Clear();
        Map.SendMessage("DestroyListener");
        SceneManager.LoadScene("StartScene");

    }
}
