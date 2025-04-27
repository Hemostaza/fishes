using Godot;
using Godot.Collections;
using System;

public partial class CardController : Node
{
    //Text array of cards
    [Export]
    CardResource[] playerCards;

    [Export]
    CardDeck playerDeck;
    CardDeck foodDeck;
    [Export]
    CardDeck discardedDeck;
    [Export]
    PackedScene cardScene;
    [Export]
    CardField newlyPlayedField;
    [Export]
    PlayerHandCardField playerField;
    [Export]
    CardField inPlayField;
    
    // [Export]
    // Button discardBtn;


    //Karta wysyła sygnał że się wypierdala na ryj i odrzuca
    //Przyjmuje sygnał i wykonuej funkcje discardCard na karcie która sygnał wysłała?
    //Moze polekart wysyłą sygnał o dobrzuceniu kary mowiac ktora karte wyjebao



    void populateDecks()
    {
        playerDeck.PopulateDeck(playerCards);
    }

    public override void _Ready()
    {
        base._Ready();
        populateDecks();
        playerDeck.cardScene = cardScene;
        playerField.discardedFromHand += DiscardCard;
        //discardBtn.Pressed += DiscardAction;
    }

    public void DisableInput(bool disabled)
    {
        playerDeck.Disabled = disabled;
        playerField.Disable(disabled);
        newlyPlayedField.Disable(disabled);
        playerField.DisableButtons(disabled);

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
        discardedDeck.AddCardFirst(cardResource);
    }

    public void DiscardAction(){
        
        //Card dicardedCard = (Card) discardBtn.GetParent();
        //dicardedCard.RemoveChild(discardBtn);
        // DiscardCard(((PlayerHandCardField)playerField).GetActiveCard());
        // ((PlayerHandCardField)playerField).SetActiveCard(null);
    }


    public void PlayCards()
    {
        Array<Node> children = newlyPlayedField.GetCardContainer().GetChildren();

        foreach (Card c in children)
        {
            newlyPlayedField.RemoveCard(c);
            inPlayField.AddCardSorted(c);
            c.OnPlayed(this);
        }
    }

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
