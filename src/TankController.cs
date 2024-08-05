using Godot;
using Godot.Collections;
using System;

public partial class TankController : Node
{
    public static TankController Instance { get; private set; }

    Node2D tank;
    PackedScene fishScene;

    PackedScene foodScene;

    Fish activeFish;

    RandomNumberGenerator rng; 
    public override void _Ready()
    {
        Instance = this;
        fishScene = ResourceLoader.Load<PackedScene>("res://Scenes/Fish.tscn");
        foodScene = ResourceLoader.Load<PackedScene>("res://Scenes/Food.tscn");

        rng = new RandomNumberGenerator();
        //GD.Print("FishScene");
        tank = (Node2D) GetNode("/root/CHUJ/Tank");
    }

    public Node2D GetTank(){
        return tank;
    }

    public void SpawnFish(int index)
    {
        if(fishScene!=null){

            Fish instance = (Fish) fishScene.Instantiate();

            Vector2 spawnPos = new Vector2(rng.RandfRange(10,710),0);

            // fishName = rng.RandiRange(0,2)>1 ? "Default" : "Default1";

            FishData fishData = FishDataResources.Instance.GetFishDataByIndex(index);
            //instance.SetFishData(fishData);
            instance.SpawnFish(fishData);
            instance.Position = spawnPos;
            tank.AddChild(instance);
            
            //GD.Print("Try to spawn fish "+fishData.Name );
        }
    }
    public  void SpawnFood(string Name){
        //GD.Print("spawn tank");
        if(GetFoodCountInTank() < PlayerStatus.Instance.GetMaxFoodCount()){
            //index = FoodDataResources.Instance.GetFoodByIndex();
            if(foodScene!=null){
                Food instance = (Food) foodScene.Instantiate();
                FoodData foodData = FoodDataResources.Instance.GetFoodDataByName(Name);
                instance.SetFoodData(foodData);
                Vector2 mousePos = tank.GetGlobalMousePosition();
                instance.Position = mousePos;
                tank.AddChild(instance);
            }
        }
    }
    public int GetFoodCountInTank(){
        Array<Node> foods =  GetTree().GetNodesInGroup("food");
        return foods.Count;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if(@event is InputEventMouseButton mouseButton){
            
            if(mouseButton.Pressed){
                SpawnFood("defaultFood");
            }
        }
    }

    public Fish GetActiveFish(){
        return activeFish;
    }
    public void SetActiveFish(Fish fish){
        if(activeFish!=null){
            activeFish.ZIndex -= 10;
        }
        activeFish = fish;
        activeFish.ZIndex += 10;
        //emit signal on tab?
    }

    public RandomNumberGenerator GetRandomNumberGenerator(){
        return rng;
    }

}
