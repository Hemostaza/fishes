using Godot;
using System;

public partial class FeedingState : State
{

    Fish fish;

    Node2D target;

    double searchTimer;

    Vector2 direction;

    public override void InitState()
    {
        base.InitState();
        fish = (Fish) parent;
    }

    public override void Enter(){
        base.Enter();
        searchTimer = 1;

        SearchForTarget();

    }

    public bool SearchForTarget(){
        //if (animatedSprite2D.Animation == "swimm")
        //{
        //GD.Print("Szukam celu");
            Node2D oldTarget = target;
            target = fish.GetHungerComponent().TargetedFood();
            if (target == null)
            {
                EmitSignal(SignalName.transitioned, this, "Idle");
                return false;
            }
            if (target != null || !oldTarget.Equals(target))
            {
                direction = fish.Position.DirectionTo(target.Position);
                fish.SetNewDirection(direction);
                // fish.TurnSide();
                //GD.Print("Cel znalezion: "+target);
                fish.TurnSide();
                return true;
            }
        //}
        return false;
    }

    public override void Update(double delta){
        base.Update(delta);
        searchTimer-=delta;

        if(searchTimer<=0){
            //GD.Print("Czas sie skonczyl.");
            SearchForTarget();
            searchTimer = 1;
        }
        try{
            //GD.Print("Płyne do celu: ");
            SwimmToTarget();
        }catch (ObjectDisposedException){
            SearchForTarget();
        }

        if(fish.GetHungerComponent().GetHunger()<-5){
            EmitSignal(SignalName.transitioned,this,"Dieded");
        }

    }
    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
    }

    public void SwimmToTarget(){
        if(target!=null){
            if(animationPlayer.CurrentAnimation=="swimm"){
                direction = fish.Position.DirectionTo(target.Position);
                fish.SetNewDirection(direction);
                parent.Position += direction * (fish.GetSpeed() * 2);
            }
            else{
                parent.Position += fish.oldDirection;
            }
            if(fish.OverlapsArea(target) && animationPlayer.CurrentAnimation=="swimm"){
                fish.GetHungerComponent().Eated(target);
                target.QueueFree();
                animationPlayer.Play("eat");
                EmitSignal(SignalName.transitioned,this,"Idle");
            }
            
        }
    }
}
