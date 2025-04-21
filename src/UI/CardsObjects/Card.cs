using System;
using Godot;
using Godot.Collections;
public partial class Card : Control
{
    [Export]
    CardResource cardResource;

    [Export]
    RichTextLabel cardNameLabel;
    [Export]
    Label cardPowerLabel;
    [Export]
    RichTextLabel cardDescriptionLabel;
    [Export]
    TextureRect image;

    bool mouseIn;
    CardField field;

    //int maxTurnCount=0;
    int turnLived;
    bool disabled;

    public void CreateCard(CardResource resource)
    {
        cardResource = resource;
        image.Texture = resource.GetTexture2D();
        cardNameLabel.Text = resource.GetCardName();
        cardPowerLabel.Text = resource.GetCardStrenght().ToString();
        cardDescriptionLabel.Text = resource.GetCardDescription();
        turnLived=0;
        disabled = false;
        this.MouseEntered += OnMouseEntered;
        this.MouseExited += OnMouseExited;
    }

    public CardResource GetResource()
    {
        return cardResource;
    }
    public override void _Ready()
    {
        base._Ready();
    }

    public override string ToString()
    {
        return cardResource.ToString();
    }

    public void OnPlayed(CardController cardController){
        //jak zagrana karta zostaje:   
        cardResource.OnPlayed(cardController, this);
    }

    public void DoAction(CardController cardController)
    {
        //Ruszaj sie lewo prawo albo sraj 
        cardResource.Action();
    }
    public void EndTrun(CardController cardController){
        cardResource.EndTrun();
    }

    void OnMouseEntered()
    {
        mouseIn = true;
        CardField cardField = (CardField)GetParent().GetParent();
        cardField.OnMouseEntered(this);
    }

    void OnMouseExited()
    {
        mouseIn = false;
        CardField cardField = (CardField)GetParent().GetParent();
        cardField.OnMouseExited(this);
    }

    public override void _GuiInput(InputEvent @event)
    {
        base._GuiInput(@event);
        if (@event is InputEventMouseButton mouseButton && mouseIn)
        {

            if (mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left && disabled==false)
            {
                //GD.Print(field.Name);
                CardField cardField = (CardField)GetParent().GetParent();
                GD.Print(cardField.Name);
                cardField.CardPressed(this);
            }
        }
    }

    public void Disable(bool val){
        disabled = val;
    }

}
