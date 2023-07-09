using UnityEngine;

public class Stage1 : StageState
{

    public Stage1(GameController gameController, GameObject newBurnable) : base(gameController, newBurnable)
    {

    }

    public override void Execute()
    {
        if (fireController.BurnPower <= 0)
        {
            gameController.ChangeState(new Stage2(gameController, gameController.BurnablePrefabs[1]));
        }
    }

    public override void Exit()
    {
    }
}