using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class DeathZoneTrigger : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        AudioManager.Instance.PlaySFX("Death");
        AudioManager.Instance.musicSource.Stop();
        Destroy(other.gameObject);
    }   
}
