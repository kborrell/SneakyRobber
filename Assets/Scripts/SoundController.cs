using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundController : MonoBehaviour {

    public enum AudioKey
    {
        PickObject,
        ThrowObject,
        Spotted,
        Sospicious,
        CollectScore
    }

    [Serializable]
    public struct Audio
    {
        public AudioKey key;
        public AudioClip clip;
    }

    public void PlayBackgroundMusic()
    {
        m_musicAudioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        m_musicAudioSource.Stop();
    }

    public void PlayAudio(AudioKey key)
    {
        if (m_audioDictionary.ContainsKey(key))
        {
            m_sfxAudioSource.PlayOneShot(m_audioDictionary[key]);
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        foreach (Audio audio in m_audios)
        {
            m_audioDictionary[audio.key] = audio.clip;
        }
    }

    private void Start()
    {
        m_musicAudioSource.clip = m_backgroundMusic;
        PlayBackgroundMusic();
    }

    public List<Audio> m_audios;
    public AudioClip m_backgroundMusic;

    public AudioSource m_musicAudioSource;
    public AudioSource m_sfxAudioSource;

    private Dictionary<AudioKey, AudioClip> m_audioDictionary = new Dictionary<AudioKey, AudioClip>();

    public static SoundController Instance { get; private set; }
}
