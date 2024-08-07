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

    int fishIndex;
    Node2D tank;

    bool fishUnlocked;
    public override void _Ready()
    {
        tank = TankController.Instance.GetTank();
        button.ButtonDown += OnClick;
        //TestButtonData();
        base._Ready();
    }

    public void TestButtonData(){
        FishData fish = FishDataResources.Instance.GetFishDataByIndex(1);
        //button.Icon = fish.icon;
        label.Text = fish.value.ToString();
        //fishName = fish.Name;
    }

    public void SetButtonData(String name,int index, int value, Texture2D fishIco){
        button.Icon = fishIco;
        label.Text = value.ToString();
        fishIndex = index;
        //fishName = name;
        button.Disabled = PlayerStatus.Instance.IsFishLocked(index);
    }

    public void SetButtonData(int index,int value, Texture2D fishIco){
        button.Icon = fishIco;
        label.Text = value.ToString();
        fishIndex = index;
        button.Disabled = PlayerStatus.Instance.IsFishLocked(index);
    }

    void OnClick(){
        //tank.SpawnFish(fishName);
        TankController.Instance.SpawnFish(fishIndex);
        //GD.Print("Klikniete"); spawn fishdata xD spawn fish by name: spawnFish.
    }
    public void UnlockButton(){
        button.Disabled = false;
    }

}
