using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuAudio : MonoBehaviour
{
    [SerializeField] AudioPlayer mainMusic = null;
    [SerializeField] AudioPlayer buttonAudio = null;

    public static StartMenuAudio Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (mainMusic == null)
        {
            mainMusic = gameObject.GetComponent<AudioPlayer>();
        }
        mainMusic.onSoundFinished += () => PlayMusic();
    }

    private void Start()
    {
        PlayMusic();
    }

    private void PlayMusic()
    {
        mainMusic.PlayAudioByName("intro");
    }

    public void PlayButtonAudio()
    {
        buttonAudio.PlayAudioByName("button");
    }
}
