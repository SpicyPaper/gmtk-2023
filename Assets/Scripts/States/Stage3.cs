using UnityEngine;
using System.Collections;

public class Stage3 : StageState
{

    public Stage3(GameController gameController, GameObject newBurnable) : base(gameController, newBurnable)
    {
        currentLevel = GameState.Level3;
        waitTimeBeforeLevelStart = gameController.WaitTimeBeforeLevel3Start;
    }

    public override void Execute()
    {
        if (levelStarted && fireController.BurnPower <= gameController.BurnPowerThreshold)
        {
            StageState nextState = new Stage4(gameController, gameController.BurnablePrefabs[3]);
            gameController.ChangeState(new Shop(gameController, nextState));
        }
    }

    public override void Exit()
    {
    }
}