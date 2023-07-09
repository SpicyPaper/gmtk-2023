using UnityEngine;

public class StageState : State
{
    int numberOfBurnables = 10;

    FireController fireController;

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
            b.SetSpeed(20);
        }

    }

    public override void Execute()
    {
        // Check if camera has reached its final position.
        if (fireController.BurnPower <= 0)
        {
            // gameController.ChangeState(new SomeOtherState(gameController));
        }
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