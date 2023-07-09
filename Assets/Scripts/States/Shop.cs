using UnityEngine;

public class Shop : State
{
    State nextState;

    public Shop(GameController gameController, State nextState) : base(gameController)
    {
        this.nextState = nextState;
    }

    public override void Enter()
    {
        gameController.ShopCanvas.SetActive(true);
        gameController.SetLevel(GameState.Shop);
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
        gameController.ShopCanvas.SetActive(false);
    }

    public void CloseShop()
    {
        gameController.ChangeState(nextState);
    }
}