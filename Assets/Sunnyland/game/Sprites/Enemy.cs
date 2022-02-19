using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator m_Animator;
    protected AudioSource m_DeathAudio;

    protected virtual void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_DeathAudio= GetComponent<AudioSource>();
    }

    private void Death()
    {
        GetComponent<Collider2D>().enabled=false;
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        m_Animator.SetTrigger("death");
        m_DeathAudio.Play();
    }
}