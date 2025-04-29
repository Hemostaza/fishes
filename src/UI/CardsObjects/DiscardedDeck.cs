using Godot;
using System;

public partial class DiscardedDeck : CardDeck
{
    [Export]
    Card cardOnTop;

    public override void UpdateCardUI()
    {
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
    }

}
