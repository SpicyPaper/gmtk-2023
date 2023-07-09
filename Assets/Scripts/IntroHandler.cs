using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroHandler : MonoBehaviour
{
    [SerializeField] private FireController fireController;
    [SerializeField] private Camera cam;
    [SerializeField] private TMP_Text starveText;
    [SerializeField] private TMP_Text startText;

    [SerializeField] private List<float> elapsedTimes;

    private int currentElapsedStep;

    private float elapsed;

    private float camTranslateForward = 70;
    private float initFireVolume = 0.2f;
    private float lookat = 1.3f;

    private bool onlyOnce;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        fireController.SetBurnPower(0);
        starveText.color = new Color(1, 1, 1, 0);
        startText.gameObject.SetActive(false);
        cam.transform.LookAt(Vector3.up * lookat);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentElapsedStep >= elapsedTimes.Count)
        {
            return;
        }

        elapsed += Time.deltaTime;
        float currentElapsedTime = elapsedTimes[currentElapsedStep];
        float perc = Time.deltaTime / currentElapsedTime;
        float totalPerc = elapsed / currentElapsedTime;

        switch (currentElapsedStep * 10)
        {
            case 0:
                // black screen
                onlyOnce = true;
                break;
            case 10:
                if (onlyOnce)
                {
                    SoundHandler.Instance.PlaySound(SoundHandler.SoundType.CLICK);
                    audioSource = SoundHandler.Instance.PlaySound(SoundHandler.SoundType.FIRE);
                    fireController.SetBurnPower(30);
                    fireController.SetBurnPowerDecay(0);
                    SoundHandler.Instance.SetVolume(SoundHandler.SoundType.FIRE, audioSource, initFireVolume);
                    onlyOnce = false;
                }
                break;
            case 20:
                fireController.AddBurnPower(50 * Mathf.Sin(Mathf.Lerp(0, Mathf.PI / 2, perc)));
                break;
            case 30:
                fireController.SetBurnPowerDecay(5);
                cam.transform.Translate(camTranslateForward * perc * Vector3.forward, Space.Self);

                SoundHandler.Instance.SetVolume(SoundHandler.SoundType.FIRE, audioSource, Mathf.Sin(Mathf.Lerp(0, Mathf.PI / 2, totalPerc)));
                break;
            case 40:
                starveText.color = new Color(1, 1, 1, Mathf.Sin(Mathf.Lerp(0, Mathf.PI / 2, totalPerc)));
                startText.gameObject.SetActive(true);
                fireController.SetMinBurnPower(35);
                break;
            case 50:
                cam.transform.Translate(45 * perc * Vector3.up, Space.World);
                cam.transform.LookAt(Vector3.up * lookat);
                break;
            default:
                break;
        }

        if (elapsed > currentElapsedTime)
        {
            elapsed -= currentElapsedTime;

            currentElapsedStep++;
        }
    }
}
