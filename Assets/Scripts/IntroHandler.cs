using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroHandler : MonoBehaviour
{
    [SerializeField] private FireController fireController;
    [SerializeField] private Camera cam;
    [SerializeField] private TMP_Text starveText;

    [SerializeField] private List<float> elapsedTimes;

    private int currentElapsedStep;

    private float elapsed;

    private float camTranslate = 70;
    private float initFireVolume = 0.2f;

    private bool onlyOnce;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        fireController.addBurnPower(-1000);
        fireController.addBurnPower(0);
        starveText.color = new Color(1, 1, 1, 0);
        cam.transform.LookAt(Vector3.up * 1);
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
                    fireController.addBurnPower(5);
                    onlyOnce = false;
                }

                //fireController.burnPower = 8;

                fireController.addBurnPower(40 * Mathf.Lerp(0, 1, perc));
                audioSource.volume = initFireVolume;
                break;
            case 20:
                cam.transform.Translate(camTranslate * perc * Vector3.forward, Space.Self);

                audioSource.volume = Mathf.Lerp(initFireVolume, 1, totalPerc);
                break;
            case 30:
                starveText.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, totalPerc));
                starveText.gameObject.SetActive(true);
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
