using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    [Header("菜单")]
    public GameObject Menu;
    public GameObject MainMenu;
    [Header("菜单灯光")]
    public GameObject Menu_Light;
    public GameObject MainMenu_Light;
    [Header("摄像机")]
    public GameObject Main_Camera;
    [Header("天空盒")]
    public Material Menu_SkyBox;
    public Material MainMenu_SkyBox;


    // Start is called before the first frame update
    void Start()
    {
        Menu.SetActive(false);
        MainMenu.SetActive(true);

        Menu_Light.SetActive(false);

        MainMenu_Light.SetActive(true);

        RenderSettings.skybox = MainMenu_SkyBox;
        //播放主菜单音乐
        MusicMgr.GetInstance().PlayBkMusic("MainMenuBGM");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void QuitGame()
    {
        music.PlaySound("点击");
        Application.Quit();
    }

    public void GoToChooseMenu()
    {
        music.PlaySound("点击");
        Menu.SetActive(true);
        MainMenu.SetActive(false);

        Menu_Light.SetActive(true);
        MainMenu_Light.SetActive(false);

        RenderSettings.skybox = Menu_SkyBox;

    }

    public void ReturnToMainMenu()
    {
        music.PlaySound("点击");
        Menu.SetActive(false);
        MainMenu.SetActive(true);

        Menu_Light.SetActive(false);
        MainMenu_Light.SetActive(true);

        RenderSettings.skybox = MainMenu_SkyBox;
        //判断是否是从主菜单转来的

    }
}
