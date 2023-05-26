using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public static ButtonAudio Instance { get; private set; }
    [SerializeField] AudioPlayer buttonAudio = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayButtonAudio()
    {
        buttonAudio.PlayAudioByName("button");
    }
}
