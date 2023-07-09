using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float camTranslateUp = 47.6f;
    private float camTranslateBack = 10.4f;

    private float initFireVolume = 0.2f;
    private float endFireVolume = 0.4f;
    private float lookat = 1.3f;
    private float lookatEnd = -20f;

    private int simpleCoalStep = 0;

    private bool pause = false;

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

        if (pause)
        {
            if (Input.anyKey)
            {
                pause = false;
            }
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
                    fireController.SetBurnPower(40);
                    fireController.SetBurnPowerDecay(0);
                    SoundHandler.Instance.SetVolume(SoundHandler.SoundType.FIRE, audioSource, initFireVolume);
                    onlyOnce = false;
                }
                break;
            case 20:
                fireController.AddBurnPower(40 * Mathf.Sin(Mathf.Lerp(0, Mathf.PI / 2, perc)));
                break;
            case 30:
                fireController.SetBurnPowerDecay(5);
                fireController.SetMinBurnPower(65);
                cam.transform.Translate(camTranslateForward * perc * Vector3.forward, Space.Self);

                SoundHandler.Instance.SetVolume(SoundHandler.SoundType.FIRE, audioSource, Mathf.Sin(Mathf.Lerp(initFireVolume, Mathf.PI / 2, totalPerc)));
                break;
            case 40:
                starveText.color = new Color(1, 1, 1, (Mathf.Lerp(0, 1, totalPerc)));
                break;
            case 50:
                startText.gameObject.SetActive(true);
                break;
            case 60:
                pause = true;
                break;
            case 70:
                starveText.color = new Color(1, 1, 1, Mathf.Sin(Mathf.Lerp(Mathf.PI / 2, 0, totalPerc)));
                startText.gameObject.SetActive(false);
                break;
            case 80:
                cam.transform.Translate(camTranslateUp * perc * Vector3.up, Space.World);
                cam.transform.Translate(camTranslateBack * perc * Vector3.back, Space.World);
                cam.transform.LookAt(Vector3.up * Mathf.Lerp(lookat, lookatEnd, totalPerc));
                SoundHandler.Instance.SetVolume(SoundHandler.SoundType.FIRE, audioSource, Mathf.Sin(Mathf.Lerp(Mathf.PI / 2, endFireVolume, totalPerc)));

                if ((totalPerc > 0.5f && simpleCoalStep == 0) ||
                    (totalPerc > 0.8f && simpleCoalStep == 1))
                {
                    AudioSource audio = SoundHandler.Instance.PlaySound(SoundHandler.SoundType.COAL_ONE);
                    SoundHandler.Instance.SetVolume(SoundHandler.SoundType.COAL_ONE, audio, 0.2f);
                    simpleCoalStep++;
                }
                break;
            case 90:
                fireController.SetMinBurnPower(30);
                if ((totalPerc > 0.3f && simpleCoalStep == 2) ||
                    (totalPerc > 0.52f && simpleCoalStep == 3) ||
                    (totalPerc > 0.7f && simpleCoalStep == 4))
                {
                    AudioSource audio = SoundHandler.Instance.PlaySound(SoundHandler.SoundType.COAL_ONE);
                    SoundHandler.Instance.SetVolume(SoundHandler.SoundType.COAL_ONE, audio, 0.4f);
                    simpleCoalStep++;
                }
                break;
            case 100:
                SoundHandler.Instance.ClearAllSounds();
                SceneManager.LoadScene("GameScene");
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
