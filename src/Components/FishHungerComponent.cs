using Godot;
using System;

public partial class FishHungerComponent : Node
{
    double hunger;
    double hungerMeter;
    float hungerSpeed;
    bool starving;
    AnimatedSprite2D fishSprites;

    float maxSize;
    Fish fish;

    Food favFood;

    public void InitComponent(Fish fish){
        FishData data = fish.fishData;
        this.fish = fish;
        fishSprites = fish.GetAnimatedSprite2D();
        hungerSpeed = data.hungerSpeed;
        hungerMeter = data.hungerMeter;
        hunger = hungerMeter;

        maxSize = data.maxSize;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
            if(!starving){
            hunger-=delta * hungerSpeed;
            }else{
            hunger -= delta;
            }
            if(hunger<0){
                if(!starving){
                    fishSprites.Modulate = new Color(0.7f,0.9f,0.7f,1);
                    //Change sprites;
                }
                starving=true;
            }
            if(hunger>=hungerMeter){
                hunger = hungerMeter;
            }
    }

    public bool isHungry(){
        double halfMeter = hungerMeter/2;
        return hunger<=halfMeter ? true : false;
    }

    public void Eated(){
        hunger = 0;
        starving = false;
        fishSprites.Modulate = new Color(1,1,1,1);
        hunger+=2;//rng.RandiRange(2,5); //+ selectedFoodNutrition
        if(fish.Scale.X<maxSize){
            fish.Scale += new Vector2(0.1f,0.1f);
        }
    }

    public double GetHunger(){
        return hunger;
    }

}
