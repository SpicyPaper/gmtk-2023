using UnityEngine;

public class StageState : State
{
    int numberOfBurnables = 5;

    protected FireController fireController;

    GameObject burnable;

    public StageState(GameController gameController, GameObject newBurnable) : base(gameController)
    {
        burnable = newBurnable;
    }

    public override void Enter()
    {
        fireController = GameObject.FindWithTag("Fire").GetComponent<FireController>();
        for (int i = 0; i < numberOfBurnables; i++)
        {
            GameObject gameObject = GameObject.Instantiate(
                burnable,
                GetRandomSpawnPosition(),
                Quaternion.identity
            );
            Burnable b = gameObject.GetComponentInChildren<Burnable>();
            b.SetSpeed(10);
        }

    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomAngle = Random.Range(0.0f, 360.0f);
        Vector3 randomDirection = Quaternion.Euler(0.0f, randomAngle, 0.0f) * Vector3.forward;
        Vector3 spawnPosition = randomDirection * 40;
        return spawnPosition;
    }
}