using UnityEngine;
using System.Collections;

public class Stage4 : StageState
{

    public Stage4(GameController gameController, GameObject newBurnable) : base(gameController, newBurnable)
    {
        currentLevel = GameState.Level4;
        waitTimeBeforeLevelStart = gameController.WaitTimeBeforeLevel4Start;
    }

    public override void Execute()
    {
        if (levelStarted && fireController.BurnPower <= gameController.BurnPowerThreshold)
        {
            // TODO handle win screen
        }
    }

    public override void Exit()
    {
    }
}