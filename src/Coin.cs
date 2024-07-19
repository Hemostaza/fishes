using Godot;
using System;

public partial class Coin : AnimatedSprite2D
{
    [Export]
    CoinData coinData;

    int value;

    bool onBottom;

    Tween tween;

    public void SetCoinData(CoinData coinData){
        this.coinData = coinData;
    }

    public override void _Ready()
    {
        value = coinData.value;
        onBottom = false;
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if(Position.Y < 160){
            Position += Vector2.Down;
        }else{
            tween = GetTree().CreateTween();
            tween.BindNode(this);
            tween.TweenProperty(this, "modulate", Colors.Transparent, 1.0f);
            tween.Parallel();
            tween.TweenProperty(this, "scale", Vector2.Zero, 1.0f);
            tween.TweenCallback(Callable.From(QueueFree));
        }
        base._PhysicsProcess(delta);

    }


}
