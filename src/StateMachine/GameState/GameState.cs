using Godot;
using System;

public partial class GameState : Node
{
    [Signal]
    public delegate void transitionedGameStateEventHandler(GameState gameState, String newStateName);

    virtual public void InitState(){
    }

    virtual public void Enter(){
    }

    virtual public void Exit(){
    }

    virtual public void Update(double delta){
    }

    virtual public void PhysicsUpdate(double delta){

    }

}
