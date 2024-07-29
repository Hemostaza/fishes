using Godot;
using System;

public partial class FeedingState : State
{

    Fish fish;

    Food target;

    double searchTimer;

    Vector2 direction;

    public override void Enter(){
        base.Enter();
        searchTimer = 1;
        fish = (Fish) parent;
        animatedSprite2D.FlipH = fish.isSpirteFlipped;
        direction = fish.lastDirection;
        SearchForTarget();

    }

    public bool SearchForTarget(){
        if (animatedSprite2D.Animation == "idle")
        {
            Food oldTarget = target;
            target = fish.TargetedFood();
            if (target == null)
            {
                fish.lastDirection = direction;
                EmitSignal(SignalName.transitioned, this, "Idle");
                return false;
            }
            if (target != null || !oldTarget.Equals(target))
            {
                fish.lastDirection = direction;
                direction = fish.Position.DirectionTo(target.Position);
                fish.TurnSide(direction.X, fish.lastDirection.X);
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

    }
    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
    }

    public void SwimmToTarget(){
        if(target!=null){
            if(animatedSprite2D.Animation=="idle"){
                fish.lastDirection = direction;
                direction = fish.Position.DirectionTo(target.Position);
                parent.Position += direction*2;
            }else{
                parent.Position += fish.lastDirection;
            }

            if(fish.OverlapsArea(target) && animatedSprite2D.Animation=="idle"){
                fish.Eated();
                target.QueueFree();
                animatedSprite2D.Play("eat");
                fish.lastDirection = direction;
                EmitSignal(SignalName.transitioned,this,"Idle");
            }
        }
            
    }

}
