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
        }
        else
        {
            Debug.LogWarning("WARNING: Singleton already created!");
        }
    }

    public void PlaySound(SoundType soundType)
    {
        GameObject audioSourceObject = Instantiate(audioSourceModel, audioSourceParent.transform);
        AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();

        List<AudioClip> audioClips = null;

        bool loop = false;

        switch (soundType)
        {
            case SoundType.COAL_VOICE:
                audioClips = coals;
                break;
            case SoundType.BIG_COAL_VOICE:
                audioClips = bigCoals;
                break;
            case SoundType.MATCHES_VOICE:
                audioClips = matches;
                break;
            case SoundType.GAS_VOICE:
                audioClips = gas;
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
            default:
                break;
        }

        AudioClip audioClip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.clip = audioClip;
        audioSource.loop = loop;

        audioSourceObject.GetComponent<AudioSourceManager>().Play();
    }
}
