using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Drawing;

public partial class CardController : Control
{
    //Text array of cards
    [Export]
    CardResource[] playerCards;

    [Export]
    CardDeck playerDeck;
    [Export]
    CardDeck discardedDeck;
    [Export]
    PackedScene cardScene;
    [Export]
    PackedScene newCardScene;
    [Export]
    CardField newlyPlayedField;
    [Export]
    PlayerHandCardField playerField;
    [Export]
    CardField inPlayField;
    [Export]
    FoodCard foodCard;
    [Export]
    CardDeck foodDeck;

    Tween cardDrawAnimTween;
    // Tween playCardAnimTween;

    [Signal]
    public delegate void OnPlayCardEndEventHandler();

    void populateDecks()
    {
        playerDeck.PopulateDeck(playerCards);

        for (int i = 0; i < 20; i++)
        {
            foodDeck.AddCard(foodCard);
        }

    }

    public override void _Ready()
    {
        base._Ready();
        populateDecks();
        playerDeck.cardScene = cardScene;
        playerDeck.OnCardDraw += DrawCard;
        discardedDeck.cardScene = cardScene;
        discardedDeck.OnCardDraw += DrawCard;
        foodDeck.cardScene = cardScene;
        foodDeck.OnCardDraw += DrawCard;
        playerField.discardedFromHand += DiscardCard;
    }

    public void DisableInput(bool disabled)
    {
        //Sprawdzić czy już wyłączone żeby nie płakało przy odłaczaniu odłączonych sygnałów
        // if(playerDeck.Disabled==disabled){
        //     return;
        // }
        playerDeck.Disabled = disabled;
        discardedDeck.Disabled = disabled;
        playerField.Disable(disabled);
        newlyPlayedField.Disable(disabled);
    }

    //WYjebać to do PlayerTurnState
    //cardcontroller.getbutton += drawkard w state
    //wtedy nie musze martwic sie o wyalczanie guziczkow jo?
    void DrawCard(CardDeck source, CardResource cardResource, CardField target)
    {
        Card card = (Card)cardScene.Instantiate();
        card.CreateCard(cardResource);
        int xOffset = 125;
        AddChild(card);
        source.Disabled = true;
        card.Position = source.Position;
        target.Disable(true, false);
        cardDrawAnimTween = GetTree().CreateTween();
        cardDrawAnimTween.TweenProperty(card, "position", new Vector2(target.Position.X + xOffset * target.GetChild(0).GetChildCount(), target.Position.Y), 0.2f);
        cardDrawAnimTween.TweenCallback(Callable.From(() =>
        {
            //RemoveChild(card);
            target.Disable(false, false);
            target.AddCard(card);
            source.Disabled = false;
        }));

    }

    public void ResetDrawCount()
    {
        playerDeck.Reset();
    }

    public CardField GetPlayedCardsField()
    {
        return newlyPlayedField;
    }

    public CardField GetCardsInPlayField()
    {
        return inPlayField;
    }

    public CardDeck GetDiscardedDeck()
    {
        return discardedDeck;
    }

    public void DiscardCard(Card card)
    {
        CardResource cardResource = card.GetResource();
        card.QueueFree();
        discardedDeck.AddCard(cardResource, true);
    }

    // public void PlayCards()
    // {
    //     //List<Card> children = newlyPlayedField.GetCards();
    //     PlayCard(newlyPlayedField.GetCards());
    // }
    // public void PlayCard(List<Card> cardsToPlay){
    //     if(cardsToPlay.Count<=0 || cardsToPlay==null){
    //         GD.Print("sygnal jebniety");
    //         EmitSignal(SignalName.OnPlayCardEnd);
    //         return;
    //     }
    //     Card card = cardsToPlay[0];
    //     card.Reparent(this);
    //     playCardAnimTween = GetTree().CreateTween();
    //     playCardAnimTween.TweenProperty(cardsToPlay[0], "position", new Vector2(0,0), 0.5f);
    //     playCardAnimTween.TweenCallback(Callable.From(() =>
    //     {
    //         inPlayField.AddCardSorted(card);
    //         card.OnPlayed(this);
    //         PlayCard(newlyPlayedField.GetCards());
    //     }));

    // }



    public Card FindInPlayedCards(String cardName)
    {
        Array<Node> nodes = newlyPlayedField.GetCardContainer().GetChildren();
        foreach (Card c in nodes)
        {
            if (cardName.Equals(c.GetResource().GetCardName()))
            {
                return c;
            }
        }
        return null;
    }

}
