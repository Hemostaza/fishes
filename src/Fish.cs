using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Fish : Entity
{
    private double hunger;
    RandomNumberGenerator rng = new RandomNumberGenerator();

    [Export]
    FishData fishData;

    [Export]
    public PackedScene coinScene;

    bool starving;

    //lista jedzenia:
    Node tank;

    SpriteFrames sprites;

    public bool isSpirteFlipped;

    bool needFood = true;

    public Vector2 newDirection;
    public Vector2 oldDirection;

    public void SetFishData(FishData fishData){
        this.fishData = fishData;
    }

    public override void _Ready()
    {
        base._Ready();
        animatedSprite2D.AnimationFinished += OnAnimationFinished;

        if(fishData.hungerMeter==0){
            needFood = false;
        }
        //isSpirteFlipped = animatedSprite2D.FlipH;
        Scale = fishData.spawnSize();
        animatedSprite2D.SpriteFrames = fishData.sprites;
        //tank = (Tank) GetParent();
        hunger = fishData.hungerMeter;//rng.RandiRange(1,3);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if(needFood){
if(!starving){
            hunger-=delta*fishData.hungerSpeed;
        }else{
            hunger -=delta;
        }
        if(hunger<0){
            if(!starving){
                Modulate = new Color(0.7f,0.9f,0.7f,1);
                //Change sprites;
            }
            starving=true;
        }
        if(hunger<-5){

            //QueueFree();
            //Emit signal dieded
        }
        }
        
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if(Input.IsActionJustPressed("jump")){
            DropCoin();
        }
    }

    public bool isHungry(){
        int halfMeter = fishData.hungerMeter/2;
        return hunger<=halfMeter ? true : false;
    }

    public Food TargetedFood(){
        Array<Node> foods = GetTree().GetNodesInGroup("food");
        if(foods.Count>0){
            Food closestFood = (Food) foods[0];
            foreach (Food food in GetTree().GetNodesInGroup("food")){
                float distance = Position.DistanceTo(food.Position);
                if(distance<Position.DistanceTo(closestFood.Position)){
                    closestFood = food;
                }
            }
            return closestFood;
        }
        
        //GD.Print("Closest food distance: "+Position.DistanceTo(closestFood.Position));
        return null;
    }

    public void Eated(){
        hunger = 0;
        starving = false;
        Modulate = new Color(1,1,1,1);
        hunger+=rng.RandiRange(2,5); //+ selectedFoodNutrition
        if(Scale.X<fishData.maxSize){
            Scale += new Vector2(0.1f,0.1f);
        }
        //getbiger change FishData;
    }

    public bool DropCoin(){
        if(coinScene!=null){
            Coin coin = (Coin) coinScene.Instantiate();
            coin.Position = Position;
            
            if(Scale.X>=1){
                coin.SetCoinData(fishData.coinDatas[0]);
                GetParent().AddChild(coin);
                return true;  
            }
        }
        return false;
    }
    public float GetSpeed(){
        return fishData.speed;
    }

    public bool TurnSide(){

        if((newDirection.X >= 0 && oldDirection.X < 0) 
        || (newDirection.X<0 && oldDirection.X>=0)){
            animatedSprite2D.Play("turn");
            return true;
        }

        return false;
    }

    void OnAnimationFinished(){
        if(animatedSprite2D.Animation == "turn"){
            animatedSprite2D.FlipH = !animatedSprite2D.FlipH;
            animatedSprite2D.Play("swimm");
        }
        if(animatedSprite2D.Animation == "eat"){
            animatedSprite2D.Play("swimm");
        }
        
    }

    public override void OnLeftClicked(){
        base.OnLeftClicked();
        GD.Print(this);
    }

    public override string ToString()
    {
        return "Rybka: " + fishData + "\n Index: " + GetIndex() + "\n Hunger: "+hunger;
    }

    public void SetNewDirection(Vector2 direction){
        oldDirection = newDirection;
        newDirection = direction;
    }

}
