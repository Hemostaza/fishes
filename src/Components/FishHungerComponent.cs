using Godot;
using Godot.Collections;
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

    String favFood;
    float favBonus;

    String foodGroup;

    public void InitComponent(Fish fish){
        FishData data = fish.fishData;
        this.fish = fish;
        fishSprites = fish.GetAnimatedSprite2D();

        favFood = data.favroiteFoodName;

        foodGroup = data.carnivore ? "fishFood" : "food";
        //fishfoodmaxsize mozna dodac?

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

    public bool FindFood(){
        return GetTree().GetNodesInGroup(foodGroup).Count > 0;
    }

    public Food TargetedFood(){

        Array<Node> foods = GetTree().GetNodesInGroup(foodGroup);

        if(foods.Count>0){
            Food closestFood = (Food) foods[0];
            foreach (Food food in GetTree().GetNodesInGroup(foodGroup)){
                float distance = fish.Position.DistanceTo(food.Position);
                float distanceToClosestFood = fish.Position.DistanceTo(closestFood.Position);
                if(distance < distanceToClosestFood){
                    closestFood = food;
                }
            }
            return closestFood;
        }
        return null;
    }

    public bool isHungry(){
        double halfMeter = hungerMeter/2;
        return hunger<=halfMeter ? true : false;
    }

    public void Eated(String foodName, float nutrition){
        hunger = 0;
        starving = false;
        fishSprites.Modulate = new Color(1,1,1,1);
        hunger += nutrition;
        if(foodName.Equals(favFood)){
            hunger*=2;
        }
        //hunger+=2;//rng.RandiRange(2,5); //+ selectedFoodNutrition
        if(fish.Scale.X<maxSize){
            fish.Scale += new Vector2(0.1f,0.1f);
        }
        if(fish.Scale.X>2){
            fish.RemoveFromGroup("fishFood");
        }
    }
    public double GetHunger(){
        return hunger;
    }

}
