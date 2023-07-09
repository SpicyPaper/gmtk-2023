using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public enum SoundType
    {
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

    [SerializeField] private List<AudioClip> coals = null;
    [SerializeField] private List<AudioClip> bigCoals = null;
    [SerializeField] private List<AudioClip> matches = null;
    [SerializeField] private List<AudioClip> gas = null;
    [SerializeField] private List<AudioClip> mainMusic = null;
    [SerializeField] private List<AudioClip> fire = null;
    [SerializeField] private List<AudioClip> clicks = null;

    private Dictionary<SoundType, ArrayList> registeredSounds;

    private GameObject audioSourceParent;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSourceParent = new GameObject("Audio Source Parent");
            audioSourceParent.transform.SetParent(transform);
            audioSourceParent.transform.localPosition = Vector3.zero;

            registeredSounds = new Dictionary<SoundType, ArrayList>
            {
                { SoundType.COAL_VOICE, new ArrayList() { 0, null } },
                { SoundType.BIG_COAL_VOICE, new ArrayList() { 0, null } },
                { SoundType.MATCHES_VOICE, new ArrayList() { 0, null } },
                { SoundType.GAS_VOICE, new ArrayList() { 0, null } },
            };
        }
        else
        {
            Debug.LogWarning("WARNING: Singleton already created!");
        }
    }

    public void RegisterSound(SoundType soundType)
    {
        registeredSounds[soundType][0] = (int)registeredSounds[soundType][0] + 1;
    }

    public void UnregisterSound(SoundType soundType)
    {
        registeredSounds[soundType][0] = (int)registeredSounds[soundType][0] - 1;

        if ((int)registeredSounds[soundType][0] <= 0)
        {
            if ((AudioSourceManager)registeredSounds[soundType][1])
            {
                ((AudioSourceManager)registeredSounds[soundType][1]).Stop();
            }
        }
    }

    public void PlayRegisteredSounds()
    {
        foreach (var item in registeredSounds)
        {
            if ((int)item.Value[0] > 0)
            {
                PlaySound(item.Key);
            }
        }
    }

    public void PlaySound(SoundType soundType)
    {
        GameObject audioSourceObject = Instantiate(audioSourceModel, audioSourceParent.transform);
        AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();

        List<AudioClip> audioClips = null;
        AudioSourceManager audioSourceManager = null;

        bool loop = false;

        switch (soundType)
        {
            case SoundType.COAL_VOICE:
                audioClips = coals;
                audioSourceManager = audioSourceObject.GetComponent<AudioSourceManager>();
                break;
            case SoundType.BIG_COAL_VOICE:
                audioClips = bigCoals;
                audioSourceManager = audioSourceObject.GetComponent<AudioSourceManager>();
                break;
            case SoundType.MATCHES_VOICE:
                audioClips = matches;
                audioSourceManager = audioSourceObject.GetComponent<AudioSourceManager>();
                break;
            case SoundType.GAS_VOICE:
                audioClips = gas;
                audioSourceManager = audioSourceObject.GetComponent<AudioSourceManager>();
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

        if (audioSourceManager)
        {
            registeredSounds[soundType][1] = audioSourceManager;
        }

        AudioClip audioClip = audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
        audioSource.clip = audioClip;
        audioSource.loop = loop;

        audioSourceObject.GetComponent<AudioSourceManager>().Play();
    }
}
