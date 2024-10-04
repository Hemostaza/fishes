using Godot;
using System;

public partial class Food : Area2D
{
    Tween tween;
    Sprite2D sprite2D;

    FoodData foodData;
    AnimationPlayer animationPlayer;
    public override void _Ready()
    {
        animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
        animationPlayer.Play("float");
        base._Ready();
        //GetTree().CallGroup("fish", "FoodInTank");
        tween = GetTree().CreateTween();
        tween.BindNode(this);
        tween.TweenProperty(this, "modulate", Colors.Turquoise, 2.0f);
        tween.TweenProperty(this, "scale", Vector2.Zero, 2.0f);
        tween.TweenCallback(Callable.From(QueueFree));
        
    }

    public void SetFoodData(FoodData foodData){
        this.foodData = foodData;
        sprite2D = (Sprite2D)GetNode("Sprite2D");
        sprite2D.Texture = foodData.sprites;
    }

    public FoodData GetFoodData(){
        return foodData;
    }
    public override void _PhysicsProcess(double delta)
    {
        if(Position.Y < 440){
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
