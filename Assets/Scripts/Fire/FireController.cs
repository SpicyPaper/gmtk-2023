using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float initialScale;

    public float BurnPower { get; private set; }

    private Vector3 initialPosition;

    /* create a singleton */
    public static FireController instance = null;

    public float intensityChangeFrequency = 2;
    [SerializeField] private Light fireLight;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            initialPosition = transform.position;
            burnPowerDecay = -Mathf.Abs(burnPowerDecay);
            initialScale = redFire.GetComponent<ParticleSystem>().startSize;
            BurnPower = initialBurnPower;
            /* fireLight = GameObject.Find("FireLight").GetComponent<Light>(); */
            setFireScale(initialScale);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void setFireScale(float scale)
    {
        //redFire.localScale = new Vector3(scale, scale, scale);
        //yellowFire.localScale = new Vector3(scale, scale, scale);
        //oragenFire.localScale = new Vector3(scale, scale, scale);

        //float yTarget = (transform.localScale.y) / 2.0f;
        //transform.Translate(0, (yTarget - transform.position.y), 0);
        fireLight.range = (Mathf.Exp(scale / 1.5f)-1) * 100;

        float v = 0.4f;
        redFire.GetComponent<ParticleSystem>().startSize = Mathf.Max(4*v,(Mathf.Exp(scale / 1.5f) - 1) * 4) ;
        yellowFire.GetComponent<ParticleSystem>().startSize = Mathf.Max( v, (Mathf.Exp(scale / 1.5f) - 1) * 1f);
        oragenFire.GetComponent<ParticleSystem>().startSize = Mathf.Max(2 * v, (Mathf.Exp(scale / 1.5f) - 1) * 2f);
    }

    public void ResetFireScale()
    {
        setFireScale(initialScale);
        BurnPower = initialBurnPower;
    }

    public void addBurnPower(float burnPower)
    {
        this.BurnPower += burnPower;
        if (this.BurnPower < 0)
        {
            this.BurnPower = 0;
        }
        else
        {
            setFireScale(this.BurnPower / initialBurnPower * initialScale);
        }
    }

    void FixedUpdate()
    {
        /* gradually reducing burn power */
        addBurnPower(burnPowerDecay * Time.deltaTime);
        float perlinNoise = Mathf.PerlinNoise(Time.frameCount / 1000f * intensityChangeFrequency, 0) * 500 + 500;
        fireLight.intensity = perlinNoise;
    }

}
