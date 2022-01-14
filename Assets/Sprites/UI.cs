using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject PauseMenu;

    public AudioMixer AudioMixer;
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
    //暂停游戏
    public void PauseGame()
    {
        // GameObject.Find("Canvas/Resume").SetActive(true);
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    //返回游戏
    public void ResumeGame()
    {
        // GameObject.Find("Canvas/Resume").SetActive(false);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //设置音量
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