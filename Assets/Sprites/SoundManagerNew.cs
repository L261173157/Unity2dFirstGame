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
        Instance = this;
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