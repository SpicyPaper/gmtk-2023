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


    public void BurnableClicked(Burnable burnable)
    {
        Debug.Log("Burnable clicked from GameController");
        Debug.Log("Burnable power: " + burnable.GetBurnPower());
        HitBurnable(burnable);
    }


    private void HitBurnable(Burnable burnable)
    {
        burnable.TakeDamage(1);
    }


    public void BurnableReachedFire(Burnable burnable)
    {
        Debug.Log("Burnable reached fire");
        FireController.instance.addBurnPower(burnable.GetBurnPower());
        Destroy(burnable.gameObject);
        // Destroy(burnable.transform.parent.gameObject);
    }

}
