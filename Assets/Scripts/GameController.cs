using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    /* TODO: create a singleton to handle game logic */
    public static GameController instance = null;
    public static GameState gameState = GameState.Menu;
    private State currentState;

    public List<GameObject> BurnablePrefabs;


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

        ChangeState(new Stage1(this, BurnablePrefabs[0]));
    }

    void Update()
    {
        // Call Execute on the current state every frame.
        currentState.Execute();
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;

        if (currentState != null)
            currentState.Enter();
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
