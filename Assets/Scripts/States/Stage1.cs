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

        levelStarted = true;
    }

    public override void Execute()
    {
        FireController.instance.SetBurnPowerDecay(1.5f);

        if (levelStarted && FireController.instance.BurnPower <= gameController.BurnPowerThreshold)
        {
            StageState nextState = new Stage2(gameController, gameController.BurnablePrefabs[1]);
            gameController.ChangeState(new Shop(gameController, nextState));
        }
    }
}