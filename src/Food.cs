using Godot;
using System;

public partial class Food : Area2D
{
    Tween tween;
    
    public override void _Ready()
    {
        base._Ready();
        //GetTree().CallGroup("fish", "FoodInTank");
        tween = GetTree().CreateTween();
        tween.BindNode(this);
        tween.TweenProperty(this, "modulate", Colors.Turquoise, 2.0f);
        tween.TweenProperty(this, "scale", Vector2.Zero, 2.0f);
        tween.TweenCallback(Callable.From(QueueFree));

    }
    public override void _PhysicsProcess(double delta)
    {
        if(Position.Y < 160){
            Position += Vector2.Down;
        }
        base._PhysicsProcess(delta);
    }

    public override void _ExitTree()
    {
        GetTree().CallGroup("fish", "AleHecaNieMaMnie",this);
        base._ExitTree();
    }
}
