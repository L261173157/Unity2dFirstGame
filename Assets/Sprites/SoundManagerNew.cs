using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerNew : MonoBehaviour
{
    public static SoundManagerNew Instance { get; private set; }

    [SerializeField]
    private AudioSource PlayerAudio;

    [SerializeField]
    private AudioSource BGM;

    [SerializeField]
    private AudioClip JumpAudio;

    [SerializeField]
    private AudioClip HurtAudio;

    private void Awake()
    {
        //多场景调用使用声音
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }

    public void Jump()
    {
        PlayerAudio.clip = JumpAudio;
        PlayerAudio.Play();
    }

    public void Hurt()
    {
        PlayerAudio.clip = HurtAudio;
        PlayerAudio.Play();
    }

    public void StopAllAudio()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }

    public void StartAllAudio()
    {
        BGM.Play();
    }
}