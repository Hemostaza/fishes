using Godot;
using System;

public partial class DiscardedDeck : CardDeck
{
    [Export]
    Card cardOnTop;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void UpdateCardUI()
    {
        //base.UpdateCardUI();
        cardsLeftInDeck = cards.Count;
        if(cardsLeftInDeck>0){
            cardOnTop.CreateCard((CardResource)cards.First.Value);
            cardOnTop.Disabled(true);
            cardOnTop.SetGreyed(true);
            cardOnTop.MouseFilter = MouseFilterEnum.Ignore;
            cardOnTop.Visible = true;
        }else{
            cardOnTop.Visible = false;
        }
        // cardsLeftInDeck = cards.Count;
        // if (cardsLeftInDeck <= 0)
        // {
        //     textureRect.Texture = emptyDeckTexture;
        // }
        // else if (!textureRect.Texture.Equals(fullDeckTexture))
        // {
        //     textureRect.Texture = fullDeckTexture;
        // }
        // cardsLeftInDeckLabel.Text = cardsLeftInDeck.ToString();
    }

}
