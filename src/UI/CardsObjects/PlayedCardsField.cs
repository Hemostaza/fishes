using Godot;
using System;

public partial class PlayedCardsField : CardField
{
    [Export]
    CardField target;

    Vector2 offset = Vector2.Zero;

    Control duplicate;
    bool mouseIn;

    public override void CardPressed(Card card)
    {
        //card.ChangeCardField(target);
        //SendCard(card, target);
        RemoveCard(card);
        target.AddCard(card);

    }

    public override void OnMouseEntered(Card card)
    {
        base.OnMouseEntered(card);
        //card.ZIndex += 1000;
        duplicate = (Control)card.Duplicate();
        duplicate.MouseFilter = MouseFilterEnum.Ignore;
        GetParent().AddChild(duplicate);
        duplicate.GlobalPosition = card.GlobalPosition;
        mouseIn=true;

        // offset /= card.Size;
        // card.Scale*=2;
        // card.Position -= offset;

    }
    public override void OnMouseExited(Card card)
    {
        base.OnMouseExited(card);
        duplicate.Free();
        mouseIn=false;
        // card.ZIndex-=1000;

        // offset /= card.Size;

        // card.Scale*=0.5f;

        // card.Position += offset;
    }


}
