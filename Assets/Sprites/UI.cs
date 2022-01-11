using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject PauseMenu;

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
    }

    public void ResumeGame()
    {
       // GameObject.Find("Canvas/Resume").SetActive(false);
        PauseMenu.SetActive(false);
    }
}