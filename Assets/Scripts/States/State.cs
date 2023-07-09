using UnityEngine;

public abstract class State
{
    protected GameController gameController;

    public State(GameController gameController)
    {
        this.gameController = gameController;
    }

    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }
}