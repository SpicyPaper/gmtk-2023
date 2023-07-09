using UnityEngine;
using System.Collections;

public class Stage0 : StageState
{

    public Stage0(GameController gameController, GameObject newBurnable) : base(gameController, newBurnable)
    {
        currentLevel = GameState.Level0;
        waitTimeBeforeLevelStart = gameController.WaitTimeBeforeLevel1Start;
    }

    public override void Execute()
    {
        if (levelStarted)
        {
            StageState nextnextState = new Stage1(gameController, gameController.BurnablePrefabs[0]);
            Tutorial nextState = new Tutorial(gameController, nextnextState);
            gameController.ChangeState(nextState);
        }
    }

    public override void Exit()
    {
    }
}