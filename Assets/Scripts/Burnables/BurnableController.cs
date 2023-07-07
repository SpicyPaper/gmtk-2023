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

    void OnMouseDown()
    {
        Debug.Log("Clicked on " + gameObject.name);
        /* get the GameController instance it is a global singleton*/
        GameController gameController = GameController.instance;
        /* call the BurnableClicked method on the GameController instance */
        gameController.BurnableClicked(burnable);

    }
}
