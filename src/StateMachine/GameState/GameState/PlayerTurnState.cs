using Godot;
using System;

public partial class PlayerTurnState : GameState
{

    double turnTime = 3.0;
    [Export]
    CardController cardController;

    public override void InitState()
    {
        base.InitState();
    }

    public override void Enter()
    {
        base.Enter();
        turnTime = 3.0;
        GD.Print(this.Name);
        cardController.DisableInput(false);
        cardController.ResetDrawCount();
    }


    public override void Update(double delta){
        base.Update(delta);
        turnTime-=delta;

        if(turnTime<=0){
             EmitSignal(SignalName.transitionedGameState,this,"ActionTimeState");
        }
    }

}
