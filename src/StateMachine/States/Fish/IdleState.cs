using Godot;
using System;
using System.IO;
using System.Net.Http.Headers;

public partial class IdleState : State
{
    private double swimmTime;
    private Vector2 swimmVector = Vector2.Left;
    private Vector2 oldVector;
    RandomNumberGenerator rng = new RandomNumberGenerator();

    Fish fish;
    bool flipped;

    public override void Enter(){
        fish = (Fish) parent;
        swimmTime=0;
        swimmVector = fish.lastDirection;
        fish.isSpirteFlipped = animatedSprite2D.FlipH;
        //swimmVector = Vector2.Left;
        base.Enter();
    }

    public override void Update(double delta){
        base.Update(delta);
        //if(Input.IsActionJustPressed("jump")){
            //fish.TargetedFood();
        if(fish.isHungry() && GetTree().GetNodesInGroup("food").Count>0){
            EmitSignal(SignalName.transitioned,this,"Feeding");
        }

    }
    public override void PhysicsUpdate(double delta)
    {
        
        if(animatedSprite2D.Animation == "idle"){
            ChangeSwimm();
            swimmTime -= delta;
            parent.Position += swimmVector;
        }else{
            parent.Position += fish.lastDirection*0.5f;
        }

        base.PhysicsUpdate(delta);
    }

    public void ChangeSwimm(){

        if(swimmTime<=0){
            fish.lastDirection = swimmVector;
            swimmVector = new Vector2(rng.RandfRange(-1,1) ,rng.RandfRange(-1,1))*fish.GetSpeed();
            swimmTime = 2;
            fish.TurnSide(swimmVector.X, fish.lastDirection.X);
        }
    }

}
