using Godot;
using System;

public partial class FishHealthComponent : Node
{
    double maxHelath;
    double heatlh;
    bool chargingHealth;
    double healthRegen;

    Fish fish;

    public void InitComponent(Fish fish){
        this.fish = fish;
        FishData data = fish.fishData;
        maxHelath = data.maxHealth;
        heatlh = maxHelath;
        chargingHealth = false;
        healthRegen = data.healthRegen;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Healing(delta);
    }

    void Healing(double delta){
        if(chargingHealth==true){

            heatlh += healthRegen * delta * PlayerStatus.Instance.GetFishHealthRegeneration();
            if(heatlh>=maxHelath){
                heatlh = maxHelath;
                chargingHealth=false;
                //particle.QueueFree();
            }
        }
    }

    public void GetHit(float hit){
        GD.Print("GET HIT"+hit);
        if(heatlh >= 0 && !chargingHealth){
            heatlh -= hit;
            CheckHealth();
            fish.StartFlash(new Color(100,100,100,100));
        }
    }
    void CheckHealth(){
        if(heatlh<=0){
            fish.DropCoin();
            chargingHealth = true;
            // particle = new AnimatedSprite2D
            // {
            //     SpriteFrames = particleFrames
            // };
            // AddChild(particle);
            // particle.Play();
        }
    }

    public double GetHealth(){
        return heatlh;
    }
}
