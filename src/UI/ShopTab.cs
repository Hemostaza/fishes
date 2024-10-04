using Godot;
using Godot.Collections;
using System;

public partial class ShopTab : Control
{
    [Export]
    PackedScene buttonScene;
    [Export]
    GridContainer buttonGrid;

    FishDataResources fishDataResources;
    PlayerStatus playerStatus;

    Array<ShopButton> ShopButtons;

    public override void _Ready()
    {
        base._Ready();
        fishDataResources = FishDataResources.Instance;
        playerStatus = PlayerStatus.Instance;
        playerStatus.onFishUnlocked += UnlockButton;
        ShopButtons = new Array<ShopButton>();
        InsertButtons();
    }

    void InsertButtons(){
        //GD.Print(fishDataResources.fishDatasByValue[0]);
        //wyjebac tablice i uzyc GetFishDataByIndex() z fish data resources
        if(buttonScene!=null){
            FishData[] fishDatas = fishDataResources.GetFishDatas();
            for(int i = 0; i<fishDatas.Length;i++){
                ShopButton instance = (ShopButton) buttonScene.Instantiate();
                //instance.SetButtonData(i,fishDatas[i].value,fishDatas[i].sprites);
                ShopButtons.Add(instance);
                buttonGrid.AddChild(instance);
            }
        }
    }
    void UnlockButton(int index){
        ShopButtons[index].UnlockButton();
    }
}
