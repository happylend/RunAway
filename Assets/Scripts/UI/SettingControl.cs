using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingControl : MonoBehaviour
{
    public GameObject Setting;
    public GameObject SettingObj;
    public GameObject Map;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Game_Continue()
    {
        //声效
        music.PlaySound("点击");
        SettingObj.SetActive(true);
        Setting.SetActive(false);
    }

    public void Game_Restart()
    {
        //声效
        music.PlaySound("点击");
        EventCenter.GetInstance().EventTrigger("RestartGame", MapControl.Restart_Num);
        SettingObj.SetActive(true);
        Setting.SetActive(false);
    }

    public void Game_Menu()
    {
        //声效
        music.PlaySound("点击");
        PoolMgr.GetInstance().Clear();

        //变换BGM
        MusicMgr.GetInstance().PauseBKMusic();
        MusicMgr.GetInstance().PlayBkMusic("MainMenuBGM");
        Map.SendMessage("DestroyListener");
        SceneManager.LoadScene("StartScene");

    }
}
