using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private static SFXPlayer instance;

    [SerializeField] private AudioSource sfxPlayerSource;

    [Header("Sound Clips")]
    [SerializeField] private AudioClip popSound;
    [SerializeField] private AudioClip tipSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip errorSound;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;

    }

    public static SFXPlayer GetInstance()
    {
        return instance;
    }

    public void PlayOneShot(AudioClip clip)
    {
        sfxPlayerSource.PlayOneShot(clip);
    }

    public void PlaySound(string soundName)
    {
        switch(soundName)
        {
            case "pop":
                sfxPlayerSource.PlayOneShot(popSound);
                break;
            case "click":
                sfxPlayerSource.PlayOneShot(clickSound);
                break;
            case "tip":
                sfxPlayerSource.PlayOneShot(tipSound);
                break;
            case "error":
                sfxPlayerSource.PlayOneShot(errorSound);
                break;
            default:
                break;
        }
    }
}
