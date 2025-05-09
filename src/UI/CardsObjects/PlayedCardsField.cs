using Godot;
using System;

public partial class PlayedCardsField : CardField
{
    [Export]
    CardField target;

    Vector2 offset = new Vector2(125 / 8, 150 / 8);

    Control duplicate;
    bool mouseIn;

    public override void CardPressed(Card card)
    {
        if(target.HasSpace()){
            OnMouseExited(card);
            RemoveCard(card);
            target.AddCard(card);
        }
    }

    public override void AddCard(Card card)
    {
        base.AddCard(card);
        card.MouseClickCardHand += CardPressed;
    }

    public override void RemoveCard(Card card)
    {
        base.RemoveCard(card);
        card.MouseClickCardHand -= CardPressed;
    }



    public override void OnMouseEntered(Card card)
    {
        base.OnMouseEntered(card);
        duplicate = (Control)card.Duplicate();
        duplicate.MouseFilter = MouseFilterEnum.Ignore;
        GetParent().AddChild(duplicate);
        duplicate.GlobalPosition = card.GlobalPosition - offset;
        mouseIn = true;
    }
    public override void OnMouseExited(Card card)
    {
        base.OnMouseExited(card);
        duplicate.Free();
        mouseIn = false;
    }


}
