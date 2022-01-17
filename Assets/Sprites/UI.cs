using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject PauseMenu;

    public AudioMixer AudioMixer;

    //暂停游戏
    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    //恢复游戏
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    //控制音量
    public void SetVolume(float value)
    {
        AudioMixer.SetFloat("MainVolume", value);
    }
    //返回主菜单
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}