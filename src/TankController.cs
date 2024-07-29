using Godot;
using Godot.Collections;
using System;

public partial class TankController : Node
{
    public static TankController Instance { get; private set; }

    Tank tank;
    PackedScene fishScene;

    PackedScene foodScene;

    PackedScene mouseClick;

    public override void _Ready()
    {
        Instance = this;
        fishScene = ResourceLoader.Load<PackedScene>("res://Scenes/Fish.tscn");
        foodScene = ResourceLoader.Load<PackedScene>("res://Scenes/Food.tscn");

        //GD.Print("FishScene");
        tank = (Tank) GetNode("/root/CHUJ/Tank");
    }

    public Tank GetTank(){
        return tank;
    }

    public void SpawnFish(string fishName)
    {
        if(fishScene!=null){
            Fish instance = (Fish) fishScene.Instantiate();
            FishData fishData = FishDataResources.Instance.GetFishDataByName(fishName);
            instance.SetFishData(fishData);
            tank.AddChild(instance);
            
            //GD.Print("Try to spawn fish "+fishData.Name );
        }
    }
    public  void SpawnFood(int index){
        //GD.Print("spawn tank");
        if(GetFoodCountInTank() < PlayerStatus.Instance.GetMaxFoodCount()){
            //index = FoodDataResources.Instance.GetFoodByIndex();
            if(foodScene!=null){
                Food instance = (Food) foodScene.Instantiate();
                Vector2 mousePos = tank.GetLocalMousePosition();
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
                SpawnFood(0);
            }
        }
    }

}
