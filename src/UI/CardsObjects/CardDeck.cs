using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public partial class CardDeck : Button
{
    LinkedList<CardResource> cards = new LinkedList<CardResource>();
    RandomNumberGenerator rng = new RandomNumberGenerator();
    [Export]
    CardField target;

    [Export]
    int maxDrawCount = 1;
    int currentDraw = 0;

    Label drawLeftLabel;
    Label cardsLeftInDeck;

    public PackedScene cardScene;

    [Export]
    PackedScene newScene;

    public override void _Ready()
    {
        base._Ready();
        drawLeftLabel = (Label)GetNode("DrawsLeft");
        cardsLeftInDeck = (Label)GetNode("CardsLeft");
        drawLeftLabel.Text = (maxDrawCount - currentDraw).ToString();
        cardsLeftInDeck.Text = cards.Count.ToString();

    }

    public void PopulateDeck(CardResource[] cardResources)
    {
        if (cardResources == null || cardResources.Length == 0)
        {
            return;
        }
        foreach (CardResource cr in cardResources)
        {
            if (rng.RandiRange(0, 100) > 50)
            {
                AddCardFirst(cr);
            }
            else AddCardLast(cr);
        }

        cardsLeftInDeck.Text = cards.Count.ToString();
    }

    public void AddCardLast(CardResource card)
    {
        cards.AddLast(card);
        cardsLeftInDeck.Text = cards.Count.ToString();
    }

    public void AddCardFirst(CardResource card)
    {
        cards.AddFirst(card);
        cardsLeftInDeck.Text = cards.Count.ToString();
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (CardResource c in cards)
        {
            stringBuilder.Append(c + " \n");
        }
        return stringBuilder.ToString();
    }

    public void DrawCard()
    {
        DrawCard(target);
    }

    public void DrawCard(CardField target)
    {
        try
        {
            CardResource card = cards.First.Value;

            Card cardInstance = (Card)cardScene.Instantiate();
            cardInstance.CreateCard(card);
            target.AddCard(cardInstance);

            ChangeCurrentDraw(1);
            cards.RemoveFirst();
            cardsLeftInDeck.Text = cards.Count.ToString();
        }
        catch (Exception e)
        {
            GD.Print(e);
        }
    }

    public void ChangeCurrentDraw(int amount)
    {
        currentDraw += amount;
        drawLeftLabel.Text = (maxDrawCount - currentDraw).ToString();
    }

    public override void _Pressed()
    {
        base._Pressed();
        if (currentDraw < maxDrawCount)
        {
            DrawCard();
        }
        else
        {
            GD.Print("MaxCount in this round");
        }
    }

    public void Reset()
    {
        currentDraw = 0;
        drawLeftLabel.Text = (maxDrawCount - currentDraw).ToString();
    }
    public void ChangeMaxDrawCount(int amount)
    {
        maxDrawCount += amount;
        drawLeftLabel.Text = (maxDrawCount - currentDraw).ToString();
    }


}
