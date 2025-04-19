using Godot;
using System;

public partial class NewCardObject : Button
{
    [Export]
    RichTextLabel cardNameLabel;
    [Export]
    Label cardPowerLabel;
    [Export]
    RichTextLabel cardDescriptionLabel;
    [Export]
    TextureRect image;

    NewCard card;
    public void CreateCard(NewCard card)
    {

        this.card = card;
        cardNameLabel.Text = card.GetNewCardName();

    }

    public override void _Ready()
    {
        base._Ready();
    }

    public override string ToString()
    {
        return card.GetNewCardName();
    }

    public void DoAction()
    {
        card.Action(this);
    }


}
