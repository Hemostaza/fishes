using Godot;
using System;
using System.Collections.Generic;

public partial class Tank : AnimatedSprite2D
{
    Vector2 mousePos;

    [Export]
    PackedScene food;
    [Export]
    PackedScene fish;
    [Export]
    FishData fishData;
    
    [Signal]
    public delegate void foodSpawnedEventHandler();

    Food instance;
    Area2D tankArea;

    [Export]
    CollisionShape2D collider;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        // if(Input.IsActionJustPressed("fire")){
        //     //SpawnFood();
        // }
        // if(Input.IsActionJustPressed("jump")){
        //     SpawnFish();
        // }

    }

    // public void SpawnFood(){

    //     if(food!=null){
    //         instance = (Food) food.Instantiate();
    //         mousePos = GetLocalMousePosition();
    //         instance.Position = mousePos;
    //         AddChild(instance);
    //     }
    // }
    // public void SpawnFish(String name){

    //     if(fish!=null){
    //         Fish inst = (Fish) fish.Instantiate();
    //         mousePos = GetLocalMousePosition();
    //         fishData = FishDataResources.Instance.GetFishDataByName(name);
    //         inst.SetFishData(fishData);
    //         inst.Position = mousePos;
    //         AddChild(inst);
    //     }
    // }

    
    // void click(Viewport viewport, InputEvent @event, int shapeIdx){
    //     if(@event is InputEventMouseButton mouseButton){
            
    //         if(mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left){
    //             viewport.SetInputAsHandled();
    //             TankController.Instance.SpawnFood(0);
    //         }
    //     }
    // }
    

}
