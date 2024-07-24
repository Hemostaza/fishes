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
    FishData ryba;
    
    [Signal]
    public delegate void foodSpawnedEventHandler();

    Food instance;
    Area2D tankArea;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        if(Input.IsActionJustPressed("fire")){
            //SpawnFood();
        }
        if(Input.IsActionJustPressed("jump")){
            SpawnFish();
        }

    }

    public void SpawnFood(){

        if(food!=null){
            instance = (Food) food.Instantiate();
            mousePos = GetLocalMousePosition();
            instance.Position = mousePos;
            AddChild(instance);
        }
    }
    public void SpawnFish(){

        if(fish!=null){
            Fish inst = (Fish) fish.Instantiate();
            mousePos = GetLocalMousePosition();
            inst.SetFishData(ryba);
            inst.Position = mousePos;
            AddChild(inst);
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if(@event is InputEventMouseButton mouseButton){
            
            if(mouseButton.Pressed){
                SpawnFood();
                GD.Print("Niekliklo");
            }
        }
    }
    // public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
    // {
    //     base._InputEvent(viewport, @event, shapeIdx);
    //     viewport.PhysicsObjectPickingSort=true;
    //     if(@event is InputEventMouseButton mouseButton){
    //         GD.Print("Dank"+viewport);
    //     }
    // }
}
