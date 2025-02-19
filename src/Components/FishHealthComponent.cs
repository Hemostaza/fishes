using Godot;
using System;

public partial class FishHealthComponent : Node
{
    double maxHelath;
    double heatlh;
    bool chargingHealth;
    double healthRegen;

    double bonusHealthRegen;

    Fish fish;
    
    AnimatedSprite2D particle;
    [Export]
    SpriteFrames spriteFrames;

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
            bonusHealthRegen=PlayerStatus.Instance.GetBuffValue("fishHelthRegeneration").As<double>();
            heatlh += healthRegen * delta * bonusHealthRegen;
            if(heatlh>=maxHelath){
                heatlh = maxHelath;
                chargingHealth=false;
                particle.QueueFree();
            }
        }
    }

    public bool GetHit(float hit){
        //GD.Print("GET HIT"+hit);
        if(heatlh >= 0 && !chargingHealth){
            heatlh -= hit;
            CheckHealth();
            fish.StartFlash(new Color(100,100,100,100));
            return true;
        }
        return false;
    }
    void CheckHealth(){
        if(heatlh<=0){
            fish.DropCoin();
            chargingHealth = true;

            //Jakaś klasa spejclana na aprtiklesy w przyszłosci teraz chuj z tym

            particle = new AnimatedSprite2D(){
                SpriteFrames = spriteFrames
            };
            fish.AddChild(particle);
            particle.Play();
        }
    }

    public double GetHealth(){
        return heatlh;
    }
}
