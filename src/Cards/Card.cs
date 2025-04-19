using System;
using Godot;
using Godot.Collections;
public partial class Card : Button
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

    public String name;
    int cardStrenght;
    String desc;
    Texture2D img;

    bool mouseIn;
    CardField field;

    //int maxTurnCount=0;
    int turnLived;
    bool disabled;
    public Card()
    {
        // cardNameLabel = (RichTextLabel) GetNode("Panel/NamePlaq/Label");
        // cardDescriptionLabel = (RichTextLabel) GetNode("Panel/ActionDesc");
        // cardPowerLabel = (Label) GetNode("Panel/StrPlaq/Label");
        // image = (TextureRect) GetNode("Panel/CardIcon");

        name = "BASE";
        cardStrenght = 1;
        desc = "BASE Description";

    }

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

    public void ChangeCardField(CardField field)
    {
        if (this.field != null)
            this.field.RemoveCard(this);
        this.field = field;
        this.field.AddCard(this);
    }

    public void ChangeCardFieldSort(CardField field){
        if (this.field != null)
            this.field.RemoveCard(this);
        Array<Node> cards = field.GetCardContainer().GetChildren();
        int index = 0;
        foreach (Card c in cards)
        {
            int cardStr = GetResource().GetCardStrenght();
            int cStr = c.GetResource().GetCardStrenght();
            if (cardStr < cStr)
            {
                break;
            }
            index++;
        }
        this.field = field;
        this.field.AddCard(this);
        this.field.GetCardContainer().MoveChild(this,index);
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
        cardResource.Action(cardController, this);
    }
    public void EndTrun(CardController cardController){
        cardResource.EndTrun(cardController,this);
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

    // public override void _Pressed()
    // {
    //     base._Pressed();
    //     CardField cardField = (CardField)GetParent().GetParent();
    //     cardField.CardPressed(this);
    // }

    public override void _GuiInput(InputEvent @event)
    {
        base._GuiInput(@event);
        if (@event is InputEventMouseButton mouseButton && mouseIn)
        {

            if (mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left && disabled==false)
            {
                GD.Print(field.Name);
                CardField cardField = (CardField)GetParent().GetParent();
                GD.Print(cardField.Name);
                cardField.CardPressed(this);
                // CardField cardField = (CardField)GetParent().GetParent();
                // cardField.CardPressed(this);
            }
        }
    }

    public void Disable(bool val){
        disabled = val;
    }

}
