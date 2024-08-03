using Godot;
using System;

public partial class FoodData : Resource
{
    [Export]
    public String Name;

    [Export]
    public float nutrition;
    [Export]
    public int price;
    [Export]
    public SpriteFrames spriteFrames;

}
