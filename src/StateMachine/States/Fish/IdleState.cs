using Godot;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

public partial class IdleState : State
{
    private double swimmTime;
    private Vector2 swimmVector = Vector2.Left;
    private Vector2 oldVector;
    

    Fish fish;
    bool flipped;
    bool debug = false;

    public override void InitState()
    {
        base.InitState();
        fish = (Fish) parent;
        fish.onDie += Die;
        if(debug){
            GD.Print("Debug ON");
        }
    }

    void Die(Fish fish){
        EmitSignal(SignalName.transitioned,this,"Dieded");
    }

    public override void Enter(){
        base.Enter();
        swimmTime=0;
    }

    public override void Update(double delta){
        base.Update(delta);
        if(fish.GetHungerComponent().isHungry() && GetTree().GetNodesInGroup("food").Count>0 ){ //|| GetTree().GetNodesInGroup("fishFood").Count>0
            EmitSignal(SignalName.transitioned,this,"Feeding");
        }

    }
    public override void PhysicsUpdate(double delta)
    {
        
        if(animatedSprite2D.Animation == "swimm"){
            ChangeSwimm();
            swimmTime -= delta;
            parent.Position += swimmVector;
        }else{
            parent.Position += fish.newDirection*0.5f;
        }

        base.PhysicsUpdate(delta);
    }

    public void ChangeSwimm(){

        if(swimmTime<=0){
            swimmVector = new Vector2(fish.GetRng().RandfRange(-1,1) ,fish.GetRng().RandfRange(-1,1)) 
                * fish.GetRng().RandfRange(fish.GetSpeed()/2,fish.GetSpeed());
            fish.SetNewDirection(swimmVector);
            fish.TurnSide();
            swimmTime = 2;
        }
    }

}
