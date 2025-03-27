using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
//ֵ��ע���һ���ǣ�setactive�����ò�ֻ���ڿɼ��벻�ɼ�����Ϊfalse��gameobject�Ľű�Ҳһ����ʧЧ
{
    [SerializeField] private GameObject EscMenu;
    [SerializeField] private GameObject SettingMenu;
    [SerializeField] private GameObject ExitMenu;
    private bool escIsActived = false;
    private bool settingIsActived = false;
    private bool exitIsActived = false;
    void Start()
    {
        EscMenu.SetActive(false);
        SettingMenu.SetActive(false);
        ExitMenu.SetActive(false);
    }

    void Update()
    {
        EscControll();
        CheckEscInSetting();
        CheckEscInExit();
    }


    private void EscControll()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !escIsActived && !settingIsActived && !exitIsActived)
        {
            EscMenu.SetActive(true);
            escIsActived = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && escIsActived && !settingIsActived && !exitIsActived)
        {
            EscMenu.SetActive(false);
            escIsActived = false;
            Time.timeScale = 1;
        }
    }
    private void ExitEscToOtherWindow()
    {
        EscMenu.SetActive(false);
        escIsActived = false;
        if (Time.timeScale != 0)
            Time.timeScale = 0;
    }
    private void EnterEscFromOtherWindow()
    {
        EscMenu.SetActive(true);
        escIsActived = true;
        if (Time.timeScale != 0)
            Time.timeScale = 0;
    }
    public void ReturnButtonInEsc()
    {
        EscMenu.SetActive(false);
        escIsActived = false;
        Time.timeScale = 1;
    }
    public void SettingsBottonInEsc()
    {
        settingIsActived = true;
        ExitEscToOtherWindow();
        SettingMenu.SetActive(true);
    }
    public void ReturnBottonInSetting()
    {
        settingIsActived = false;
        EnterEscFromOtherWindow();
        SettingMenu.SetActive(false);
    }
    public void ExitBottonInEsc()
    {
        exitIsActived = true;
        ExitEscToOtherWindow();
        ExitMenu.SetActive(true);
    }

    public void ReturnButtonInExit()
    {
        exitIsActived = false;
        EnterEscFromOtherWindow();
        ExitMenu.SetActive(false);
    }

    public void ReturnToTitleButtonInExit() 
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGameInExit() 
    {
        Application.Quit();//ִ���Ƴ���Ϸָ�unity�Դ�   
    }
    private void CheckEscInSetting()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !escIsActived && settingIsActived)
        {
            SettingMenu.SetActive(false);
            ReturnBottonInSetting();
        }
    }
    private void CheckEscInExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !escIsActived && exitIsActived)
        {
            ExitMenu.SetActive(false);
            ReturnButtonInExit();
        }
    }
}
