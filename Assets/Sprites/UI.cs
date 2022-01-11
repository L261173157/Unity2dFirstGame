using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void ResumeEnable()
    {
        GameObject.Find("Canvas/Resume").SetActive(true);
    }

    public void ResumeDisable()
    {
        GameObject.Find("Canvas/Resume").SetActive(false);
    }
}