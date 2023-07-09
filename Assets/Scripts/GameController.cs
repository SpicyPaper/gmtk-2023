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

    public GameObject ShopCanvas;

    public GameObject TutorialCanvas;

    public bool CanClick = true;

    [Header("Stages Config")]
    public int NumberOfSpawnBurnables = 3;
    public float BurnPowerThreshold = 10.0f;
    public float WaitTimeBeforeLevel1Start = 3.0f;
    public float WaitTimeBeforeLevel2Start = 3.0f;
    public float WaitTimeBeforeLevel3Start = 3.0f;
    public float WaitTimeBeforeLevel4Start = 3.0f;

    [Header("Upgrade Config")]
    public float UpgradeClickCooldownModifier = 0.2f;
    public float UpgradeRangeModifier = 1.2f;
    public float UpgradePuddleModifier = 1.2f;

    [SerializeField] private float currentPuddleSize = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            FireController.instance.SetBurnPowerDecay(0);
            FireController.instance.SetMinBurnPower(19);
            FireController.instance.SetMaxBurnPower(80);
            FireController.instance.SetBurnPower(40);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        //FireController.instance.SetBurnPowerDecay(1.5f);
        State nextState = new Stage0(this, BurnablePrefabs[0]);

        ChangeState(nextState);
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

    public void SetLevel(GameState newGameState)
    {
        gameState = newGameState;
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
        FireController.instance.AddBurnPower(burnable.GetBurnPower());
        Destroy(burnable.gameObject);
        // Destroy(burnable.transform.parent.gameObject);
    }

    public void UpgradeClick()
    {
        // TODO: Upgrade click

        ((Shop)currentState).CloseShop();
    }

    public void UpgradeRange()
    {
        // TODO: Upgrade range
        ((Shop)currentState).CloseShop();
    }

    public void UpgradePuddle()
    {
        // TODO: Upgrade puddle
        SetPuddleSize(GetPuddleSize() + UpgradePuddleModifier);
        ((Shop)currentState).CloseShop();
    }


    public void SetPuddleSize(float size)
    {
        currentPuddleSize = size;
    }

    public float GetPuddleSize()
    {
        return currentPuddleSize;
    }

}
