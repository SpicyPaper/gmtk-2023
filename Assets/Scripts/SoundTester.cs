using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SoundTester : MonoBehaviour
{
    private float elapsed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed > 4)
        {
            Debug.Log(elapsed);
            elapsed = 0;
            SoundHandler.SoundType type = (SoundHandler.SoundType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(SoundHandler.SoundType)).Length);
            SoundHandler.Instance.PlaySound(type);
        }
    }
}
