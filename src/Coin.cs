using Godot;
using System;
using System.Diagnostics;

public partial class Coin : Area2D
{
    [Export]
    CoinData coinData;

    int value;

    bool onBottom;
    AnimatedSprite2D AnimatedSprite2D; //exportem?
    Tween tween;

    public void SetCoinData(CoinData coinData){
        this.coinData = coinData;
        AnimatedSprite2D = (AnimatedSprite2D)GetNode("Sprite");
        AnimatedSprite2D.SpriteFrames = coinData.sprites;

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
        if(Position.Y < 440){
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
    

    bool mouseIn = false;
    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventMouseButton mouseButton && mouseIn){
            GetTree().Root.SetInputAsHandled();
            if(mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left){
                GD.Print("Pijonc");
                int money = PlayerStatus.Instance.GetStats("money").As<int>();
                money += coinData.value;
                PlayerStatus.Instance.ChangeStats("money",money);
                QueueFree();
            }
        }
    }
    public override void _MouseEnter()
    {
        base._MouseEnter();
        mouseIn = true;
        GD.Print("Mouse In Area: "+Name);
    }

    public override void _MouseExit()
    {
        base._MouseExit();
        mouseIn = false;
        GD.Print("Mouse leave Area: "+Name);
    }

}
