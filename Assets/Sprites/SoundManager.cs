using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioSource AudioSource;
    [SerializeField]
    private AudioClip JumpAudio;

    private void Awake()
    {
        Instance = this;
    }

    public void Jump()
    {
        AudioSource.clip=JumpAudio;
        AudioSource.Play();

    }
}
