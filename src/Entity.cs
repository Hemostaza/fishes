using Godot;
using System;

public partial class Entity : Area2D
{
    [Export]
    public AnimatedSprite2D animatedSprite2D;

    public override void _Ready()
    {
        base._Ready();
        animatedSprite2D.Play("idle");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    public AnimatedSprite2D GetAnimatedSprite2D(){
        return animatedSprite2D;
    }
}
