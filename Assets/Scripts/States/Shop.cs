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
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}