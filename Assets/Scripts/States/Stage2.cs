using UnityEngine;

public class Stage2 : StageState
{
    protected float waitTimeBeforeLevelStart = 3.0f;
    public Stage2(GameController gameController, GameObject newBurnable) : base(gameController, newBurnable)
    {
        currentLevel = GameState.Level2;
    }

    public override void Execute()
    {
        if (fireController.BurnPower <= 10)
        {
            StageState nextState = new Stage3(gameController, gameController.BurnablePrefabs[2]);
            gameController.ChangeState(new Shop(gameController, nextState));
        }
    }

    public override void Exit()
    {
    }
}