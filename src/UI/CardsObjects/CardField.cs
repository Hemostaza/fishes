using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
public partial class CardField : Control
{
    int maxCards = 0;

    Node cardContainer;

    public override void _Ready()
    {
        base._Ready();
        SetContainer(GetChild(0));
    }

    public void SetContainer(Node noed)
    {
        cardContainer = noed;
    }

    virtual public void AddCard(Card card)
    {
        cardContainer.AddChild(card);
    }

    virtual public void RemoveCard(Card card)
    {
        cardContainer.RemoveChild(card);
    }

    public Node GetCardContainer()
    {
        return cardContainer;
    }

    virtual public List<Card> GetCards()
    {
        List<Card> cards = [.. cardContainer.GetChildren().Cast<Card>()];
        return cards;
    }

    virtual public Card FindFirstCard(String cardName)
    {
        Array<Node> nodes = cardContainer.GetChildren();
        foreach (Card c in nodes)
        {
            if (cardName.Equals(c.GetResource().GetCardName()))
            {
                return c;
            }
        }
        return null;
    }

    // public void DiscardCard(Card card)
    // {
    //     CardResource res = card.GetResource();
    //     card.Free();

    //     CardDeck discarded = (CardDeck)GetParent().GetNode("Discarded");
    //     discarded.AddCardFirst(res);

    // }

    virtual public void OnMouseEntered(Card card)
    {
    }

    virtual public void OnMouseExited(Card card)
    {
    }

    virtual public void CardPressed(Card card)
    {
    }

    virtual public void Disable(bool disable)
    {
        foreach (Card c in GetChild(0).GetChildren())
        {
            c.Disabled(disable);
        }
    }
    virtual public void AddCardSorted(Card card)
    {

        List<Card> cards = [.. GetChild(0).GetChildren().Cast<Card>()];
        int index = 0;
        foreach (Card c in cards)
        {
            int cardStr = card.GetResource().GetCardStrenght();
            int cStr = c.GetResource().GetCardStrenght();
            if (cardStr < cStr)
            {
                break;
            }
            index++;
        }
        cardContainer.AddChild(card);
        cardContainer.MoveChild(card, index);
    }

}
