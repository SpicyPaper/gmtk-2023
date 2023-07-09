using UnityEngine;
using System.Collections;

public class Stage3 : StageState
{
    protected float waitTimeBeforeLevelStart = 3.0f;

    public Stage3(GameController gameController, GameObject newBurnable) : base(gameController, newBurnable)
    {

        currentLevel = GameState.Level3;
        waitTimeBeforeLevelStart = gameController.WaitTimeBeforeLevel3Start;
    }

    public override void Execute()
    {
        // if (fireController.BurnPower <= 10)
        // {
        //     StageState nextState = new Stage2(gameController, gameController.BurnablePrefabs[1]);
        //     gameController.ChangeState(new Shop(gameController, nextState));
        // }
    }

    public override void Exit()
    {
    }
}