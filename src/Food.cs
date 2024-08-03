using Godot;
using System;

public partial class Food : Area2D
{
    Tween tween;
    [Export]
    AnimatedSprite2D animatedSprite2D;

    FoodData foodData;
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

    void InitFood(){
        animatedSprite2D.SpriteFrames = foodData.spriteFrames;
    }

    public void SetFoodData(FoodData foodData){
        this.foodData = foodData;
        InitFood();
    }

    public FoodData GetFoodData(){
        return foodData;
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
