using Godot;
using System;

public partial class PlayedCardsField : CardField
{
    [Export]
    CardField target;
    public override void CardPressed(Card card)
    {
        //card.ChangeCardField(target);
        //SendCard(card, target);
        RemoveCard(card);
        target.AddCard(card);

    }
}
