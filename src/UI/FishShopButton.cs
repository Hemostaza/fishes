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
    Node tank;

    public override void _Ready()
    {
        tank = TankController.Instance.GetTank();
        button.ButtonDown += OnClick;
        TestButtonData();
        base._Ready();
    }

    public void TestButtonData(){
        FishData fish = FishDataResources.Instance.GetFishDataByIndex(1);
        button.Icon = fish.icon;
        label.Text = fish.value.ToString();
        fishName = fish.Name;
    }

    public void SetButtonData(Texture2D fishIco, String value, String name){
        button.Icon = fishIco;
        label.Text = value;
        fishName = name;
    }

    public void OnClick(){
        //tank.SpawnFish(fishName);
        TankController.Instance.SpawnFish(fishName);
        //GD.Print("Klikniete"); spawn fishdata xD spawn fish by name: spawnFish.
    }
    
}
