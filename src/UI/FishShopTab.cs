using Godot;
using Godot.Collections;
using System;

public partial class FishShopTab : Control
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
        //wyjebac tablice i uzyc GetFishDataByIndex() z fish data resources
        if(buttonScene!=null){
            FishData[] fishDatas = fishDataResources.GetFishDatas();
            for(int i = 0; i<fishDatas.Length;i++){
                FishShopButton instance = (FishShopButton) buttonScene.Instantiate();
                AtlasTexture atlasTexture = new AtlasTexture();
                atlasTexture.Atlas = fishDatas[i].sprites;
                atlasTexture.Region = new Rect2(new Vector2(0, 0), new Vector2(32,32));
                instance.SetButtonData(i,fishDatas[i].value,atlasTexture);
                fishShopButtons.Add(instance);
                buttonGrid.AddChild(instance);
            }
        }
    }
    void UnlockButton(int index){
        fishShopButtons[index].UnlockButton();
    }
}
