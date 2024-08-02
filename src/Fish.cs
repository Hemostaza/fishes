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
    public FishData fishData;

    [Export]
    public PackedScene coinScene;

    SpriteFrames sprites;

    public Vector2 newDirection;
    public Vector2 oldDirection;

    TankController tank;

    Timer flashDurationTimer = new Timer();
    float flashDuration = 0.1f;

    [Export]
    SpriteFrames particleFrames;
    AnimatedSprite2D particle;
    //Particle jako scena i osobny skrypt

    [Signal]
    public delegate void onDieEventHandler(Fish fish);

    [Export]
    FishHealthComponent healthComponent;
    [Export]
    FishHungerComponent hungerComponent;


    public void SetFishData(FishData fishData){
        this.fishData = fishData;
    }
    public FishData GetFishData(){
        return fishData;
    }

    public override void _Ready()
    {
        base._Ready();

        healthComponent.InitComponent(this);
        hungerComponent.InitComponent(this);

        tank = TankController.Instance;
        animatedSprite2D.AnimationFinished += OnAnimationFinished;

        Scale = fishData.spawnSize();
        
        animatedSprite2D.SpriteFrames = fishData.sprites;
        SetTimer();
    }

    public void SpawnFish(){
        //spadanie do akwarium
        if(fishData.spawnSize().X < 2){
            AddToGroup("fishFood");
        }
    }

    public FishHungerComponent GetHungerComponent(){
        return hungerComponent;
    }

    public FishHealthComponent GetFishHealthComponent(){
        return healthComponent;
    }

    public void Die(bool fromHunger){
        
    }

    void SetTimer(){
        flashDurationTimer.OneShot = true;
        flashDurationTimer.WaitTime = flashDuration;
        flashDurationTimer.Timeout += StopFlash;
        AddChild(flashDurationTimer);
    }

    void StopFlash(){
        Modulate = new Color(1,1,1,1);
    }

    public void StartFlash(Color color){
        flashDurationTimer.Start();
        Modulate = color;
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
        return fishData.maxSpeed;
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

        if(healthComponent.GetHit(PlayerStatus.Instance.GetClickPower())){
            GetTree().Root.SetInputAsHandled();
        }

        tank.SetActiveFish(this);
        GD.Print(tank.GetActiveFish());
    }

    public void SetNewDirection(Vector2 direction){
        oldDirection = newDirection;
        newDirection = direction;
    }

    public RandomNumberGenerator GetRng(){
        return rng;
    }
    public override string ToString()
    {
        return "Rybka: " + fishData + "\n Index: " + GetIndex() + "\n Hunger: "+hungerComponent.GetHunger() + "\n Health: "+healthComponent.GetHealth();
    }


}
