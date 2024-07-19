using Godot;
using System;

public partial class FeedingState : State
{

    Fish fish;

    Food target;

    double searchTimer;

    Vector2 direction;

    bool flipped;
    bool isFlipped;

    public override void Enter(){
        base.Enter();
        searchTimer = 1;
        fish = (Fish) parent;
        
        fish.isSpirteFlipped = animatedSprite2D.FlipH;
        direction = fish.lastDirection;
        SearchForTarget();
        // target = fish.TargetedFood();
        // direction = fish.Position.DirectionTo(target.Position);
        // TurnSide(direction.X,fish.Position.X);
    }

    public bool SearchForTarget(){
        Food oldTarget = target;

        target = fish.TargetedFood();
        if(target==null){
            EmitSignal(SignalName.transitioned,this,"Idle");
            return false;
        }
        if(target!=null || !oldTarget.Equals(target)){
            fish.lastDirection = direction;
            direction = fish.Position.DirectionTo(target.Position);
            fish.TurnSide(direction.X,fish.lastDirection.X);
            return true;
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

        //if(!target.IsQueuedForDeletion()){
        // try{
        //     ChangeSwimm();
        // }catch (ObjectDisposedException){
        //     //target = fish.TargetedFood();
        //     target = fish.TargetedFood();
        //     direction = fish.Position.DirectionTo(target.Position);
        //     TurnSide(direction.X,fish.Position.X);
        //     if(target==null){
        //         EmitSignal(SignalName.transitioned,this,"Idle");
        //     }
        // }
        //}
    }
    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
    }

    public void SwimmToTarget(){
            if(animatedSprite2D.Animation=="idle"){
                fish.lastDirection = direction;
                direction = fish.Position.DirectionTo(target.Position);
                parent.Position += direction*2;
            }else{
                parent.Position += direction;
            }

            if(fish.OverlapsArea(target)){
                fish.Eated();
                target.QueueFree();
                animatedSprite2D.Play("eat");
                EmitSignal(SignalName.transitioned,this,"Idle");
            }
    }

    // public void ChangeSwimm(){

    //     if(searchTimer<=0 || target == null){
    //         target = fish.TargetedFood();

    //         direction = fish.Position.DirectionTo(target.Position);
    //         TurnSide(direction.X,fish.Position.DirectionTo(target.Position).X);

    //         searchTimer = 1;
    //     }else{
    //         direction = fish.Position.DirectionTo(target.Position);
    //         parent.Position += direction*2;
    //         if(fish.OverlapsArea(target)){
    //             fish.Eated();
    //             target.QueueFree();
    //             EmitSignal(SignalName.transitioned,this,"Idle");
    //         }
    //     }
    // }


    bool IsOnFood(){
        return target.Position.Equals(fish.Position) ? true:false;
    }

}
