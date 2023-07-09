using UnityEngine;
using System.Collections;

public class Stage1 : StageState
{

    public Stage1(GameController gameController, GameObject newBurnable) : base(gameController, newBurnable)
    {
        currentLevel = GameState.Level1;
        waitTimeBeforeLevelStart = gameController.WaitTimeBeforeLevel1Start;
    }

    public override void Enter()
    {
        GameController.gameState = currentLevel;
    }

    public override void Execute()
    {
        FireController.instance.SetBurnPowerDecay(1);

        if (levelStarted && fireController.BurnPower <= gameController.BurnPowerThreshold)
        {
            StageState nextState = new Stage2(gameController, gameController.BurnablePrefabs[1]);
            gameController.ChangeState(new Shop(gameController, nextState));
        }
    }

    public override void Exit()
    {
    }
}