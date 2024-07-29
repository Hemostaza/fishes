using Godot;
using System;

public partial class FishShopTab : Panel
{
    //FishData[] fishDatas;
    // [Export]
    // PackedScene button;
    // [Export]
    // GridContainer buttonGrid;

    public override void _Ready()
    {
        base._Ready();
        InsertButtons();
    }

    public void InsertButtons(){
        // GD.Print(ResourcesLoader.fishDatasByValue[0]);
        // foreach(FishData fish in ResourcesLoader.fishDatasByValue){
        //     GD.Print(fish);
        //     //if(button!=null){
        //         FishShopButton instance = button.Instantiate<FishShopButton>();
        //         instance.SetButtonData(fish.icon,fish.value.ToString(),fish.Name);
        //         buttonGrid.AddChild(instance);
        //     //}
        // }
    }
}
