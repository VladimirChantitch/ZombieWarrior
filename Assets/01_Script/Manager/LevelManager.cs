using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] AudioPlayer audioPlayer;
        [SerializeField] List<Spawner> spawners = new List<Spawner>();
        [SerializeField] int waveTotal = 0;
        [SerializeField] int maxWaveZombie;

        private void Awake()
        {
            if (audioPlayer == null)
            {
                audioPlayer = gameObject.GetComponent<AudioPlayer>();
            }
            audioPlayer.onSoundFinished += () => PlayMusic();
            spawners.ForEach(s =>
            {
                s.onZombieKilled += () =>
                {
                    waveTotal += 1;
                    if (waveTotal >= maxWaveZombie)
                    {
                        StopWave();
                    }
                };
            });
        }

        private void Start()
        {
            StartWave();
        }

        private void StartWave()
        {
            audioPlayer.PlayAudioByName("startWave");
        }

        private void PlayMusic()
        {
            audioPlayer.PlayAudioByName("music");
            spawners.ForEach(s =>
            {
                s.canSpawn = true;
            });
        }

        private void StopWave()
        {
            spawners.ForEach(s =>
            {
                s.canSpawn = false;
            });       
        }
    }
}

