using Godot;
using System;

public partial class CarsInPlayField : CardField
{
    public override void OnMouseEntered(Card card)
    {
        base.OnMouseEntered(card);
        card.Position += new Vector2(0, 20);
    }
    public override void OnMouseExited(Card card)
    {
        base.OnMouseExited(card);
        card.Position -= new Vector2(0, 20);
    }
}
