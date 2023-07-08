using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    private AudioSource audioSource;

    private bool playStarted = false;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playStarted)
        {
            return;
        }
        if (audioSource.isPlaying)
        {
            return;
        }

        Destroy(gameObject);
    }

    public void Play()
    {
        audioSource.Play();

        playStarted = true;
    }
}
