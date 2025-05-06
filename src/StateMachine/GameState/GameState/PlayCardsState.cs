using Godot;
using System;
using System.Collections.Generic;

public partial class PlayCardsState : GameState
{
    [Export]
    CardController cardController;
    Button nextState;


    List<Card> cardsToPlay;
    CardField cardsInPlayField;
    Tween playCardAnimTween;

    public override void InitState()
    {
        base.InitState();
        nextState = (Button)cardController.GetNode("nextState");
        nextState.Pressed += NextState;
        cardsInPlayField = cardController.GetCardsInPlayField();

        cardController.OnPlayCardEnd += NextState;

    }

    public override void Enter()
    {
        base.Enter();
        cardController.DisableInput(true);
        cardsToPlay = cardController.GetPlayedCardsField().GetCards();
        PlayCard(cardsToPlay);

        //cardController.PlayCards();
    }
    public void PlayCard(List<Card> cardsToPlay){
        if(cardsToPlay.Count<=0 || cardsToPlay==null){
            GD.Print("sygnal jebniety");
            EmitSignal(SignalName.transitionedGameState, this, "ActionTimeState");
            return;
        }
        Card card = cardsToPlay[0];
        card.Reparent(cardController);
        playCardAnimTween = GetTree().CreateTween();
        playCardAnimTween.TweenProperty(cardsToPlay[0], "position", new Vector2(0,0), 0.5f);
        playCardAnimTween.TweenCallback(Callable.From(() =>
        {
            cardsToPlay.RemoveAt(0);
            cardsInPlayField.AddCardSorted(card);
            card.OnPlayed(cardController);
            PlayCard(cardsToPlay);
        }));
    }

    void NextState()
    {
        GD.Print("Sygnal odjebany");
        EmitSignal(SignalName.transitionedGameState, this, "ActionTimeState");
    }

}
