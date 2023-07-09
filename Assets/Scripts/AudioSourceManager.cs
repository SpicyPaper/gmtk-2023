using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    public AudioSource AudioSource { get; private set; }

    private bool playStarted = false;

    // Start is called before the first frame update
    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playStarted)
        {
            return;
        }
        if (AudioSource.isPlaying)
        {
            return;
        }

        Destroy(gameObject);
    }

    public void Play()
    {
        AudioSource.Play();

        playStarted = true;
    }

    public void Stop()
    {
        AudioSource.Stop();

        Destroy(gameObject);
    }
}
