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

    PlayerStatus playerStatus;

    [Signal]
    public delegate void onActiveFishChangeEventHandler(Fish fish);

    public override void _Ready()
    {
        Instance = this;
        fishScene = ResourceLoader.Load<PackedScene>("res://Scenes/Fish.tscn");
        foodScene = ResourceLoader.Load<PackedScene>("res://Scenes/Food.tscn");

        playerStatus = PlayerStatus.Instance;

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
            
            GD.Print(playerStatus.GetStats("foodSelected"));

            //GD.Print("Try to spawn fish "+fishData.Name );
        }
    }
    public  void SpawnFood(int index){
        //GD.Print("spawn tank");
        
        FoodData foodData = FoodDataResources.Instance.GetFoodDataByIndex(index);

        if(playerStatus.GetStats("money").As<int>()<foodData.price){
            //dzwiek ze nie ma pijondza i błyskanie pijondzów?
            GD.Print("ni ma pijondza: "+PlayerStatus.Instance.GetStats("money").As<int>());
            return;
        }
        if(GetFoodCountInTank() < playerStatus.GetBuffValue("maxFoodCount").As<int>()){
            //index = FoodDataResources.Instance.GetFoodByIndex();
            if(foodScene!=null){
                Food instance = (Food) foodScene.Instantiate();
                instance.SetFoodData(foodData);
                Vector2 mousePos = tank.GetGlobalMousePosition();
                instance.Position = mousePos;

                int money = playerStatus.GetStats("money").As<int>();
                money-=foodData.price;
                playerStatus.ChangeStats("money",money);

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
                int fod = playerStatus.GetStats("foodSelected").As<int>();
                GD.Print(fod);
                SpawnFood(fod);
            }
        }

        if(@event is InputEventKey inputEventKey){
            if(inputEventKey.Keycode==Key.A){
                PlayerStatus.Instance.UnlockFish(1);
            }
        }
    }

    public Fish GetActiveFish(){
        return activeFish;
    }
    public void SetActiveFish(Fish fish){
        
        try{
            activeFish.ZIndex -= 10;
        }catch (Exception){
            return;
        }
        activeFish = fish;
        activeFish.ZIndex += 10;
        EmitSignal(SignalName.onActiveFishChange,activeFish);
        //emit signal on tab?
    }

    public RandomNumberGenerator GetRandomNumberGenerator(){
        return rng;
    }

}
