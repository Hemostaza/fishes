using Godot;
using System;

public partial class PlayerHandCardField : CardField
{
    [Export]
    CardField target;
    [Export]
    Control btnCOntainer;
    Button playBtn;
    TextureButton discardBtn;

    Card focusedCard;

    float xOffset = 125;

    [Signal]
    public delegate void discardedFromHandEventHandler(Card card);

    public override void _Ready()
    {
        base._Ready();
        maxCards = 5;
        playBtn = (Button)btnCOntainer.GetChild(0);
        discardBtn = (TextureButton)btnCOntainer.GetChild(1);
        playBtn.Pressed += PlayCard;
        discardBtn.Pressed += DiscardCard;
    }

    public override void AddCard(Card card)
    {
        base.AddCard(card);
    }

    public override void OnMouseEntered(Card card)
    {
        base.OnMouseEntered(card);
        card.Position -= new Vector2(0, 20);
        card.ZIndex += 100;
        focusedCard = card;
        btnCOntainer.GetParent().RemoveChild(btnCOntainer);

        card.AddChild(btnCOntainer);
        btnCOntainer.Position = new Vector2(0, 0);
    }
    public override void OnMouseExited(Card card)
    {
        base.OnMouseExited(card);
        if (focusedCard != null)
        {
            card.Position += new Vector2(0, 20);
            card.ZIndex -= 100;
            focusedCard = null;
            btnCOntainer.GetParent().RemoveChild(btnCOntainer);
            GetParent().AddChild(btnCOntainer);
            btnCOntainer.Position = new Vector2(10000, 10000);
        }
    }

    public void DiscardCard()
    {
        EmitSignal(SignalName.discardedFromHand, focusedCard);
    }

    public override void CardPressed(Card card)
    {
        //base.CardPressed(card);
    }

    public void PlayCard()
    {
        Card cardToPlayed = focusedCard;
        RemoveCard(cardToPlayed);
        target.AddCard(cardToPlayed);
    }

    public void DisableButtons(bool value)
    {
        focusedCard = null;
        playBtn.Disabled = value;
        discardBtn.Disabled = value;
    }

    public override void Disable(bool disable, bool setGray)
    {
        DisableButtons(disable);
        OnMouseExited(focusedCard);
        focusedCard = null;
        foreach (Card c in GetChild(0).GetChildren())
        {
            c.Disabled(disable);
            if(setGray)c.SetGreyed(disable);
        }
    }



}
