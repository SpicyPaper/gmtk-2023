using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    /* TODO: create a singleton to handle game logic */
    public static GameController instance = null;



    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
    }


    public bool BurnableClicked(Burnable burnable)
    {
        bool isBurnableDead = HitBurnable(burnable);

        return isBurnableDead;
    }


    private bool HitBurnable(Burnable burnable)
    {
        return burnable.TakeDamage(1);
    }


    public void BurnableReachedFire(Burnable burnable)
    {
        FireController.instance.addBurnPower(burnable.GetBurnPower());
        Destroy(burnable.gameObject);
        // Destroy(burnable.transform.parent.gameObject);
    }

}
