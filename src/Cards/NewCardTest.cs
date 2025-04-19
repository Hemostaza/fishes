using Godot;
using System;

public partial class NewCardTest : NewCard
{

    CardController cr;
    
    CardField playedField;

    public override void Action(NewCardObject parent)
    {
        CardField cardField = (CardField) parent.GetParent().GetParent();
        base.Action(parent);
        GD.Print(cardField);
    }

}
