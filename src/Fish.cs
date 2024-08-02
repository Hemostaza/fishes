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

    SpriteFrames sprites;

    public bool isSpirteFlipped;

    bool needFood = true;

    public Vector2 newDirection;
    public Vector2 oldDirection;

    double heatlh;
    bool chargingHealth;
    double chargingHealthTime;

    TankController tank;

    Timer flashDurationTimer = new Timer();
    float flashDuration = 0.1f;

    //FishHungerController
    //FishClickController
    //FishHealthController

    [Export]
    SpriteFrames particleFrames;
    AnimatedSprite2D particle;
    //Particle jako scena i osobny skrypt

    [Signal]
    public delegate void onDieEventHandler(Fish fish);

    public void SetFishData(FishData fishData){
        this.fishData = fishData;
    }

    public override void _Ready()
    {
        base._Ready();

        tank = TankController.Instance;
        animatedSprite2D.AnimationFinished += OnAnimationFinished;

        heatlh = fishData.maxHealth;
        chargingHealthTime = fishData.healthRegen;

        if(fishData.hungerMeter==0){
            needFood = false;
        }
        //isSpirteFlipped = animatedSprite2D.FlipH;
        Scale = fishData.spawnSize();
        animatedSprite2D.SpriteFrames = fishData.sprites;
        //tank = (Tank) GetParent();
        hunger = fishData.hungerMeter;//rng.RandiRange(1,3);
        SetTimer();
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
                    animatedSprite2D.Modulate = new Color(0.7f,0.9f,0.7f,1);
                    //Change sprites;
                }
                starving=true;
            }
            if(hunger<-5){
                EmitSignal(SignalName.onDie,this);
                //QueueFree();
                //Emit signal dieded
            }
        }
        Healing(delta);
    }

    void Healing(double delta){
        if(chargingHealth==true){

            heatlh += fishData.healthRegen * delta * PlayerStatus.Instance.GetFishHealthRegeneration();
            if(heatlh>=fishData.maxHealth){
                heatlh = fishData.maxHealth;
                chargingHealth=false;
                particle.QueueFree();
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
        animatedSprite2D.Modulate = new Color(1,1,1,1);
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
        if(heatlh>0 && !chargingHealth){
            heatlh -= PlayerStatus.Instance.GetClickPower();
            CheckHealth();
            StartFlash(new Color(100,100,100,100));
        }
        tank.SetActiveFish(this);
        GD.Print(tank.GetActiveFish());
    }

    void FlashLight(){
        Modulate = new Color(100,100,100,100);
    }

    void CheckHealth(){
        if(heatlh<=0){
            DropCoin();
            chargingHealth = true;
            particle = new AnimatedSprite2D
            {
                SpriteFrames = particleFrames
            };
            AddChild(particle);
            particle.Play();
        }
    }

    public override string ToString()
    {
        return "Rybka: " + fishData + "\n Index: " + GetIndex() + "\n Hunger: "+hunger + "\n Health: "+heatlh;
    }

    public void SetNewDirection(Vector2 direction){
        oldDirection = newDirection;
        newDirection = direction;
    }

    public RandomNumberGenerator GetRng(){
        return rng;
    }

}
