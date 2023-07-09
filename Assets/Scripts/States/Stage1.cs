using UnityEngine;
using System.Collections;

public class Stage1 : StageState
{
    protected float waitTimeBeforeLevelStart = 3.0f;

    public Stage1(GameController gameController, GameObject newBurnable) : base(gameController, newBurnable)
    {
        currentLevel = GameState.Level1;
    }

    public override void Execute()
    {
        if (fireController.BurnPower <= 10)
        {
            StageState nextState = new Stage2(gameController, gameController.BurnablePrefabs[1]);
            gameController.ChangeState(new Shop(gameController, nextState));

        }
    }

    public override void Exit()
    {
    }
}