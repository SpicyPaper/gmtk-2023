using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableController : MonoBehaviour
{
    // Start is called before the first frame update
    private Burnable burnable;
    void Start() { 
        burnable = GetComponent<Burnable>();
    }

    // Update is called once per frame
    void Update() { }
}
