using Godot;
using System;

public partial class NewCard : Node
{
    
    String name;
    int cardStrenght;
    String desc;
    Texture2D img;

    public virtual void SetCardInfo(String name, int str, String desc){
        this.name = name;
        cardStrenght = str;
        this.desc = desc;
    }

    public virtual String GetNewCardName(){
        return name;
    }

    public virtual void Action(NewCardObject parent){
        GD.Print("chuj");
    }

}
