using Godot;
using System;

public partial class PlayerHandCardField : CardField
{
    Card activeCard;
    [Export]
    CardField target;
    [Export]
    Button discardBtn;
    public override void _Ready()
    {
        base._Ready();
        //SetContainer(GetChild(1));
        //discardBtn.Pressed += () => DiscardCard(activeCard);
    }

    public override void AddCard(Card card)
    {
        base.AddCard(card);
        //card.MouseEntered += () => OnMouseEnter(card);
        //card.MouseExited += () => OnMouseExit(card);
    }

    public void OnMouseEnter(Card card)
    {
        SetActiveCard(card);
        //activeCard.Position += new Vector2(0,-20);
    }

    void SetActiveCard(Card card)
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
        discardBtn.Position = card.Position;
        SetActiveCard(card);
    }
    public override void OnMouseExited(Card card)
    {
        base.OnMouseExited(card);
        
        discardBtn.Position = new Vector2(100000,100000);
        SetActiveCard(null);
    }

    public override void _GuiInput(InputEvent @event)
    {
        base._GuiInput(@event);
    }


    public override void CardPressed(Card card)
    {
        RemoveCard(card);
        target.AddCard(card);
        //card.ChangeCardField(target);
        //SendCard(card, target);
    }


}
