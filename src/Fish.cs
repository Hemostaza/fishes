using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public partial class Fish : Entity
{
    private double hunger;
    RandomNumberGenerator rng = new RandomNumberGenerator();

    [Export]
    FishData fishData;

    bool hungry;
    bool starving;

    //lista jedzenia:
    Node tank;

    SpriteFrames sprites;

    public bool isSpirteFlipped;

    public Vector2 lastDirection;

    public void SetFishData(FishData fishData){
        this.fishData = fishData;
    }

    public override void _Ready()
    {
        base._Ready();
        isSpirteFlipped = false;
        Scale = fishData.spawnSize();
        animatedSprite2D.SpriteFrames = fishData.sprites;
        tank = (Tank) GetParent();
        hunger = -1;//rng.RandiRange(1,3);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if(hunger>0){
            hunger-=delta;
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
        return hunger<0 ? true : false;
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
        //hunger+=rng.RandiRange(2,5);
        if(Scale.X<fishData.maxSize){
            Scale += new Vector2(0.1f,0.1f);
        }
        //getbiger change FishData;
    }

    public bool DropCoin(){
        if(fishData.coinScene!=null){
            Coin coin = (Coin) fishData.coinScene.Instantiate();
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
    void AleHecaNieMaMnie(Food food){
        
    }

    public void OnAnimationFinished(){
        // if(animatedSprite2D.Animation=="turn"){
        //     if(isSpirteFlipped!=animatedSprite2D.FlipH){
        //         animatedSprite2D.FlipH = isSpirteFlipped;
        //     }
        // }
        if(animatedSprite2D.Animation!="idle"){
            animatedSprite2D.Play("idle");
        }
        if(isSpirteFlipped!=animatedSprite2D.FlipH){
            animatedSprite2D.FlipH = isSpirteFlipped;
        }
    }

    public void TurnSide(float X, float oldX){
        //if (animatedSprite2D.Animation == "idle"){
            if (!isSpirteFlipped && X > 0 && oldX < 0){
                animatedSprite2D.Play("turn");
                //GD.Print("turn right");
                isSpirteFlipped = true;
            }
            if (isSpirteFlipped && X < 0 && oldX > 0){
                animatedSprite2D.Play("turn");
                //GD.Print("turn left");
                isSpirteFlipped = false;
            }
       // }
        //GD.Print(flipped);
    }


}
