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
    [Export]
    Material shader;

    bool mouseIn;
    CardField field;

    //int maxTurnCount=0;
    int turnLived;
    bool disabled;

    [Signal]
    public delegate void MouseEnteredCardHandEventHandler(Card card);
    [Signal]
    public delegate void MouseExitedCardHandEventHandler(Card card);
    [Signal]
    public delegate void MouseClickCardHandEventHandler(Card card);

    public void CreateCard(CardResource resource)
    {
        cardResource = resource;
        image.Texture = resource.GetTexture2D();
        cardNameLabel.Text = resource.GetCardName();
        cardPowerLabel.Text = resource.GetCardStrenght().ToString();
        cardDescriptionLabel.Text = resource.GetCardDescription();
        turnLived = 0;
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

    public void OnPlayed(CardController cardController)
    {
        //jak zagrana karta zostaje:   
        cardResource.OnPlayed(cardController, this);
    }

    public void DoAction(CardController cardController)
    {
        //Ruszaj sie lewo prawo albo sraj 
        cardResource.Action();
    }
    public void EndTrun(CardController cardController)
    {
        cardResource.EndTrun();
    }

    void OnMouseEntered()
    {
        mouseIn = true;
        EmitSignal(SignalName.MouseEnteredCardHand,this);
    }

    void OnMouseExited()
    {
        mouseIn = false;
        EmitSignal(SignalName.MouseExitedCardHand,this);
    }

    public override void _GuiInput(InputEvent @event)
    {
        base._GuiInput(@event);
        if (@event is InputEventMouseButton mouseButton && mouseIn)
        {
            if (mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left && disabled == false)
            {
                EmitSignal(SignalName.MouseClickCardHand,this);
            }
        }
    }

    public void Disabled(bool val)
    {
        disabled = val;
        if (!val)
        {
            this.MouseExited += OnMouseExited;
            this.MouseEntered += OnMouseEntered;
        }
        else
        {
            this.MouseEntered -= OnMouseEntered;
            this.MouseExited -= OnMouseExited;
        }

    }
    public void SetGreyed(bool val)
    {
        foreach (CanvasItem child in GetChildren())
        {
            if (val)
                child.Material = shader;
            else child.Material = null;
        }
    }

}
