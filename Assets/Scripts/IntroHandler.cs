using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroHandler : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    [SerializeField] private Camera cam;

    [SerializeField] private List<float> timestamps;

    private int currentTimestamp;

    private float elapsed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;

        switch (currentTimestamp)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            default:
                break;
        }

        if (elapsed > timestamps[currentTimestamp])
        {
            elapsed -= timestamps[currentTimestamp];

            currentTimestamp++;
        }
    }
}
