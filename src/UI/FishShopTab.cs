using Godot;
using System;

public partial class FishShopTab : Panel
{
    //FishData[] fishDatas;
    [Export]
    PackedScene buttonScene;
    [Export]
    GridContainer buttonGrid;

    FishDataResources fishDataResources;
    PlayerStatus playerStatus;

    public override void _Ready()
    {
        base._Ready();
        fishDataResources = FishDataResources.Instance;
        playerStatus = PlayerStatus.Instance;
        InsertButtons();
    }

    public void InsertButtons(){
        //GD.Print(fishDataResources.fishDatasByValue[0]);
        if(buttonScene!=null){
            foreach(var fish in fishDataResources.GetFishDatas()){
                FishData fishData = fish;
                GD.Print(fishData);
                FishShopButton instance = (FishShopButton) buttonScene.Instantiate();
                instance.SetButtonData(fishData.Name,fishData.value,fishData.icon);
                buttonGrid.AddChild(instance);
            }
        }
        //foreach(FishData fish in fishDataResources.GetFishDatas()){
            // SetButtonData(Texture2D fishIco, String value, String name){
            // playerStatus.IsFishUnlocked(fish.Name);
            // String fishName;
            // fishName = "Default";
            // GD.Print(fishDataResources.GetFishDataByName(fishName));
            // GD.Print(playerStatus.IsFishUnlocked(fishName));
            //if(button!=null){
                // FishShopButton instance = button.Instantiate<FishShopButton>();
                // instance.SetButtonData(fish.icon,fish.value.ToString(),fish.Name);
                // buttonGrid.AddChild(instance);
            //}
        //}
    }
}
