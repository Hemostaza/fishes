using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public partial class CardDeck : Button
{
    public LinkedList<CardResource> cards = new LinkedList<CardResource>();
    RandomNumberGenerator rng = new RandomNumberGenerator();
    [Export]
    CardField target;

    [Export]
    int maxDrawCount = 1;
    int currentDraw = 0;

    Label drawLeftLabel;
    public int cardsLeftInDeck;
    Label cardsLeftInDeckLabel;

    public PackedScene cardScene;

    [Export]
    Texture2D emptyDeckTexture;
    [Export]
    Texture2D fullDeckTexture;

    TextureRect textureRect;

    [Signal]
    public delegate void OnCardDrawEventHandler(CardDeck source, CardResource cardResource, CardField cardfield);

    public override void _Ready()
    {
        base._Ready();
        drawLeftLabel = (Label)GetNode("DrawsLeft");
        cardsLeftInDeckLabel = (Label)GetNode("CardsLeft");
        drawLeftLabel.Text = (maxDrawCount - currentDraw).ToString();
        cardsLeftInDeck = cards.Count;
        cardsLeftInDeckLabel.Text = cardsLeftInDeck.ToString();
        textureRect = (TextureRect)GetNode("TextureRect");
        textureRect.Texture = fullDeckTexture;

        UpdateCardUI();
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
                AddCard(cr, true);
            }
            else AddCard(cr, false);
        }
    }

    public virtual void AddCard(CardResource card, bool fisrt)
    {
        if (fisrt)
        {
            cards.AddFirst(card);
        }
        else
        {
            cards.AddLast(card);
        }
        UpdateCardUI();
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

    public virtual void DrawCard(CardField target)
    {
        if (target.HasSpace())
        {
            CardResource card = (CardResource)cards.First.Value.Duplicate();
            EmitSignal(SignalName.OnCardDraw, this, card, target);
            GD.Print("wyciagam karte");
            // Card cardInstance = (Card)cardScene.Instantiate();
            // cardInstance.CreateCard(card);
            // target.AddCard(cardInstance);

            ChangeCurrentDraw(1);
            cards.RemoveFirst();
            UpdateCardUI();
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

    public virtual void UpdateCardUI()
    {
        cardsLeftInDeck = cards.Count;
        if (cardsLeftInDeck <= 0)
        {
            textureRect.Texture = emptyDeckTexture;
        }
        else if (!textureRect.Texture.Equals(fullDeckTexture))
        {
            textureRect.Texture = fullDeckTexture;
        }
        cardsLeftInDeckLabel.Text = cardsLeftInDeck.ToString();
    }

}
