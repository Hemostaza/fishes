using System;
using Godot;
using Godot.Collections;
[GlobalClass]
public abstract partial class CardResource : Resource
{
    [Export]
    String cardName;
    [Export]
    int cardStrenght;
    [Export]
    String cardDescription;
    [Export]
    Texture2D texture;
    [Export]
    int maxTurnCount;

    public CardController cardController;
    public Card parent;
    public int GetCardStrenght(){
        return cardStrenght;
    }
    public String GetCardName(){
        return cardName;
    }
    public String GetCardDescription(){
        return cardDescription;
    }
    public Texture2D GetTexture2D(){
        return texture;
    }

    public int GetMaxTurnCount(){
        return maxTurnCount;
    }
    public abstract void Action(CardController cardController,Card parent);

    public abstract void EndTrun(CardController cardController, Card parent);
    public virtual void OnPlayed(CardController cardController, Card parent){
        
        this.cardController = cardController;
        this.parent = parent;
    }
    public override string ToString()
    {
        return cardName+" "+cardStrenght+" "+cardDescription;
    }

}
