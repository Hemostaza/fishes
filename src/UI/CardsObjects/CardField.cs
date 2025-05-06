using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
public partial class CardField : Control
{
    public int maxCards = 0;

    Node cardContainer;


    const int xCardSize = 125;
    const float xOffser = (float)xCardSize / 2;

    public override void _Ready()
    {
        base._Ready();
        SetContainer(GetChild(0));
    }

    public void SetContainer(Node noed)
    {
        cardContainer = noed;
    }

    public bool HasSpace()
    {
        return maxCards == 0 ? true : maxCards > cardContainer.GetChildCount();
    }

    virtual public void AddCard(Card card)
    {
        //cardContainer.AddChild(card);
        card.Reparent(cardContainer);
        card.MouseEnteredCardHand += OnMouseEntered;
        card.MouseExitedCardHand += OnMouseExited;
    }
    virtual public void RemoveCard(Card card)
    {
        //cardContainer.RemoveChild(card);
        card.MouseEnteredCardHand -= OnMouseEntered;
        card.MouseExitedCardHand -= OnMouseExited;
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

    virtual public void OnMouseEntered(Card card)
    {
    }

    virtual public void OnMouseExited(Card card)
    {
    }

    virtual public void CardPressed(Card card)
    {
    }

    virtual public void Disable(bool disable, bool setGray)
    {
        foreach (Card c in GetChild(0).GetChildren())
        {
            c.Disabled(disable);
        }
    }

    virtual public void Disable(bool disable)
    {
        Disable(disable,true);
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
        card.Reparent(cardContainer);
        cardContainer.MoveChild(card, index);
    }

}
