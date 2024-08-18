using Godot;
using System;

public partial class PlayerInfoPanel : Node
{
    [Export]
    Label label;

    PlayerStatus playerStatus;
    public override void _Ready()
    {
        base._Ready();
        playerStatus = PlayerStatus.Instance;

        label.Text = playerStatus.GetStats("money").As<String>();

        playerStatus.onMoneyChange += ChangeValue;
        
    }

    void ChangeValue(int value)
    {
        label.Text = value.ToString();
    }

}
