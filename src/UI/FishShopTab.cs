using Godot;
using Godot.Collections;
using System;

public partial class FishShopTab : Panel
{
    [Export]
    PackedScene buttonScene;
    [Export]
    GridContainer buttonGrid;

    FishDataResources fishDataResources;
    PlayerStatus playerStatus;

    Array<FishShopButton> fishShopButtons;

    public override void _Ready()
    {
        base._Ready();
        fishDataResources = FishDataResources.Instance;
        playerStatus = PlayerStatus.Instance;
        playerStatus.onFishUnlocked += UnlockButton;
        fishShopButtons = new Array<FishShopButton>();
        InsertButtons();
    }

    void InsertButtons(){
        //GD.Print(fishDataResources.fishDatasByValue[0]);
        if(buttonScene!=null){
            FishData[] fishDatas = fishDataResources.GetFishDatas();
            for(int i = 0; i<fishDatas.Length;i++){
                FishShopButton instance = (FishShopButton) buttonScene.Instantiate();
                instance.SetButtonData(i,fishDatas[i].value,fishDatas[i].icon);
                fishShopButtons.Add(instance);
                buttonGrid.AddChild(instance);
            }
        }
    }
    void UnlockButton(int index){
        fishShopButtons[index].UnlockButton();
    }
}
