using Godot;
using Godot.Collections;
using System;

public partial class CardController : Node
{
    [Export]
    CardDeck playerDeck;
    [Export]
    CardResource[] playerCards;
    CardDeck foodDeck;
    [Export]
    CardDeck discardedDeck;
    [Export]
    PackedScene cardScene;
    [Export]
    CardField playedCardsField;
    [Export]
    CardField playerField;
    [Export]
    CardField cardsInPlay;

    [Export]
    public PackedScene fishScene;



    Dictionary<Card, Entity> cardLink;

    void populateDecks()
    {
        playerDeck.PopulateDeck(playerCards);
    }

    public override void _Ready()
    {
        base._Ready();
        cardLink = new Dictionary<Card, Entity>();
        populateDecks();
        playerDeck.cardScene = cardScene;
    }

    public void DisableDecks(bool disabled)
    {
        playerDeck.Disabled = disabled;
        playerField.Disable(disabled);
        playedCardsField.Disable(disabled);

    }

    public void ResetDrawCount()
    {
        playerDeck.Reset();
    }

    public CardField GetPlayedCardsField()
    {
        return playedCardsField;
    }

    public CardField GetCardsInPlayField()
    {
        return cardsInPlay;
    }

    public CardDeck GetDiscardedDeck()
    {
        return discardedDeck;
    }

    public void DiscardCard(Card card)
    {
        CardResource cardResource = card.GetResource();
        try{
            Entity entity = cardLink[card];
            entity.QueueFree();
        }catch(Exception e){
            GD.Print("no entitit "+e);
        }
        card.QueueFree();
        discardedDeck.AddCardFirst(cardResource);
    }

    public void PlayCards()
    {
        Array<Node> children = playedCardsField.GetCardContainer().GetChildren();

        foreach (Card c in children)
        {
            c.OnPlayed(this);
            // String cardName = c.GetResource().GetCardName();
            // FishData fishData = FishDataResources.Instance.GetFishDataByName(cardName);
            // if (fishData != null)
            // {
            //     Entity instance = (Entity)fishScene.Instantiate();
            //     instance.Spawn(fishData);
            //     instance.TreeExited += () => OnEntityExit(instance);
            //     AddChild(instance);
            //     instance.LinkCard(c);
            //     cardLink.Add(c, instance);
            // }
            //c.ChangeCardFieldSort(cardsInPlay);
        }
    }

    public Card FindInPlayedCards(String cardName)
    {
        Array<Node> nodes = playedCardsField.GetCardContainer().GetChildren();
        foreach (Card c in nodes)
        {
            if (cardName.Equals(c.GetResource().GetCardName()))
            {
                return c;
            }
        }
        return null;
    }

    // public void DoCardActions()
    // {
    //     Array<Node> nodes = playedCardsField.GetCardContainer().GetChildren();
    //     foreach (Card c in nodes)
    //     {
    //         c.DoAction(this);
    //     }
    // }

    public Entity GetLinkedEntity(Card card)
    {
        return cardLink[card];
    }

    public void OnEntityExit(Entity entity)
    {
        DiscardCard(entity.linkedCard);
    }

    public Dictionary<Card, Entity> GetLinkedCards()
    {
        return cardLink;
    }

}
