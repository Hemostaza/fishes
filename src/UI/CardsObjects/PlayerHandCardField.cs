using Godot;
using System;

public partial class PlayerHandCardField : CardField
{
    Card activeCard;
    [Export]
    CardField target;
    [Export]
    Control btnCOntainer;
    Button playBtn;
    Button discardBtn;

    Card focusedCard;

    [Signal]
    public delegate void discardedFromHandEventHandler(Card card);

    public override void _Ready()
    {
        base._Ready();
        playBtn = (Button)btnCOntainer.GetChild(0);
        discardBtn = (Button)btnCOntainer.GetChild(1);
        playBtn.Pressed += PlayCard;
        discardBtn.Pressed += DiscardCard;
    }

    public override void AddCard(Card card)
    {
        base.AddCard(card);
    }

    public void SetActiveCard(Card card)
    {
        if (activeCard != null)
            activeCard.Position += new Vector2(0, 20);
        activeCard = card;
        if (card != null)
            activeCard.Position -= new Vector2(0, 20);
    }

    public override void OnMouseEntered(Card card)
    {
        base.OnMouseEntered(card);
        card.Position -= new Vector2(0, 20);
        focusedCard = card;
        btnCOntainer.GetParent().RemoveChild(btnCOntainer);
        card.AddChild(btnCOntainer);//.Position = card.Position;
        
        btnCOntainer.Position = new Vector2(0,0);
    }
    public override void OnMouseExited(Card card)
    {
        base.OnMouseExited(card);
        card.Position += new Vector2(0, 20);
        focusedCard = null;
        btnCOntainer.GetParent().RemoveChild(btnCOntainer);
        GetParent().AddChild(btnCOntainer);
        btnCOntainer.Position = new Vector2(10000,10000);
    }

    public void DiscardCard()
    {
        EmitSignal(SignalName.discardedFromHand, focusedCard);
        SetActiveCard(null);
    }

    public void PlayCard()
    {
        Card cardToPlayed = focusedCard;
        RemoveCard(cardToPlayed);
        target.AddCard(cardToPlayed);
    }

    public void DisableButtons(bool value){
        playBtn.Disabled = value;
        discardBtn.Disabled = value;
    }


}
