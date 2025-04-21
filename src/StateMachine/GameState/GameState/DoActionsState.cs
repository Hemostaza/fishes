using System;
using Godot;
using Godot.Collections;

public partial class DoActionsState : GameState
{
    double actionTime = 3.0;
    [Export]
    CardController cardController;

    Dictionary<Card, Entity> cardLink;

    [Export]
    PackedScene fishScene;

    [Export]
    PackedScene cardScene;
    [Export]
    CardResource cr;

    public override void InitState()
    {
        base.InitState();
        cardLink = new Dictionary<Card, Entity>();
    }

    public override void Enter()
    {
        base.Enter();

        actionTime = 3.0;
        GD.Print(this.Name);
        cardController.DisableDecks(true);
        cardController.PlayCards();
        DoActions();
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        actionTime -= delta;

        if (actionTime <= 0)
        {
            EmitSignal(SignalName.transitionedGameState, this, "PlayerTurnState");
        }
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
