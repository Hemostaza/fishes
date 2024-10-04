using Godot;
using System;

public partial class FishInfoPanel : Panel
{
    TankController tankController;
    [Export]
    SpriteFrames fishSpriteFrames;
    [Export]
    Label maxHungerLabel;
    [Export]
    Label maxE2CLabel;

    public override void _Ready()
    {
        base._Ready();
        tankController = TankController.Instance;

        tankController.onActiveFishChange += ChangeActiveFish;
        
    }

    void ChangeActiveFish(Fish fish){
    }
    
}
