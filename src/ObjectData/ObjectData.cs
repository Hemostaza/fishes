using Godot;
using System;
[GlobalClass]
public partial class ObjectData : Resource
{   
    
    [Export]
    public String Name;

    [Export]
    public Texture2D sprites;

    [Export]
    public int value;
    [Export]
    public Vector2 spriteSize = new Vector2(32,32);
    [Export]
    public int spritesCount = 4;

    public override string ToString()
    {
        return "\n Name: "+ Name +"\n Value: "+value;
    }

}
