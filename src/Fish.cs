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
    //AnimatedSprite2D particle;
    //Particle jako scena i osobny skrypt

    [Signal]
    public delegate void onDieEventHandler(Fish fish);

    [Export]
    FishHealthComponent healthComponent;
    [Export]
    FishHungerComponent hungerComponent;

    //AnimationPlayer animationPlayer;


    // public void SetFishData(FishData fishData){
    //     this.fishData = fishData;
    // }
    public FishData GetFishData(){
        return fishData;
    }

    public override void _Ready()
    {
        base._Ready();

        healthComponent.InitComponent(this);
        hungerComponent.InitComponent(this);

        tank = TankController.Instance;
        //animationPlayer = GetAnimationPlayer();
        //GD.Print("anim"+animationPlayers);
        //animatedSprite2D.AnimationFinished += OnAnimationFinished;
        //animationPlayer.AnimationFinished += OnAnimationFinished;

        //Scale = fishData.spawnSize();
        
        //animatedSprite2D.SpriteFrames = fishData.sprites;
        sprite2D.Texture = fishData.sprites;
        SetTimer();
        //SpawnFish();
    }

    public void SpawnFish(FishData data){
        //spadanie do akwarium
        fishData = data;

        Scale = fishData.spawnSize();
        
        if(Scale.X < 2){
            AddToGroup("fishFood");
        }
    }

    public FishHungerComponent GetHungerComponent(){
        return hungerComponent;
    }

    public FishHealthComponent GetFishHealthComponent(){
        return healthComponent;
    }

    public override void Die(){
        sprite2D.Modulate = new Color(1,1,1,0.5f);
        animationPlayer.Play("dead");
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
        if(coinScene!=null && hungerComponent.GetHunger()>=0){
            Coin coin = (Coin) coinScene.Instantiate();
            coin.Position = Position;
            coin.ZIndex=100;
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
            //animatedSprite2D.Play("turn");
            animationPlayer.Play("turn");
            return true;
        }

        return false;
    }

    void _on_animation_player_animation_finished(String finishedAnim){
        if(finishedAnim == "turn"){
            //animatedSprite2D.FlipH = !animatedSprite2D.FlipH;
            animationPlayer.Play("swimm");
            //animatedSprite2D.Play("swimm");
        }
        if(finishedAnim == "eat"){
            animationPlayer.Play("swimm");
            //animatedSprite2D.Play("swimm");
        }
    }

    public override void OnLeftClicked(){
        base.OnLeftClicked();

        if(healthComponent.GetHit(PlayerStatus.Instance.GetBuffValue("clickPower").As<float>())){
            GetTree().Root.SetInputAsHandled();
        }

        tank.SetActiveFish(this);
        //GD.Print(tank.GetActiveFish());
    }

    public void SetNewDirection(Vector2 direction){
        oldDirection = newDirection;
        newDirection = direction;
        if(newDirection.X>0){
            sprite2D.FlipH = true;
        }
        else{
            sprite2D.FlipH = false;
        }
        //GD.Print("old: "+oldDirection+" \n new: "+newDirection);
    }

    public RandomNumberGenerator GetRng(){
        return rng;
    }
    public override string ToString()
    {
        return "Rybka: " + fishData + "\n Index: " + GetIndex() + "\n Hunger: "+hungerComponent.GetHunger() + "\n Health: "+healthComponent.GetHealth();
    }


}
