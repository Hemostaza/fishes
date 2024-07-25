using Godot;
using Godot.Collections;
using System;

public partial class FishShopButton : Control
{
    [Export]
    Button button;
    [Export]
    Label label;

    Texture2D fishIcon;
    String fishValue;

    String fishName;

    public override void _Ready()
    {

        button.ButtonDown += OnClick;

        base._Ready();
    }

    public void SetButtonData(Texture2D fishIco, String value, String name){
        button.Icon = fishIco;
        label.Text = value;
        fishName = name;
    }

    public void OnClick(){
        //GD.Print("Klikniete"); spawn fishdata xD spawn fish by name: spawnFish.
    }
    
}
