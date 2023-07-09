using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using static SoundHandler;

public class SoundHandler : MonoBehaviour
{
    public enum SoundType
    {
        COAL_ONE,
        COAL_VOICE,
        BIG_COAL_VOICE,
        MATCHES_VOICE,
        GAS_VOICE,
        MAIN_MUSIC,
        FIRE,
        CLICK
    }

    public static SoundHandler Instance { get; private set; }


    [SerializeField] private GameObject audioSourceModel = null;

    [SerializeField] private List<AudioClip> coalOnes = null;
    [SerializeField] private List<AudioClip> coals = null;
    [SerializeField] private List<AudioClip> bigCoals = null;
    [SerializeField] private List<AudioClip> matches = null;
    [SerializeField] private List<AudioClip> gas = null;
    [SerializeField] private List<AudioClip> mainMusic = null;
    [SerializeField] private List<AudioClip> fire = null;
    [SerializeField] private List<AudioClip> clicks = null;

    private GameObject audioSourceParent;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSourceParent = new GameObject("Audio Source Parent");
            audioSourceParent.transform.SetParent(transform);
            audioSourceParent.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogWarning("WARNING: Singleton already created!");
        }
    }

    public AudioSource PlaySound(SoundType soundType)
    {
        GameObject audioSourceObject = Instantiate(audioSourceModel, audioSourceParent.transform);
        AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();

        List<AudioClip> audioClips = null;
        AudioSourceManager audioSourceManager = null;

        bool loop = false;

        switch (soundType)
        {
            case SoundType.COAL_ONE:
                audioClips = coalOnes;
                break;
            case SoundType.COAL_VOICE:
                audioClips = coals;

                loop = true;
                break;
            case SoundType.BIG_COAL_VOICE:
                audioClips = bigCoals;

                loop = true;
                break;
            case SoundType.MATCHES_VOICE:
                audioClips = matches;

                loop = true;
                break;
            case SoundType.GAS_VOICE:
                audioClips = gas;

                loop = true;
                break;
            case SoundType.MAIN_MUSIC:
                audioClips = mainMusic;

                loop = true;
                break;
            case SoundType.FIRE:
                audioClips = fire;

                loop = true;
                break;
            case SoundType.CLICK:
                audioClips = clicks;
                break;
        }

        AudioClip audioClip = audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
        audioSource.clip = audioClip;
        audioSource.loop = loop;
        SetVolume(soundType, audioSource, MaxVolume(soundType));

        audioSourceObject.GetComponent<AudioSourceManager>().Play();

        return audioSource;
    }

    public float MaxVolume(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.COAL_ONE:
                return 0.8f;
            case SoundType.COAL_VOICE:
                return 0.8f;
            case SoundType.BIG_COAL_VOICE:
                return 0.6f;
            case SoundType.MATCHES_VOICE:
                return 0.8f;
            case SoundType.GAS_VOICE:
                return 0.5f;
            case SoundType.MAIN_MUSIC:
                return 0.9f;
            case SoundType.FIRE:
                return 0.6f;
            case SoundType.CLICK:
                return 0.7f;
        }

        return 0;
    }

    public void SetVolume(SoundType soundType, AudioSource audioSource, float volume)
    {
        if (volume > MaxVolume(soundType))
        {
            volume = MaxVolume(soundType);
        }

        audioSource.volume = volume;
    }

    public void ClearAllSounds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }
    }
}
