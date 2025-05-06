using System;
using Godot;
using Godot.Collections;

public partial class DoActionsState : GameState
{
    double actionSpeed = 1.0;
    [Export]
    CardController cardController;

    [Export]
    PackedScene fishScene;

    [Export]
    PackedScene cardScene;
    [Export]
    CardResource cr;

    public override void InitState()
    {
        base.InitState();
    }

    public override void Enter()
    {
        base.Enter();

        actionSpeed = 1.0;
        //TODO: sprawdzać czy input jest wyłączony zanim się go znów wyłączy
        //cardController.DisableInput(true);
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        actionSpeed -= delta;


        if (actionSpeed <= 0)
        {
            EmitSignal(SignalName.transitionedGameState, this, "PlayerTurnState");
        }
    }

    void StopAction()
    {
       // GD.Print("chuj");
    }

    public override void Exit()
    {
        base.Exit();
        EndTurns();
    }


    public void DoActions()
    {
        CardField inPlay = cardController.GetCardsInPlayField();
        foreach (Card card in inPlay.GetCardContainer().GetChildren())
        {
            //card.TestAction();
            card.DoAction(cardController);
            //card.OnActionEnd += () => GD.Print("cipa");
        }
    }

    public void EndTurns()
    {
        CardField inPlay = cardController.GetCardsInPlayField();
        foreach (Card card in inPlay.GetCardContainer().GetChildren())
        {
            //card.TestAction();
            card.EndTrun(cardController);
        }
    }
}
