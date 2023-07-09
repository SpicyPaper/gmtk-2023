using UnityEngine;

public class Stage2 : StageState
{

    public Stage2(GameController gameController, GameObject newBurnable) : base(gameController, newBurnable)
    {

    }

    public override void Execute()
    {
        if (fireController.BurnPower <= 0)
        {
            // gameController.ChangeState(new SomeOtherState(gameController));
        }
    }

    public override void Exit()
    {
    }
}