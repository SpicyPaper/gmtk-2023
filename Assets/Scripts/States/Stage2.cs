using UnityEngine;

public class Stage2 : StageState
{
    public Stage2(GameController gameController, GameObject newBurnable) : base(gameController, newBurnable)
    {
        currentLevel = GameState.Level2;
        waitTimeBeforeLevelStart = gameController.WaitTimeBeforeLevel2Start;
    }

    public override void Execute()
    {
        if (fireController.BurnPower <= gameController.BurnPowerThreshold)
        {
            StageState nextState = new Stage3(gameController, gameController.BurnablePrefabs[2]);
            gameController.ChangeState(new Shop(gameController, nextState));
        }
    }

    public override void Exit()
    {
    }
}