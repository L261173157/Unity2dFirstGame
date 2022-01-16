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
    //��ͣ��Ϸ
    public void PauseGame()
    {
        // GameObject.Find("Canvas/Resume").SetActive(true);
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    //������Ϸ
    public void ResumeGame()
    {
        // GameObject.Find("Canvas/Resume").SetActive(false);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    //控制音量
    public void SetVolume(float value)
    {
        AudioMixer.SetFloat("MainVolume", value);
    }
    //�������˵�
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}