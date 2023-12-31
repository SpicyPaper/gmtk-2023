using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FireController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float initialBurnPower = 100f;

    [SerializeField]
    private float burnPowerDecay = 1f;

    [SerializeField] private Transform redFire;

    [SerializeField] private Transform oragenFire;

    [SerializeField] private Transform yellowFire;

    [SerializeField] private float redFlameMaxValue = 8f;
    [SerializeField] private float orangeFlameMaxValue = 5f;
    [SerializeField] private float yellowFlameMaxValue = 2f;

    [SerializeField] private float redFlameMinValue = 4f;
    [SerializeField] private float orangeFlameMinValue = 2f;
    [SerializeField] private float yellowFlameMinValue = 1f;

    private float initialScale;

    public float BurnPower { get; private set; }

    private Vector3 initialPosition;

    private float minBurnPower = 0;
    private float maxBurnPower = float.MaxValue;

    /* create a singleton */
    public static FireController instance = null;

    public float intensityChangeFrequency = 2;
    [SerializeField] private Light fireLight;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            initialPosition = transform.position;
            SetBurnPowerDecay(burnPowerDecay);
            initialScale = redFire.GetComponent<ParticleSystem>().startSize;
            BurnPower = initialBurnPower;
            /* fireLight = GameObject.Find("FireLight").GetComponent<Light>(); */
            SetFireScale(initialScale);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        /* gradually reducing burn power */
        AddBurnPower(burnPowerDecay * Time.deltaTime);
        float perlinNoise = Mathf.PerlinNoise(Time.frameCount / 1000f * intensityChangeFrequency, 0) * 500 + 500;
        fireLight.intensity = perlinNoise;
    }

    // Update is called once per frame
    void SetFireScale(float scale)
    {
        //redFire.localScale = new Vector3(scale, scale, scale);
        //yellowFire.localScale = new Vector3(scale, scale, scale);
        //oragenFire.localScale = new Vector3(scale, scale, scale);

        //float yTarget = (transform.localScale.y) / 2.0f;
        //transform.Translate(0, (yTarget - transform.position.y), 0);
        fireLight.range = (Mathf.Exp(scale / 1.5f) - 1) * 100;

        float v = 0f;
        redFire.GetComponent<ParticleSystem>().startSize = Mathf.Min(8,Mathf.Max(4 * v, (Mathf.Exp(scale / 1.5f) - 1) * 4));
        yellowFire.GetComponent<ParticleSystem>().startSize = Mathf.Min(3, Mathf.Max(v, (Mathf.Exp(scale / 1.5f) - 1) * 1f));
        oragenFire.GetComponent<ParticleSystem>().startSize = Mathf.Min(5, Mathf.Max(2 * v, (Mathf.Exp(scale / 1.5f) - 1) * 2f));

        VelocityOverLifetimeModule yellowVelocity = yellowFire.GetComponent<ParticleSystem>().velocityOverLifetime;
        VelocityOverLifetimeModule redVelocity = redFire.GetComponent<ParticleSystem>().velocityOverLifetime;
        VelocityOverLifetimeModule orangeVelocity = oragenFire.GetComponent<ParticleSystem>().velocityOverLifetime;

        yellowVelocity.y = Mathf.Min(3, scale * 1f);
        orangeVelocity.y = Mathf.Min(5, scale *3);
        redVelocity.y = Mathf.Min(8,scale * 4);
    }

    public void ResetFireScale()
    {
        SetFireScale(initialScale);
        BurnPower = initialBurnPower;
    }

    public void SetMinBurnPower(float burnPower)
    {
        minBurnPower = burnPower;
    }

    public void SetMaxBurnPower(float burnPower)
    {
        maxBurnPower = burnPower;
    }

    public void SetBurnPower(float burnPower)
    {
        BurnPower = burnPower;
        SetFireScale(BurnPower / initialBurnPower * initialScale);
    }

    public void AddBurnPower(float burnPower)
    {
        burnPower += BurnPower;
        if (burnPower < minBurnPower)
        {
            burnPower = minBurnPower;
        }
        else if(burnPower > maxBurnPower) {
            burnPower = maxBurnPower;
        }
        SetBurnPower(burnPower);
    }

    public void SetBurnPowerDecay(float burnPowerDecay)
    {
        this.burnPowerDecay = -Mathf.Abs(burnPowerDecay);
    }

}
