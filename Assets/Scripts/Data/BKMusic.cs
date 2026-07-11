using System;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;
    private AudioSource audioSource;
    private BKMusic()
    {
        instance = this;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SetMusicValue(GameDataMgr.Instance.musicData.musicValue);
        SetMusicIsMute(GameDataMgr.Instance.musicData.musicIsOpen);
    }

    public void SetMusicValue(float value)
    {
        audioSource.volume = value;
    }

    public void SetMusicIsMute(bool value)
    {
        audioSource.mute = !value;
    }
}
