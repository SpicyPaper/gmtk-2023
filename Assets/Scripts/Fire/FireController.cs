using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float initialBurnPower = 50f;

    [SerializeField]
    private float burnPowerDecay = 1f;

    [SerializeField] private Transform redFire;

    [SerializeField] private Transform oragenFire;

    [SerializeField] private Transform yellowFire;

    private float initialScale;

    private float burnPower;

    private Vector3 initialPosition;

    /* create a singleton */
    public static FireController instance = null;

    public float intensityChangeFrequency = 2;
    [SerializeField]private Light fireLight;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            initialPosition = transform.position;
            burnPowerDecay = -Mathf.Abs(burnPowerDecay);
            initialScale = redFire.localScale.x;
            burnPower = initialBurnPower;
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
        redFire.localScale = new Vector3(scale, scale, scale);
        yellowFire.localScale = new Vector3(scale, scale, scale);
        oragenFire.localScale = new Vector3(scale, scale, scale);

        //float yTarget = (transform.localScale.y) / 2.0f;
        //transform.Translate(0, (yTarget - transform.position.y), 0);
        fireLight.range = scale*15;

        redFire.GetComponent<ParticleSystem>().startSize = scale/2;
        yellowFire.GetComponent<ParticleSystem>().startSize = scale / 2 * 0.25f;
        oragenFire.GetComponent<ParticleSystem>().startSize = scale / 2 * 0.5f;
    }

    public void addBurnPower(float burnPower)
    {
        this.burnPower += burnPower;
        if (this.burnPower < 0)
        {
            this.burnPower = 0;
        }
        else
        {
            setFireScale(this.burnPower / initialBurnPower * initialScale);
        }
    }

    void FixedUpdate()
    {
        /* gradually reducing burn power */
        addBurnPower(burnPowerDecay * Time.deltaTime);
        float perlinNoise = Mathf.PerlinNoise(Time.frameCount/1000f*intensityChangeFrequency,0) * 500 + 500;
        fireLight.intensity = perlinNoise;
    }

}
