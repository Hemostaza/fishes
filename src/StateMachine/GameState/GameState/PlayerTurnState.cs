using Godot;
using System;

public partial class PlayerTurnState : GameState
{

    double turnTime = 3.0;
    [Export]
    CardController cardController;
    Button nextTurn;

    public override void InitState()
    {
        base.InitState();
        nextTurn = (Button) cardController.GetNode("NextTurn");
        nextTurn.Pressed += NextTurn;
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
            //EmitSignal(SignalName.transitionedGameState,this,"ActionTimeState");
        }
    }

    public void NextTurn(){
        EmitSignal(SignalName.transitionedGameState,this,"ActionTimeState");
    }

}
