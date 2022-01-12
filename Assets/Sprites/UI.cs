using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    public void PauseGame()
    {
        // GameObject.Find("Canvas/Resume").SetActive(true);
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        // GameObject.Find("Canvas/Resume").SetActive(false);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetVolume(float value)
    {
        AudioMixer.SetFloat("MainVolume", value);
    }
}