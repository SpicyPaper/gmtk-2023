using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float initialBurnPower = 100.0f;

    [SerializeField]
    private float burnPowerDecay = 1f;

    private float initialScale;

    private float burnPower;

    private Vector3 initialPosition;


    [SerializeField]
    private Transform redFire;

    [SerializeField]
    private Transform orangeFire;

    [SerializeField]
    private Transform YellowFire;

    /* create a singleton */
    public static FireController instance = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            initialPosition = transform.position;
            burnPowerDecay = -Mathf.Abs(burnPowerDecay);
            initialScale = redFire.localScale.x;
            burnPower = initialBurnPower;
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
        orangeFire.localScale = new Vector3(scale, scale, scale);
        YellowFire.localScale = new Vector3(scale, scale, scale);

        //transform.localScale = new Vector3(scale, scale, scale);
        //float yTarget = (transform.localScale.y) / 2.0f;
        //transform.Translate(0, (yTarget - transform.position.y), 0);
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
    }
}
