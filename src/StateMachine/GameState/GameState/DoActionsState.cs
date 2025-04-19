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

        // Card cardInstance = (Card)cardScene.Instantiate();
        // cardInstance.CreateCard(cr);


        actionTime = 3.0;
        GD.Print(this.Name);
        cardController.DisableDecks(true);
        cardController.PlayCards();
        //cardController.GetCardsInPlayField().SortChildren();
        // CardField played = cardController.GetPlayedCardsField();
        // CardField inPlay = cardController.GetCardsInPlayField();
        //dictionary = cardController.getcardsInPlay();

        //SPawn FIshes
        // Array<Node> children = played.GetCardContainer().GetChildren();

        // //SPawnuj nowe RYBY
        // foreach (Card c in children)
        // {
        //     //if c is card
        //     //if()
        //     //load Resource to entity by card Name
        //     //spawn entity
        //     String cardName = c.GetResource().GetCardName();
        //     FishData fishData = FishDataResources.Instance.GetFishDataByName(cardName);
        //     if (fishData == null)
        //     {
        //         break;
        //     }
        //     Entity instance = (Fish)fishScene.Instantiate();
        //     instance.Spawn(fishData);
        //     AddChild(instance);
        //     instance.LinkCard(c);
        //     cardLink.Add(c, instance);
        //     //if(fishres has string)
        //     //spawn fish
        // }

        // played.SendAllCards(inPlay);
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

    public void EndTurns(){
        CardField inPlay = cardController.GetCardsInPlayField();
        foreach (Card card in inPlay.GetCardContainer().GetChildren())
        {
            //card.TestAction();
            card.EndTrun(cardController);
        }
    }
}
