using Godot;
using Godot.Collections;
using System;

public partial class ShopButtonFish : ShopButton
{
    [Export]
    Button button;
    [Export]
    Label label;

    int value;

    int index;

    bool fishUnlocked;

    public override void SetButtonData(int index,int value, Texture2D icon){
        button.Disabled = PlayerStatus.Instance.IsFishLocked(index);
    }

    public override void OnClickEffect(int index){
        TankController.Instance.SpawnFish(index);
    }

}
