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

    /* create a singleton */
    public static FireController instance = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            burnPowerDecay = -Mathf.Abs(burnPowerDecay);
            initialScale = transform.localScale.x;
            burnPower = initialBurnPower;
            setScale(initialScale);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void setScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
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
            setScale(this.burnPower / initialBurnPower * initialScale);
        }
    }

    void FixedUpdate()
    {
        /* gradually reducing burn power */
        addBurnPower(burnPowerDecay * Time.deltaTime);
    }
}
