using Godot;
using System;

public partial class FeedingState : State
{

    Fish fish;

    Food target;

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
        if (animatedSprite2D.Animation == "swimm")
        {
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
                fish.TurnSide();
                return true;
            }
        }
        return false;
    }

    public override void Update(double delta){
        base.Update(delta);
        searchTimer-=delta;

        if(searchTimer<=0){
            SearchForTarget();
            searchTimer = 1;
        }
        try{
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
            direction = fish.Position.DirectionTo(target.Position);
            fish.SetNewDirection(direction);
            if(animatedSprite2D.Animation=="swimm"){
                parent.Position += direction * (fish.GetSpeed() * 2);
            }
            else{
                parent.Position += direction;
            }
            if(fish.OverlapsArea(target) && animatedSprite2D.Animation=="swimm"){
                fish.GetHungerComponent().Eated(target.GetFoodData().Name,target.GetFoodData().nutrition);
                target.QueueFree();
                animatedSprite2D.Play("eat");
                EmitSignal(SignalName.transitioned,this,"Idle");
            }
        }
    }
}
