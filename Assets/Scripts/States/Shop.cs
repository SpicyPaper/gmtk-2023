using UnityEngine;

public class Shop : State
{
    State nextState;

    float waitTime = 5.0f;

    public Shop(GameController gameController, State nextState) : base(gameController)
    {
        this.nextState = nextState;
    }

    public override void Enter()
    {
        gameController.Canvas.SetActive(true);
        gameController.SetLevel(GameState.Shop);
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
        gameController.Canvas.SetActive(false);
    }

    public void CloseShop()
    {
        gameController.ChangeState(nextState);
    }
}